using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Administrador
{
    public int IdAdministrador { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoInstitucional { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
