using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class ProfesorAsignatura{
    [Key] 
    public int ProfesoAsignaturaId {get; set;}
    public int ProfesorId {get; set;}
    public int AsignaturaId {get; set;}
    public virtual Profesor? Profesor {get;set;}
    public virtual Asignatura? Asignatura {get; set;}
}