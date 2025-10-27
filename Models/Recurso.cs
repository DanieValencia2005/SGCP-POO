using System;
using System.Collections.Generic;

namespace SGCP_POO.Models;

public partial class Recurso
{
    public int IdRecurso { get; set; }

    public int IdEstudiante { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public string? PalabrasClave { get; set; }

    public string? Tema { get; set; }

    public string? Dificultad { get; set; }

    public string? Formato { get; set; }

    public string? Enlace { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }

}
