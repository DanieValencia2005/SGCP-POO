using System.ComponentModel.DataAnnotations;

namespace SGCP_POO.Models
{
    public partial class Estudiante : Usuario
    {
        public int IdEstudiante { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public override string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public override string CorreoInstitucional { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "La contraseña debe tener al menos 6 caracteres y máximo 15")]
        public override string Contraseña { get; set; } = null!;

        public virtual ICollection<Informacion> Informacions { get; set; } = new List<Informacion>();
        public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
        public virtual ICollection<TarjetaConocimiento> Tarjetas { get; set; } = new List<TarjetaConocimiento>();
    }
}
