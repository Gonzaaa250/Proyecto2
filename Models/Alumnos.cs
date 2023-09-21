using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Alumno{
    [Key]
    public int AlumnoId {get; set;}
    public string Nombre {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public bool Eliminar {get; set;}
    public int CarreraId {get; set;}
    public virtual Carrera? Carrera{ get; set;}
}
public class ListadoAlumno{
    public int AlumnoId {get; set;}
    public string Nombre {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public bool Eliminar {get; set;}
    public string CarreraNombre {get; set;}
}