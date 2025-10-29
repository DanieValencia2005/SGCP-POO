namespace SGCP_POO.Models
{
    public class TarjetaRecurso
    {
        public int IdTarjetaRecurso { get; set; }
        public int IdTarjeta { get; set; }
        public int IdRecurso { get; set; }
        public string? Retroalimentacion { get; set; }
        public int? Calificacion { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Navegación
        public virtual TarjetaConocimiento Tarjeta { get; set; }
        public virtual Recurso Recurso { get; set; }
    }
}