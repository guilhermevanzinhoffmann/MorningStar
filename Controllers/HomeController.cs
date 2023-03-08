using Microsoft.AspNetCore.Mvc;
using MorningStar.Models;
using MorningStar.Services.Interfaces;
using NuGet.Protocol;
using System.Diagnostics;

namespace MorningStar.Controllers
{
    public class HomeController : Controller
    {

        private readonly IGraficoService _service;
        public HomeController(IGraficoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GraficoEntradas()
        {
            var entradas = _service.GetEntradas();
            return StatusCode(StatusCodes.Status200OK, entradas.ToJson());
        }

        public IActionResult GraficoSaidas()
        {
            var saidas = _service.GetSaidas();
            return StatusCode(StatusCodes.Status200OK, saidas.ToJson());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}