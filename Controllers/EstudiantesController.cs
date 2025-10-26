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
<<<<<<< HEAD
        }

        // GET: Estudiantes/Actualizar
        public IActionResult Actualizar()
        {
            return View();
        }

        // GET: Estudiantes/POO
        public IActionResult POO()
        {
            return View();
        }

        // GET: Estudiantes/GuardarRecurso
        public IActionResult GuardarRecurso()
        {
            return View();
        }

        // GET: Estudiantes/BuscarRecurso
        public IActionResult BuscarRecursos()
        {
            return View();
        }
        // Acción para cargar Área de Estudio
        public IActionResult AreadeEstudio()
        {
            return View();
        }
        public IActionResult Repositorio()
        {
            return View();
=======
>>>>>>> 0f34125 (Opcion buscar recurso en desarrollo y guardar recurso finalizada)
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
            var recursos = from r in _context.Recursos select r;

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

        public IActionResult AreadeEstudio()
        {
            return View();
        }

        public IActionResult Repositorio()
        {
            return View();
        }
    }
}


