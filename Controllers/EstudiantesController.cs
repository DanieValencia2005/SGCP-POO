using Microsoft.AspNetCore.Mvc;
using SGCP_POO.Models;

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

                // Redirigir a una vista de confirmación, login o donde prefieras
                return RedirectToAction("Index", "Login"); // puedes cambiar esto
            }

            return View(estudiante); // Corrige esto, antes decía View("estudiante")
        }
        public IActionResult Actualizar()
        {
            return View(); // Asegúrate de tener la vista "Actualizar.cshtml" dentro de Views/Estudiantes/
        }

    }
}
