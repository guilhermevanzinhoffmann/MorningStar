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
    public class SaidasController : Controller
    {
        private readonly ISaidaService _service;
        private readonly IMercadoriaService _mercadoriaService;
        private readonly IGraficoService _graficoService;

        public SaidasController(ISaidaService service, IMercadoriaService mercadoriaService, IGraficoService graficoService)
        {
            _service = service;
            _mercadoriaService = mercadoriaService;
            _graficoService = graficoService;
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
            var saidasDia = _graficoService.GetSaidasDia(mercadoriaID, mes);
            return StatusCode(StatusCodes.Status200OK, saidasDia.ToJson());
        }

        public IActionResult GetGraficoMes(int mercadoriaID)
        {
            var saidasMes = _graficoService.GetSaidasMes(mercadoriaID);
            return StatusCode(StatusCodes.Status200OK, saidasMes.ToJson());
        }

        // GET: Saidas
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Saidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var saida = await _service.GetByIdAsync(id.Value);

            if (saida == null)
            {
                return NotFound();
            }

            return View(saida);
        }

        // GET: Saidas/Create
        public IActionResult Create()
        {
            ViewData["Mercadoria"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: Saidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHoraSaida,Local,Quantidade,MercadoriaID")] Saida saida)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(saida);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", saida.MercadoriaID);
            return View(saida);
        }

        // GET: Saidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var saida = await _service.GetByIdAsync(id.Value);

            if (saida == null)
                return NotFound();

            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", saida.MercadoriaID);
            return View(saida);
        }

        // POST: Saidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHoraSaida,Local,Quantidade,MercadoriaID")] Saida saida)
        {
            if (id != saida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(saida);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaidaExists(saida.Id))
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
            ViewData["MercadoriaID"] = new SelectList(_mercadoriaService.GetAll(), "Id", "Id", saida.MercadoriaID);
            return View(saida);
        }

        // GET: Saidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var saida = await _service.GetByIdAsync(id.Value);

            if (saida == null)
                return NotFound();

            return View(saida);
        }

        // POST: Saidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saida = await _service.GetByIdAsync(id);

            if (saida != null)
                await _service.DeleteAsync(saida.Id);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Relatorio(int? pagina, bool? pdf)
        {
            var saidas = _service.GetAll().OrderBy(x => x.DataHoraSaida).ToList();

            if (pdf != true)
            {
                int numeroRegistros = 5;
                int numeroPagina = (pagina ?? 1);
                return View(saidas.ToPagedList(numeroPagina, numeroRegistros));
            }
            else
            {
                int pagNumero = 1;

                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "Relatorio",
                    IsGrayScale = true,
                    Model = saidas.ToPagedList(pagNumero, saidas.Count),
                    FileName = "relatorio.pdf",

                };

                return relatorioPDF;
            }
        }
        private bool SaidaExists(int id)
            => _service.Exists(id);
    }
}
