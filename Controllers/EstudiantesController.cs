using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCP_POO.Models;


namespace SGCP_POO.Controllers
{
    [RequireStudentSession]
    public class EstudiantesController : Controller
    {
        private readonly SGCPContext _context;

        public EstudiantesController(SGCPContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }


        // POST
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEstudiante(
            [Bind("IdEstudiante,Nombre,CorreoInstitucional,Contraseña")] Estudiante estudiante)
        {
            if (!ModelState.IsValid)
                return View("Create", estudiante);

            bool correoExiste = await _context.Estudiantes
                .AnyAsync(e => e.CorreoInstitucional == estudiante.CorreoInstitucional);

            if (correoExiste)
            {
                ModelState.AddModelError(
                    nameof(estudiante.CorreoInstitucional),
                    "El correo institucional ya está en uso"
                );
                return View("Create", estudiante);
            }

            _context.Add(estudiante);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }


        public IActionResult POO()
        {
            return View();
        }

        public IActionResult GuardarRecurso()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarRecurso([Bind("Titulo,Descripcion,PalabrasClave,Tema,Dificultad,Formato,Enlace")] Recurso recurso)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");

            if (idEstudiante == null)
            {
                ModelState.AddModelError("", "No se encontró la sesión del estudiante.");
                return View(recurso);
            }

            recurso.IdEstudiante = idEstudiante.Value;

            if (ModelState.IsValid)
            {
                // Guardar el recurso sin asociarlo aún al área de estudio
                _context.Add(recurso);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Recurso guardado correctamente.";
                ModelState.Clear(); // Limpia los campos del formulario
                return View();
            }

            return View(recurso);
        }



        public async Task<IActionResult> BuscarRecursos(string tema, string dificultad, string formato)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");
            if (idEstudiante == null) return RedirectToAction("Index", "Login");

