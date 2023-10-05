using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto2.Models;
public class Profesor{
    [Key]
    public int ProfesorId {get; set;}
    public string? Nombre {get; set;}
    public string? DNI {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string? Direccion {get; set;}
    public string? Email {get; set;}
    public bool Eliminar {get; set;}
    public int Asignaturaid {get; set;}
public virtual Asignatura? Asignatura {get; set;}
}