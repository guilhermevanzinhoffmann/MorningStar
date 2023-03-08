using Microsoft.EntityFrameworkCore;
using MorningStar.Data;
using MorningStar.Models;
using MorningStar.Repositories.Interfaces;

namespace MorningStar.Repositories
{
    public class MercadoriaRepository : IMercadoriaRepository
    {
        private readonly MorningStarContext _context;

        public MercadoriaRepository(MorningStarContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Mercadoria mercadoria)
        {
            _context.Add(mercadoria);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mercadoria>> GetAllAsync()
        {
            if (_context.Mercadorias == null)
                return null;

            return await _context.Mercadorias
                .Include(x => x.Fabricante)
                .ToListAsync();
        }

        public IEnumerable<Mercadoria> GetAll()
        {
            if (_context.Mercadorias == null)
                return null;

            return _context.Mercadorias
                .Include(x => x.Fabricante)
                .ToList();
        }

        public async Task<Mercadoria> GetByIdAsync(int id)
        {
            if (_context.Mercadorias == null)
                return null;

            return await _context.Mercadorias
                .Include(x => x.Fabricante)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Exists(int id)
        {
            if (_context.Mercadorias == null)
                return false;

            return _context.Mercadorias.Any(x => x.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var mercadoria = await _context.Mercadorias
                .Include(x => x.Fabricante)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (mercadoria != null)
                _context.Mercadorias.Remove(mercadoria);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mercadoria mercadoria)
        {
            _context.Mercadorias.Update(mercadoria);
            await _context.SaveChangesAsync();
        }

        public bool Exists(string nome)
        {
            if (_context.Mercadorias == null)
                return false;

            return _context.Mercadorias.Any(x => x.Nome == nome);
        }
    }
}
