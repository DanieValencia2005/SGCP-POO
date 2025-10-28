namespace SGCP_POO.Models
{
    public partial class Retroalimentacion
    {
        public int IdRetroalimentacion { get; set; }
        public int IdAreaEstudio { get; set; }
        public int IdRecurso { get; set; }   // ✅ FALTABA
        public string? RetroalimentacionTexto { get; set; }
        public int? Calificacion { get; set; }

        public virtual AreaEstudio? AreaEstudio { get; set; } // 🔄 Corrección de nombre
        public virtual Recurso? Recurso { get; set; }         // ✅ FALTABA
    }
}
