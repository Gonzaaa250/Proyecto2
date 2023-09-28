using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using Proyecto2.Data;
using Proyecto2.Models;
namespace Proyecto2.Controllers;
[Authorize]
public class TareaController : ContentResult
{
    private readonly ILogger<TareaController> _logger;
    private readonly ApplicationDbContext _context;
    public TareaController (ILogger<TareaController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context=  context;
    }
    public IActionResult Index()
    {
        var Asignatura = _context.Asignatura?.ToList();
        ViewBag.AsignaturaId = new SelectList(Asignatura, "AsignaturaId", "NombreA");
        return View();
    }
    public JsonResult BuscarTarea(int TareaId = 0)
    {
        var Tarea = _context.Tarea.ToList();
        if (TareaId > 0)
        {
            Tarea = Tarea.Where(t => t.TareaId == TareaId).OrderBy(t => t.FechaCreacion).ToList();
        }
        return Json(Tarea);
    }
    public JsonResult GuardarTarea(int TareaId, string Titulo, string Descripcion, DateTime FechaCreacion, DateTime FechaVencimiento, int AsignaturaId)
    {
        bool result = false;
        if(!string.IsNullOrEmpty(Titulo))
        {
            if(TareaId ==0)
            {
                var tareaExistente = _context.Tarea.FirstOrDefault(t=> t.Titulo == Titulo);
                if(tareaExistente ==null)
                {
                    var asignatura = _context.Asignatura.FirstOrDefault(a => a.AsignaturaId == AsignaturaId);
                    if(asignatura != null)
                    {
                        var guardartarea = new Tarea
                        {
                            Titulo = Titulo,
                            Descripcion = Descripcion,
                            FechaCreacion = FechaCreacion,
                            FechaVencimiento = FechaVencimiento,
                            Asignatura = asignatura
                        };
                        _context.Add(guardartarea);
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
            else
            {
                var tareaExistente = _context.Tarea.FirstOrDefault(t=> t.Titulo == Titulo && t.TareaId !=TareaId);
                if(tareaExistente == null)
                {
                    var actualizartarea = _context.Tarea.Find(TareaId);
                    if(actualizartarea != null)
                    {
                        actualizartarea.Titulo = Titulo;
                        actualizartarea.Descripcion = Descripcion;
                        actualizartarea.FechaCreacion = FechaCreacion;
                        actualizartarea.FechaVencimiento = FechaVencimiento;
                        actualizartarea.Asignatura = _context.Asignatura.FirstOrDefault(a => a.AsignaturaId == AsignaturaId);

                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
        }
        return Json(result);
    }
    public JsonResult EliminarTarea(int TareaId, int Eliminar)
    {
        int result =0;
        var tarea = _context.Tarea.Find(TareaId);
        if(tarea != null)
        {
            if(Eliminar ==0)
            {
                tarea.Eliminar =false;
                _context.SaveChanges();
            }
            else if(Eliminar ==1)
            {
                _context.Remove(tarea);
                _context.SaveChanges();
            }
            result=1;
        }
        return Json(result);
    }
}