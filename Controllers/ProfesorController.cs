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
using Proyecto2.Data;
using Proyecto2.Models;
namespace Proyecto2.Controllers;
[Authorize]
public class ProfesorController : Controller
{
    private readonly ILogger<ProfesorController> _logger;
    private readonly ApplicationDbContext _context;
    public ProfesorController(ILogger<ProfesorController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var profesores = _context.Profesor.ToList();
        return View();
    }
    public JsonResult BuscarProfesor(int ProfesorId = 0)
    {
        var profesores = _context.Profesor.ToList();

        if (ProfesorId > 0)
        {
            profesores = profesores.Where(p => p.ProfesorId == ProfesorId).ToList();
        }

        profesores = profesores.OrderBy(p => p.Nombre).ToList();

        return Json(profesores);
    }

    public JsonResult GuardarProfesor(int ProfesorId, string Nombre, DateTime FechaNacimiento, string DNI, string Direccion, string Email, int Asignaturaid)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(DNI))
        {
            if (ProfesorId == 0)
            {
                var profesorExistente = _context.Profesor.FirstOrDefault(p => p.Nombre == Nombre);
                if (profesorExistente == null)
                {
                    var asignatura = _context.Asignatura.FirstOrDefault(a => a.AsignaturaId == Asignaturaid);
                    if (asignatura != null)
                    {
                        var guardarprofesor = new Profesor
                        {
                            Nombre = Nombre,
                            DNI = DNI,
                            Direccion = Direccion,
                            FechaNacimiento = FechaNacimiento,
                            Email = Email,
                            Asignatura = asignatura
                        };
                        _context.Add(guardarprofesor);
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
            else
            {
                var profesorExistente = _context.Profesor.FirstOrDefault(p => p.Nombre == Nombre && p.ProfesorId != ProfesorId);
                if (profesorExistente == null)
                {
                    var actualizarprofesor = _context.Profesor.Find(ProfesorId);
                    if (actualizarprofesor != null)
                    {
                        actualizarprofesor.Nombre = Nombre;
                        actualizarprofesor.Direccion = Direccion;
                        actualizarprofesor.DNI = DNI;
                        actualizarprofesor.FechaNacimiento = FechaNacimiento;
                        actualizarprofesor.Email = Email;
                        actualizarprofesor.Asignatura = _context.Asignatura.FirstOrDefault(a => a.AsignaturaId == Asignaturaid);

                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
        }
        return Json(result);
    }
    public JsonResult EliminarProfesor(int ProfesorId, int Eliminar)
    {
        int result = 0;
        var profesor = _context.Profesor.Find(ProfesorId);
        if (profesor != null)
        {
            if (Eliminar == 0)
            {
                profesor.Eliminar = false;
                _context.SaveChanges();
            }
            else
            {
                if (Eliminar == 1)
                {
                    profesor.Eliminar = true;
                    _context.Remove(profesor);
                    _context.SaveChanges();
                }
            }
        }
        result = 1;
        return Json(result);
    }
}