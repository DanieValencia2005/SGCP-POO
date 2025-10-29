namespace SGCP_POO.Models
{
    public class TarjetaConocimiento
    {
        public int IdTarjeta { get; set; }
        public string NombreTarjeta { get; set; }
        public int IdEstudiante { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navegación
        public virtual Estudiante Estudiante { get; set; }
        public virtual ICollection<TarjetaRecurso> TarjetasRecursos { get; set; } = new List<TarjetaRecurso>();
    }
}