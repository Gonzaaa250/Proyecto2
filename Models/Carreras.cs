using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Carrera{
    [Key]
    public int CarreraId {get; set;}
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NombreC {get; set;}
    [Required(ErrorMessage = "La duracion de la carrera es obligatoria")]
    public int Duracion {get; set;}
    public bool Eliminar {get; set;}
    public virtual ICollection <Alumno> Alumno {get; set;}
    public virtual ICollection<Asignatura> Asignatura {get; set;}
}