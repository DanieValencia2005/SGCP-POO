using Microsoft.AspNetCore.Mvc;
using SGCP_POO.Models;

namespace SGCP_POO.Controllers
{
    public class LoginController : Controller
    {
        private readonly SGCPContext _context;

        public LoginController(SGCPContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View("Login");
        }

        // GET: Registro
        public IActionResult Registro()
        {
            return View(); // carga Views/Login/Registro.cshtml
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string Correo_institucional, string Contrasena)
        {
            Usuario? usuario = await Usuario.Autenticar_Usuario(
                _context,
                Correo_institucional,
                Contrasena
            );

            if (usuario != null)
            {
                HttpContext.Session.SetString("Nombre", usuario.Nombre);
                HttpContext.Session.SetString("Correo", usuario.CorreoInstitucional);

                if (usuario is Estudiante estudiante)
                {
                    HttpContext.Session.SetString("Rol", "Estudiante");
                    HttpContext.Session.SetInt32("IdEstudiante", estudiante.IdEstudiante);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.MensajeError = "Correo o contraseña inválidos.";
            return View("Login");
        }
    }
}
