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

public class AlumnoController : Controller
{
    private readonly ILogger<AlumnoController > _logger;
    public readonly ApplicationDbContext _context;
    public AlumnoController (ILogger<AlumnoController > logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var Carrera = _context.Carrera?.ToList();
        ViewBag.ClubId = new SelectList(Carrera, "CarreraId", "Nombre");
        return View();
    }
    public JsonResult BuscarAlumno(int AlumnoId = 0)
    {
        var alumno = _context.Alumno.ToList();
        if (AlumnoId > 0)
        {
            alumno = alumno.Where(a => a.AlumnoId == AlumnoId).OrderBy(a => a.NombreA).ToList();
        }
        return Json(alumno);
    }
    public JsonResult GuardarAlumno(int AlumnoId, string Nombre, string Apellido, DateTime FechaNacimiento, string Carrera)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(Nombre)&& !string.IsNullOrEmpty(Apellido))
        {
            var alumnoExistente = _context.Alumno.FirstOrDefault(a => a.NombreA == Nombre);
            if (alumnoExistente == null)
            {
                var guardaralumno = new Alumno
                {
                    NombreA = Nombre,
                    Apellido= Apellido,
                    FechaNacimiento = FechaNacimiento,
                    // Carrera = Carrera
                };
                _context.Add(guardaralumno);
                _context.SaveChanges();
                result = true;
            }
            else
            {
                var alumnosExistente = _context.Alumno.FirstOrDefault(a => a.NombreA == Nombre && a.AlumnoId != AlumnoId);
                if (alumnosExistente == null)
                {
                    var alumnoeditar = _context.Alumno.Find(AlumnoId);

                    if (alumnoeditar != null)
                    {
                        alumnoeditar.NombreA = Nombre;
                        alumnoeditar.Apellido = Apellido;
                        alumnoeditar.FechaNacimiento = FechaNacimiento;
                        // alumnoeditar.Carrera = Carrera;

                        _context.SaveChanges();
                        result = true;
                    }
                }

            }
        }
        return Json(result);
    }
    public JsonResult ElimarAlumno(int AlumnoId, int Eliminar)
    {
        int result = 0;
        var alumno = _context.Alumno.Find(AlumnoId);

        if (alumno != null)
        {
            if (Eliminar == 0)
            {
                alumno.Eliminar = false;
                _context.SaveChanges();
            }

            else
            {
                if (Eliminar == 1)
                {
                    alumno.Eliminar = true;
                    _context.Remove(alumno);
                    _context.SaveChanges();
                }
            }
        }
        result = 1;

        return Json(result);
    }
}