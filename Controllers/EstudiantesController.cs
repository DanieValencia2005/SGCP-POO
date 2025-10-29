using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCP_POO.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SGCP_POO.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly SGCPContext _context;

        public EstudiantesController(SGCPContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEstudiante([Bind("IdEstudiante,Nombre,CorreoInstitucional,Contraseña")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }

            return View(estudiante);
        }

        public IActionResult Actualizar()
        {
            return View();
        }

        public IActionResult POO()
        {
            return View();
        }

        public IActionResult GuardarRecurso()
        {
            return View();
        }
        public IActionResult Repositorio()
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
                _context.Add(recurso);
                await _context.SaveChangesAsync();

                ViewBag.Message = "✅ Recurso guardado correctamente.";
                ModelState.Clear();
                return View();
            }

            return View(recurso);
        }

        public async Task<IActionResult> BuscarRecursos(string tema, string dificultad, string formato)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");
            if (idEstudiante == null)
                return RedirectToAction("Index", "Login");

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

            if (recurso == null) return NotFound("Recurso no encontrado o no pertenece al estudiante");

            // Buscar tarjeta de conocimiento existente
            var tarjeta = await _context.TarjetasConocimiento
                .Include(t => t.TarjetasRecursos)
                .FirstOrDefaultAsync(t => t.IdEstudiante == idEstudiante.Value && t.NombreTarjeta == "Mi tarjeta de estudio");

            if (tarjeta == null)
            {
                tarjeta = new TarjetaConocimiento
                {
                    IdEstudiante = idEstudiante.Value,
                    NombreTarjeta = "Mi tarjeta de estudio",
                    FechaCreacion = DateTime.Now // ✅ Se asigna la fecha aquí
                };
                _context.TarjetasConocimiento.Add(tarjeta);
                await _context.SaveChangesAsync();
            }

            // Agregar recurso a la tarjeta si no existe
            if (!tarjeta.TarjetasRecursos.Any(tr => tr.IdRecurso == recurso.IdRecurso))
            {
                var tarjetaRecurso = new TarjetaRecurso
                {
                    IdTarjeta = tarjeta.IdTarjeta,
                    IdRecurso = recurso.IdRecurso,
                    FechaRegistro = DateTime.Now // ✅ Se asigna fecha aquí
                };
                _context.TarjetasRecursos.Add(tarjetaRecurso);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("BuscarRecursos");
        }

        // Ver Área de Estudio
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
            if (idEstudiante == null)
                return RedirectToAction("Index", "Login");

            if (string.IsNullOrEmpty(nombreTarjeta) || recursosIds == null || !recursosIds.Any())
            {
                TempData["Error"] = "Debe ingresar un nombre y seleccionar al menos un recurso.";
                return RedirectToAction("AreaEstudio");
            }

            // Crear tarjeta
            var tarjeta = new TarjetaConocimiento
            {
                IdEstudiante = idEstudiante.Value,
                NombreTarjeta = nombreTarjeta,
                FechaCreacion = DateTime.Now
            };
            _context.TarjetasConocimiento.Add(tarjeta);
            await _context.SaveChangesAsync();

            // Agregar recursos con retroalimentación y calificación
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

            // ELIMINAR los recursos de la "tarjeta temporal" del Área de Estudio
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

            TempData["Mensaje"] = "✅ Tarjeta de conocimiento creada correctamente.";
            return RedirectToAction("AreaEstudio");
        }





    }
}
