using Microsoft.AspNetCore.Mvc;
using SGCP_POO.Models;

namespace SGCP_POO.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly SGCPContext _context;

        public AdministradorController(SGCPContext context)
        {
            _context = context;
        }

        // GET: Administradors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administradors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdministrador,Nombre,CorreoInstitucional,Contraseña")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrador);
                await _context.SaveChangesAsync();

                // Redirige a login u otra vista después de registrar
                return RedirectToAction("Index", "Login"); // puedes cambiar esto
            }

            return View(administrador);
        }
    }
}
