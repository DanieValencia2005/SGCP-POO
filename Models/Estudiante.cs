using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoInstitucional { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Informacion> Informacions { get; set; } = new List<Informacion>();

    public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
    public virtual ICollection<AreaEstudio> AreaEstudios { get; set; } = new List<AreaEstudio>();
    public virtual ICollection<Repositorio> Repositorios { get; set; } = new List<Repositorio>();

}
