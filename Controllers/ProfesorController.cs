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
        var profesores=_context.Profesor.ToList();
        return View();
    }
    public JsonResult BuscarProfesor( int ProfesorId = 0)
    {
        var profesores=_context.Profesor.ToList();
        if(ProfesorId >0)
        {
            profesores = profesores.Where(p => p.ProfesorId == ProfesorId).OrderBy(p => p.NombreP).ToList();
        }
        return Json(profesores);
    }
    public JsonResult GuardarProfesor( int ProfesorId, string NombreP, string DNI, DateTime FechaNacimiento, string Direccion, string Email)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(NombreP) && !string.IsNullOrEmpty(DNI))
        {
            var profreExistente = _context.Profesor.FirstOrDefault(p => p.NombreP == NombreP);
            if (profreExistente == null )
            {
                var guardarprofe = new Profesor
                {
                    NombreP = NombreP,
                    DNI=DNI,
                    FechaNacimiento = FechaNacimiento,
                    Direccion = Direccion,
                    Email = Email
                };
                _context.Add(guardarprofe);
                _context.SaveChanges();
                result = true;
            }
            else
            {
                var profresExistente = _context.Profesor.FirstOrDefault(p => p.NombreP == NombreP && p.ProfesorId != ProfesorId);
                if(profresExistente == null)
                {
                    var profeeditar = _context.Profesor.Find(ProfesorId);
                    if (profeeditar != null)
                    {
                        profeeditar.NombreP = NombreP;
                        profeeditar.DNI = DNI;
                        profeeditar.FechaNacimiento = FechaNacimiento;
                        profeeditar.Direccion = Direccion;
                        profeeditar.Email = Email;
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
        }
        return Json(result);
    }
    public JsonResult EliminarProfesor (int ProfesorId, int Eliminar)
    {
        int result =0;
        var profesor = _context.Profesor.Find(ProfesorId);
        if (profesor != null)
        {
            if(Eliminar == 0)
            {
                profesor.Eliminar = false;
                _context.SaveChanges();
            }
            else
            {
                if(Eliminar ==1)
                {
                    profesor.Eliminar = true;
                    _context.Remove(profesor);
                    _context.SaveChanges();
                }
            }
        }
        result =1;
        return Json(result);
    }
}