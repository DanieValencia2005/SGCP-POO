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

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstudiante,Nombre,CorreoInstitucional,Contraseña")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }

            return View(estudiante);
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
        }

    }
}


