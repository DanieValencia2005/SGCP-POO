using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Registro
{
    public int IdRegistro { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public string? ConfirmacionContraseña { get; set; }

    public string? CodigoAdministrador { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdAdministrador { get; set; }

    public virtual Administrador? IdAdministradorNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