            var recursos = _context.Recursos
                .Where(r => r.IdEstudiante == idEstudiante.Value)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tema))
                recursos = recursos.Where(r => r.Tema != null && r.Tema.Contains(tema));

            if (!string.IsNullOrEmpty(dificultad))
                recursos = recursos.Where(r => r.Dificultad != null && r.Dificultad == dificultad);

            if (!string.IsNullOrEmpty(formato))
                recursos = recursos.Where(r => r.Formato != null && r.Formato.Contains(formato));

            ViewBag.Tema = tema;
            ViewBag.Dificultad = dificultad;
            ViewBag.Formato = formato;

            return View(await recursos.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEnAreaEstudio(int id)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");
            if (idEstudiante == null) return RedirectToAction("Index", "Login");

            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(r => r.IdRecurso == id && r.IdEstudiante == idEstudiante.Value);

            if (recurso == null)
                return NotFound("Recurso no encontrado o no pertenece al estudiante");

            var tarjeta = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .FirstOrDefaultAsync(t => t.IdEstudiante == idEstudiante.Value && t.NombreTarjeta == "Mi tarjeta de estudio");

            if (tarjeta == null)
            {
                tarjeta = new TarjetaConocimiento
                {
                    IdEstudiante = idEstudiante.Value,
                    NombreTarjeta = "Mi tarjeta de estudio",
                    FechaCreacion = DateTime.Now
                };
                _context.TarjetasConocimiento.Add(tarjeta);
                await _context.SaveChangesAsync();
            }

            // Evitar duplicar el recurso
            if (!tarjeta.TarjetasRecursos.Any(tr => tr.IdRecurso == recurso.IdRecurso))
            {
                var tarjetaRecurso = new TarjetaRecurso
                {
                    IdTarjeta = tarjeta.IdTarjeta,
                    IdRecurso = recurso.IdRecurso,
                    FechaRegistro = DateTime.Now
                };
                _context.TarjetasRecursos.Add(tarjetaRecurso);
                await _context.SaveChangesAsync();
            }

            TempData["Mensaje"] = "Recurso añadido correctamente a tu área de estudio.";
            return RedirectToAction("BuscarRecursos");
        }


        public async Task<IActionResult> AreaEstudio()
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");
            if (idEstudiante == null) return RedirectToAction("Index", "Login");

            var tarjeta = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .ThenInclude(tr => tr.Recurso)
                .FirstOrDefaultAsync(t => t.IdEstudiante == idEstudiante.Value && t.NombreTarjeta == "Mi tarjeta de estudio");

            return View(tarjeta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTarjetaConocimiento(
            string nombreTarjeta,
            List<int> recursosIds,
            List<string> retroalimentaciones,
            List<int> calificaciones)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");
            if (idEstudiante == null) return RedirectToAction("Index", "Login");

            if (string.IsNullOrEmpty(nombreTarjeta) || recursosIds == null || !recursosIds.Any())
            {
                TempData["Error"] = "Debe ingresar un nombre y seleccionar al menos un recurso.";
                return RedirectToAction("AreaEstudio");
            }

            var tarjeta = new TarjetaConocimiento
            {
                IdEstudiante = idEstudiante.Value,
                NombreTarjeta = nombreTarjeta,
                FechaCreacion = DateTime.Now
            };

            _context.TarjetasConocimiento.Add(tarjeta);
            await _context.SaveChangesAsync();

            for (int i = 0; i < recursosIds.Count; i++)
            {
                var tarjetaRecurso = new TarjetaRecurso
                {
                    IdTarjeta = tarjeta.IdTarjeta,
                    IdRecurso = recursosIds[i],
                    Retroalimentacion = retroalimentaciones != null && retroalimentaciones.Count > i ? retroalimentaciones[i] : null,
                    Calificacion = calificaciones != null && calificaciones.Count > i ? calificaciones[i] : (int?)null,
                    FechaRegistro = DateTime.Now
                };
                _context.TarjetasRecursos.Add(tarjetaRecurso);
            }

            await _context.SaveChangesAsync();

            var areaEstudio = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .FirstOrDefaultAsync(t => t.IdEstudiante == idEstudiante.Value && t.NombreTarjeta == "Mi tarjeta de estudio");

            if (areaEstudio != null)
            {
                var recursosAEliminar = areaEstudio.TarjetasRecursos
                    .Where(tr => recursosIds.Contains(tr.IdRecurso))
                    .ToList();

                if (recursosAEliminar.Any())
                {
                    _context.TarjetasRecursos.RemoveRange(recursosAEliminar);
                    await _context.SaveChangesAsync();
                }
            }

            TempData["Mensaje"] = "Tarjeta de conocimiento creada correctamente.";
            return RedirectToAction("AreaEstudio");
        }

        public async Task<IActionResult> Repositorio()
        {
            var tarjetas = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .ThenInclude(tr => tr.Recurso)
                .ToListAsync();

            // Agrupamos cada tarjeta solo por el primer tema válido que tenga
            var agrupado = tarjetas
                .Where(t => t.TarjetasRecursos.Any(tr => !string.IsNullOrEmpty(tr.Recurso.Tema)))
                .GroupBy(t => t.TarjetasRecursos
                    .FirstOrDefault(tr => !string.IsNullOrEmpty(tr.Recurso.Tema))!.Recurso.Tema)
                .ToDictionary(g => g.Key, g => g.ToList());

            return View(agrupado);
        }


        [HttpPost]
        public async Task<IActionResult> EditarRecurso(int idTarjetaRecurso, Recurso Recurso, string Retroalimentacion, int? Calificacion)
        {
            var tr = await _context.TarjetasRecursos
                .Include(t => t.Recurso)
                .FirstOrDefaultAsync(t => t.IdTarjetaRecurso == idTarjetaRecurso);

            if (tr == null)
                return NotFound("No se encontró el recurso asociado a la tarjeta.");

            tr.Recurso.Titulo = Recurso.Titulo;
            tr.Recurso.Descripcion = Recurso.Descripcion;
            tr.Recurso.PalabrasClave = Recurso.PalabrasClave;
            tr.Recurso.Tema = Recurso.Tema;
            tr.Recurso.Dificultad = Recurso.Dificultad;
            tr.Recurso.Formato = Recurso.Formato;
            if (!string.IsNullOrEmpty(Recurso.Enlace))
            {
                tr.Recurso.Enlace = Recurso.Enlace;
            }
            tr.Retroalimentacion = Retroalimentacion;
            tr.Calificacion = Calificacion;
            tr.FechaRegistro = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Cambios guardados correctamente.";
            return RedirectToAction("Repositorio");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTarjeta(int idTarjeta)
        {
            var tarjeta = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .FirstOrDefaultAsync(t => t.IdTarjeta == idTarjeta);

            if (tarjeta == null)
            {
                TempData["Mensaje"] = "Tarjeta no encontrada.";
                return RedirectToAction("Repositorio");
            }

            _context.TarjetasRecursos.RemoveRange(tarjeta.TarjetasRecursos);
            await _context.SaveChangesAsync();

            _context.TarjetasConocimiento.Remove(tarjeta);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Tarjeta eliminada correctamente.";
            return RedirectToAction("Repositorio");
        }
        public IActionResult Personalizacion()
        {
            return View();
        }
        public IActionResult Informacion_Proyecto()
        {
            return View();
        }

    }

}
