using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Asignatura{
    [Key]
    public int AsignaturaId {get; set;}
    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [MaxLength (50, ErrorMessage ="La longitud del nombre no puede ser mayor a 10 caracteres.")]
    public string NombreA {get; set;}
    public int CarreaId {get; set;}
    public bool Eliminar {get; set;}
    public virtual Carrera? Carrera {get; set;}
    public virtual ICollection <Tarea> Tarea {get; set;}
}