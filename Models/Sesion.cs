using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Sesion
{
    public int IdSesion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdAdministrador { get; set; }

    public virtual Administrador? IdAdministradorNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
