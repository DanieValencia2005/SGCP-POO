using System;
using System.Collections.Generic;

namespace SGCP_POO.Models
{
    public partial class Repositorio
    {
        public int IdRepositorio { get; set; }
        public int IdEstudiante { get; set; }
        public int IdAreaEstudio { get; set; }

        public virtual Estudiante? IdEstudianteNavigation { get; set; }
        public virtual AreaEstudio? IdAreaEstudioNavigation { get; set; }
    }
}
