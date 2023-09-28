using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models;
public class Tarea{
    [Key]
    public int TareaId {get; set;}
    public string Titulo {get; set;}
    public string Descripcion {get; set;}
    public DateTime FechaCreacion {get; set;}
    public DateTime FechaVencimiento {get;set;}
    public bool Eliminar {get; set;}
    public int ProfesorId {get; set;}
    public int AsignaturaId {get; set;}
    public virtual Profesor? Profesor {get; set;}
    public virtual Asignatura? Asignatura {get; set;}
}