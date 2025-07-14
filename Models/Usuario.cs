using Microsoft.EntityFrameworkCore;

namespace SGCP_POO.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Correo_Institucional { get; set; }
        public string Contraseña { get; set; }

        public Estudiante? Estudiante { get; set; }
        public Administrador? Administrador { get; set; }

        public static async Task<Usuario?> Autenticar_Usuario(SGCPContext context, string correo, string contraseña)
        {
            // Buscar en Estudiantes
            var estudiante = await context.Estudiantes
                .FirstOrDefaultAsync(e => e.CorreoInstitucional == correo && e.Contraseña == contraseña);

            if (estudiante != null)
            {
                return new Usuario
                {
                    Nombre = estudiante.Nombre!,
                    Correo_Institucional = estudiante.CorreoInstitucional!,
                    Contraseña = estudiante.Contraseña!,
                    Estudiante = estudiante
                };
            }

            // Buscar en Administradores
            var administrador = await context.Administradors
                .FirstOrDefaultAsync(a => a.CorreoInstitucional == correo && a.Contraseña == contraseña);

            if (administrador != null)
            {
                return new Usuario
                {
                    Nombre = administrador.Nombre!,
                    Correo_Institucional = administrador.CorreoInstitucional!,
                    Contraseña = administrador.Contraseña!,
                    Administrador = administrador
                };
            }

            // No encontrado
            return null;
        }
    }
}
