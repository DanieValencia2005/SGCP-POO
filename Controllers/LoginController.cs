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
            return View(); // Esto carga Views/Login/Registro.cshtml
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string Correo_institucional, string Contrasena)
        {
            var usuario = await Usuario.Autenticar_Usuario(_context, Correo_institucional, Contrasena);

            if (usuario != null)
            {
                // Guardar datos en sesión
                HttpContext.Session.SetString("Nombre", usuario.Nombre);
                HttpContext.Session.SetString("Correo", usuario.Correo_Institucional);

                if (usuario.Estudiante != null)
                {
                    HttpContext.Session.SetString("Rol", "Estudiante");
                    HttpContext.Session.SetInt32("IdEstudiante", usuario.Estudiante.IdEstudiante);
                }
                else if (usuario.Administrador != null)
                {
                    HttpContext.Session.SetString("Rol", "Administrador");
                    HttpContext.Session.SetInt32("IdAdministrador", usuario.Administrador.IdAdministrador);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.MensajeError = "Correo o contraseña inválidos.";
            return View("Login");
        }
    }
}
