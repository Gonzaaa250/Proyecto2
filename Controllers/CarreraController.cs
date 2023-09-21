using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto2.Data;
using Microsoft.AspNetCore.Authorization;
using Proyecto2.Models;
namespace Proyecto2.Controllers;
[Authorize]
public class CarreraController : Controller
{
    private readonly ILogger<CarreraController> _logger;
    public readonly ApplicationDbContext _context;
    public CarreraController(ILogger<CarreraController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var carrera = _context.Carrera.ToList();
        return View();
    }
    public JsonResult BuscarCarrera(int CarreraId = 0)
    {
        var carrera = _context.Carrera.ToList();
        if (CarreraId > 0)
        {
            carrera = carrera.Where(c => c.CarreraId == CarreraId).OrderBy(c => c.NombreC).ToList();
        }
        return Json(carrera);
    }
    public JsonResult GuardarCarrera(int CarreraId, string NombreC, int Duracion)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(NombreC))
        {
            var carreraExistente = _context.Carrera.FirstOrDefault(c => c.NombreC == NombreC);
            if (carreraExistente == null)
            {
                var guardarcarrera = new Carrera
                {
                    NombreC = NombreC,
                    Duracion = Duracion
                };
                _context.Add(guardarcarrera);
                _context.SaveChanges();
                result = true;
            }
            else
            {
                var carrerasExistente = _context.Carrera.FirstOrDefault(c => c.NombreC == NombreC && c.CarreraId != CarreraId);
                if (carrerasExistente == null)
                {
                    var carreraeditar = _context.Carrera.Find(CarreraId);

                    if (carreraeditar != null)
                    {
                        carreraeditar.NombreC = NombreC;
                        carreraeditar.Duracion = Duracion;

                        _context.SaveChanges();
                        result = true;
                    }
                }

            }
        }
        return Json(result);
    }
    public JsonResult EliminarCarrera(int CarreraId, int Eliminar)
    {
        int result = 0;
        var carrera = _context.Carrera.Find(CarreraId);

        if (carrera != null)
        {
            if (Eliminar == 0)
            {
                carrera.Eliminar = false;
                _context.SaveChanges();
            }

            else
            {
                if (Eliminar == 1)
                {
                    carrera.Eliminar = true;
                    _context.Remove(carrera);
                    _context.SaveChanges();
                }
            }
        }
        result = 1;

        return Json(result);
    }
}