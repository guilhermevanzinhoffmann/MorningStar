using Microsoft.AspNetCore.Mvc;
using MorningStar.Models;
using MorningStar.Services.Interfaces;

namespace MorningStar.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IFabricanteService _service;

        public FabricantesController(IFabricanteService service)
        {
            _service= service;
        }

        // GET: Fabricantes
        public async Task<IActionResult> Index()
        {
              return View(await _service.GetAllAsync());
        }

        // GET: Fabricantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var fabricante = await _service.GetByIdAsync(id.Value);
            
            if(fabricante == null)
                return NotFound();

            return View(fabricante);
        }

        // GET: Fabricantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(fabricante);
                return RedirectToAction(nameof(Index));
            }
            return View(fabricante);
        }
    }
}
