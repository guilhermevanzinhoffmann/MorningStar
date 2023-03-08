using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningStar.Models;

namespace MorningStar.Controllers
{
    public class FabricanteController : Controller
    {
        // GET: FabricanteController
        public ActionResult Index()
        {
            Fabricante fabricante1 = new Fabricante();
            fabricante1.Id = 1;
            fabricante1.Nome = "Fabrica 1";

            Fabricante fabricante2 = new Fabricante();
            fabricante1.Id = 2;
            fabricante1.Nome = "Fabrica 2";

            var fabricantes = new List<Fabricante>
            {
                fabricante1,
                fabricante2
            };

            return View(fabricantes);
        }

        // GET: FabricanteController/Details/5
        public ActionResult Details(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        // GET: FabricanteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FabricanteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
