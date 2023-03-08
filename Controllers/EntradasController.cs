using Chart.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorningStar.Models;
using MorningStar.Services.Interfaces;
using PagedList;
using Rotativa.AspNetCore;

namespace MorningStar.Controllers
{
    public class EntradasController : Controller
    {
        private readonly IEntradaService _service;
        private readonly IMercadoriaService _mercadoriaService;
        private readonly IGraficoService _graficoService;

        public EntradasController(IEntradaService service, IMercadoriaService mercadoriaService, IGraficoService graficoService)
        {
            _service = service;
            _mercadoriaService = mercadoriaService;
            _graficoService = graficoService;
        }

        // GET: Entradas
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Entradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var entrada = await _service.GetByIdAsync(id.Value);

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entradas/Create
        public IActionResult Create()
        {
            ViewData["Mercadoria"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Nome");
            return View();
        }

        public IActionResult MoreDetails()
        {
            var meses = new List<string>
            {
                "JANEIRO",
                "FEVEREIRO",
                "MARÇO",
                "ABRIL",
                "MAIO",
                "JUNHO",
                "JULHO",
                "AGOSTO",
                "SETEMBRO",
                "OUTUBRO",
                "NOVEMBRO",
                "DEZEMBRO"
            };
            ViewData["Meses"] = new SelectList(meses);
            ViewData["Mercadorias"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Nome");
            return View();
        }

        public IActionResult GetGrafico(int mercadoriaID, int mes)
        {
            var entradasDia = _graficoService.GetEntradasDia(mercadoriaID, mes);
            return StatusCode(StatusCodes.Status200OK, entradasDia.ToJson());
        }

        public IActionResult GetGraficoMes(int mercadoriaID)
        {
            var entradasMes = _graficoService.GetEntradasMes(mercadoriaID);
            return StatusCode(StatusCodes.Status200OK, entradasMes.ToJson());
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHoraEntrada,Local,Quantidade,MercadoriaID")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(entrada);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", entrada.MercadoriaID);
            return View(entrada);
        }

        // GET: Entradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var entrada = await _service.GetByIdAsync(id.Value);

            if (entrada == null)
                return NotFound();
            
            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", entrada.MercadoriaID);
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHoraEntrada,Local,Quantidade,MercadoriaID")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(entrada);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", entrada.MercadoriaID);
            return View(entrada);
        }

        // GET: Entradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var entrada = await _service.GetByIdAsync(id.Value);

            if (entrada == null)
                return NotFound();

            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrada = await _service.GetByIdAsync(id);

            if (entrada != null)
                await _service.DeleteAsync(entrada.Id);
            
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Relatorio(int? pagina, bool? pdf)
        {
            var entradas = _service.GetAll().OrderBy(x => x.DataHoraEntrada).ToList();

            if (pdf != true)
            {
                int numeroRegistros = 5;
                int numeroPagina = (pagina ?? 1);
                return View(entradas.ToPagedList(numeroPagina, numeroRegistros));
            }
            else
            {
                int pagNumero = 1;

                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "Relatorio",
                    IsGrayScale = true,
                    Model = entradas.ToPagedList(pagNumero, entradas.Count),
                    FileName = "relatorio.pdf",
                    
                };
                
                return relatorioPDF;
            }
        }

        private bool EntradaExists(int id)
            => _service.Exists(id);
    }
}