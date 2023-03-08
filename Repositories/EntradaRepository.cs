using Microsoft.EntityFrameworkCore;
using MorningStar.Data;
using MorningStar.Models;
using MorningStar.Repositories.Interfaces;

namespace MorningStar.Repositories
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly MorningStarContext _context;

        public EntradaRepository(MorningStarContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Entrada entrada)
        {
            _context.Add(entrada);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entrada>> GetAllAsync()
        {
            if (_context.Entradas == null)
                return null;

            return await _context.Entradas
                .Include(x => x.Mercadoria)
                .ToListAsync();
        }

        public IEnumerable<Entrada> GetAll()
        {
            if (_context.Entradas == null)
                return null;

            return _context.Entradas
                .Include(x => x.Mercadoria)
                .ToList();
        }

        public async Task<Entrada> GetByIdAsync(int id)
        {
            if (_context.Entradas == null)
                return null;

            return await _context.Entradas
                .Include(x => x.Mercadoria)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Exists(int id)
        {
            if (_context.Entradas == null)
                return false;

            return _context.Entradas.Any(x => x.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var entrada = await _context.Entradas
                .Include(x => x.Mercadoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entrada != null)
                _context.Entradas.Remove(entrada);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entrada entrada)
        {
            _context.Entradas.Update(entrada);
            await _context.SaveChangesAsync();
        }
    }
}
