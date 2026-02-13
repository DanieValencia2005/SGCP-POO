using Microsoft.EntityFrameworkCore;

namespace SGCP_POO.Models
{
    public abstract class Usuario
    {
        public virtual string Nombre { get; set; } = null!;
        public virtual string CorreoInstitucional { get; set; } = null!;
        public virtual string Contraseña { get; set; } = null!;

        public static async Task<Usuario?> Autenticar_Usuario(
            SGCPContext context,
            string correo,
            string contraseña)
        {
            var estudiante = await context.Estudiantes
                .FirstOrDefaultAsync(e =>
                    e.CorreoInstitucional == correo &&
                    e.Contraseña == contraseña);

            if (estudiante != null)
                return estudiante;

            return null;
        }
    }
}
