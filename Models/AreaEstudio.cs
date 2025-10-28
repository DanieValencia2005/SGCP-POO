using System;
using System.Collections.Generic;

namespace SGCP_POO.Models
{
    public partial class AreaEstudio
    {
        public int IdAreaEstudio { get; set; }
        public int IdEstudiante { get; set; }
        public int IdRecurso { get; set; }
        public DateTime FechaUso { get; set; }
        public string NombreTarjeta { get; set; }

        // 🔗 Relaciones
        public virtual Estudiante IdEstudianteNavigation { get; set; }
        public virtual Recurso Recurso { get; set; }

        public virtual ICollection<Retroalimentacion> Retroalimentaciones { get; set; }

        public virtual ICollection<Repositorio> Repositorios { get; set; } = new List<Repositorio>();

    }
}
