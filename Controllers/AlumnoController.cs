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
public class AlumnoController : Controller
{
    private readonly ILogger<AlumnoController> _logger;
    public readonly ApplicationDbContext _context;
    public AlumnoController(ILogger<AlumnoController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var Carrera = _context.Carrera?.ToList();
        ViewBag.CarreraId = new SelectList(Carrera, "CarreraId", "NombreC");
        return View();
    }
    public JsonResult BuscarAlumno(int AlumnoId = 0)
    {
        var alumnos = _context.Alumno.Include(a => a.Carrera).ToList();

        if (AlumnoId > 0)
        {
            alumnos = alumnos.Where(a => a.AlumnoId == AlumnoId).ToList();
        }

        alumnos = alumnos.OrderBy(a => a.Carrera.NombreC).ThenBy(a => a.Nombre).ToList();

        List<ListadoAlumno> alumnosMostrar = new List<ListadoAlumno>();
        foreach (var alumno in alumnos)
        {
            var alumnoMostrar = new ListadoAlumno
            {
                AlumnoId = alumno.AlumnoId,
                Nombre = alumno.Nombre,
                FechaNacimiento = alumno.FechaNacimiento,
                Direccion = alumno.Direccion,
                DNI = alumno.DNI,
                Email = alumno.Email,
                CarreraNombre = alumno.Carrera.NombreC
            };
            alumnosMostrar.Add(alumnoMostrar);
        }

        return Json(alumnosMostrar);
    }


    public JsonResult GuardarAlumno(int AlumnoId, string Nombre, DateTime FechaNacimiento, string DNI, string Direccion, string Email, int CarreraId)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(DNI))
        {
            if (AlumnoId == 0)
            {
                var alumnoExistente = _context.Alumno.FirstOrDefault(a => a.Nombre == Nombre);
                if (alumnoExistente == null)
                {
                    var carrera = _context.Carrera.FirstOrDefault(c => c.CarreraId == CarreraId);
                    if (carrera != null)
                    {
                        var alumnoguardar = new Alumno
                        {
                            Nombre = Nombre,
                            FechaNacimiento = FechaNacimiento,
                            Direccion = Direccion,
                            DNI = DNI,
                            Email = Email,
                            Carrera = carrera
                        };
                        _context.Add(alumnoguardar);
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
            else
            {
                var alumnoExistente = _context.Alumno.FirstOrDefault(a => a.Nombre == Nombre && a.AlumnoId != AlumnoId);
                if (alumnoExistente == null)
                {
                    var actualizaralumno = _context.Alumno.FirstOrDefault(a => a.AlumnoId == AlumnoId);
                    if (actualizaralumno != null)
                    {
                        actualizaralumno.Nombre = Nombre;
                        actualizaralumno.FechaNacimiento = FechaNacimiento;
                        actualizaralumno.Direccion = Direccion;
                        actualizaralumno.DNI = DNI;
                        actualizaralumno.Email = Email;
                        actualizaralumno.Carrera = _context.Carrera.FirstOrDefault(c => c.CarreraId == CarreraId);
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
        }

        return Json(new { success = result }); // Devuelve un resultado JSON indicando si la operación fue exitosa o no.
    }
    public JsonResult EliminarAlumno(int AlumnoId, int Eliminar)
    {
        int result = 0;
        var Alumno1 = _context.Alumno.Find(AlumnoId);

        if (Alumno1 != null)
        {
            if (Eliminar == 0)
            {
                Alumno1.Eliminar = false;
                _context.SaveChanges();
            }
            else if (Eliminar == 1)
            {
                _context.Remove(Alumno1);
                _context.SaveChanges();
            }

            result = 1;
        }

        return Json(result);
    }

}