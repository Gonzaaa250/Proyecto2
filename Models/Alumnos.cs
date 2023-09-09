using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Alumno{
    [Key]
    public int AlumnoId {get; set;}
    public string NombreA {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento { get;set ; }
    public bool Eliminar {get; set;}
    public virtual Carrera? Carrera{ get; set;}
}