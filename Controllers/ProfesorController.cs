using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto2.Data;
using Microsoft.AspNetCore.Authorization;
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
        var profesor = _context.Profesor.ToList();
        return View();
    }
    public JsonResult BuscarProfesor(int ProfesorId = 0)
    {
        var profesor = _context.Profesor.ToList();
        if (ProfesorId > 0)
        {
            profesor = profesor.Where(p => p.ProfesorId == ProfesorId).OrderBy(p => p.Nombre).ToList();
        }
        return Json(profesor);
    }
    public JsonResult GuardarProfesor(int ProfesorId, string Nombre, string DNI, DateTime FechaNacimiento, string Direccion, string Email)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(Nombre))
        {
            var profesorExistente = _context.Profesor.FirstOrDefault(p => p.Nombre == Nombre);
            if (profesorExistente == null)
            {
                var guardarprofe = new Profesor
                {
                    Nombre = Nombre,
                    DNI = DNI,
                    Direccion = Direccion,
                    FechaNacimiento = FechaNacimiento,
                    Email = Email
                };
                _context.Add(guardarprofe);
                _context.SaveChanges();
                result = true;
            }
            else
            {
                var profesoresExistente = _context.Profesor.FirstOrDefault(p => p.Nombre == Nombre && p.ProfesorId != ProfesorId);
                if (profesoresExistente == null)
                {
                    var editarprofe = _context.Profesor.Find(ProfesorId);
                    if (editarprofe != null)
                    {
                        editarprofe.Nombre = Nombre;
                        editarprofe.DNI = DNI;
                        editarprofe.Direccion = Direccion;
                        editarprofe.FechaNacimiento = FechaNacimiento;
                        editarprofe.Email = Email;
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