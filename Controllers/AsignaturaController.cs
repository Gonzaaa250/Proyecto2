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
public class AsignaturaController : Controller
{
    private readonly ILogger<AsignaturaController> _logger;
    private readonly ApplicationDbContext _context;
    public AsignaturaController(ILogger<AsignaturaController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var carreras = _context.Carrera?.ToList();
        ViewBag.CarreraId = new SelectList(carreras, "CarreraId", "NombreC");
        return View();
    }
    public JsonResult BuscarAsignatura(int AsignaturaId = 0)
    {
        var asignaturas = _context.Asignatura.ToList();
        if (AsignaturaId > 0)
        {
            asignaturas = asignaturas.Where(a => a.AsignaturaId == AsignaturaId).ToList();
        }
        asignaturas = asignaturas.OrderBy(a => a.NombreA).ToList();
        return Json(asignaturas);
    }
    public JsonResult GuardarAsignatura(int AsignaturaId, string NombreA, int CarreaId)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(NombreA))
        {
            if (AsignaturaId == 0)
            {
                var asignaturaExistente = _context.Asignatura.FirstOrDefault(a => a.NombreA == NombreA);
                if (asignaturaExistente != null)
                {
                    var carrera = _context.Carrera.FirstOrDefault(c => c.CarreraId == CarreaId);
                    if (carrera != null)
                    {
                        var guardarasignatura = new Asignatura
                        {
                            NombreA = NombreA,
                            Carrera = carrera
                        };
                        _context.Add(guardarasignatura);
                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
            else
            {
                var asignaturaExistente = _context.Asignatura.FirstOrDefault(a => a.NombreA == NombreA && a.AsignaturaId != AsignaturaId);
                if (asignaturaExistente == null)
                {
                    var actualizarasignatura = _context.Asignatura.Find(AsignaturaId);
                    if (actualizarasignatura != null)
                    {
                        actualizarasignatura.NombreA = NombreA;
                        actualizarasignatura.Carrera = _context.Carrera.FirstOrDefault(c => c.CarreraId == CarreaId);

                        _context.SaveChanges();
                        result = true;
                    }
                }
            }
        }
        return Json(result);
    }
    public JsonResult EliminarAsignatura(int AsignaturaId, int Eliminar)
    {
        int result = 0;
        var asignatura = _context.Asignatura.Find(AsignaturaId);
        if (asignatura != null)
        {
            if (Eliminar == 0)
            {
                asignatura.Eliminar = false;
                _context.SaveChanges();
            }
            else
            {
                if (Eliminar == 1)
                {
                    asignatura.Eliminar = true;
                    _context.Remove(asignatura);
                    _context.SaveChanges();
                }
            }
        }
        result = 1;
        return Json(result);
    }
}