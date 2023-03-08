using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorningStar.Data;
using MorningStar.Models;
using MorningStar.Services.Interfaces;

namespace MorningStar.Controllers
{
    public class MercadoriasController : Controller
    {
        private readonly IMercadoriaService _service;
        private readonly IFabricanteService _fabricanteService;

        public MercadoriasController(IMercadoriaService service, IFabricanteService fabricanteService)
        {
            _service = service;
            _fabricanteService = fabricanteService;
        }

        // GET: Mercadorias
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Mercadorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var mercadoria = await _service.GetByIdAsync(id.Value);
            
            if (mercadoria == null)
            {
                return NotFound();
            }

            return View(mercadoria);
        }

        // GET: Mercadorias/Create
        public IActionResult Create()
        {
            ViewData["FabricanteID"] = new SelectList(_fabricanteService.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Mercadorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NumeroRegistro,FabricanteID,Tipo")] Mercadoria mercadoria)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(mercadoria);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FabricanteID"] = new SelectList(_fabricanteService.GetAll(), "Id", "Id", mercadoria.FabricanteID);
            return View(mercadoria);
        }

        // GET: Mercadorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var mercadoria = await _service.GetByIdAsync(id.Value);

            if (mercadoria == null)
                return NotFound();

            ViewData["FabricanteID"] = new SelectList(_fabricanteService.GetAll(), "Id", "Id", mercadoria.FabricanteID);
            return View(mercadoria);
        }

        // POST: Mercadorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NumeroRegistro,FabricanteID,Quantidade,Tipo")] Mercadoria mercadoria)
        {
            if (id != mercadoria.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(mercadoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MercadoriaExists(mercadoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    return View(ex.Message);
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["FabricanteID"] = new SelectList(_fabricanteService.GetAll(), "Id", "Id", mercadoria.FabricanteID);
            return View(mercadoria);
        }

        // GET: Mercadorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var mercadoria = await _service.GetByIdAsync(id.Value);
            
            if (mercadoria == null)
                return NotFound();

            return View(mercadoria);
        }

        // POST: Mercadorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mercadoria = await _service.GetByIdAsync(id);
            
            if (mercadoria != null)
                await _service.DeleteAsync(mercadoria.Id);
            
            return RedirectToAction(nameof(Index));
        }
        
        private bool MercadoriaExists(int id)
            => _service.Exists(id);
    }
}
