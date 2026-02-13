using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Informacion
{
    public int IdInformacion { get; set; }

    public int? IdEstudiante { get; set; }

    public string? CorreoPersonal { get; set; }

    public int? Edad { get; set; }

    public string? Telefono { get; set; }

    public string? Habilidades { get; set; }

    public string? Deficiencias { get; set; }

    public int? TiempoDedicacion { get; set; }


    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
