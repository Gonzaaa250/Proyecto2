using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Asignatura{
    [Key]
    public int AsignaturaId {get; set;}
    public string NombreA {get; set;}
    public int CarreaId {get; set;}
    public bool Eliminar {get; set;}
    public virtual Carrera? Carrera {get; set;}
    public virtual ICollection<Profesor> Profesor{get;set;}
}