using Microsoft.EntityFrameworkCore;
using MorningStar.Data;
using MorningStar.Models;
using MorningStar.Repositories.Interfaces;

namespace MorningStar.Repositories
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly MorningStarContext _context;
        
        public FabricanteRepository(MorningStarContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Fabricante fabricante)
        {
            _context.Add(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fabricante>> GetAllAsync()
        {
            if (_context.Fabricantes == null)
                return null;

            return await _context.Fabricantes.ToListAsync();
        }

        public IEnumerable<Fabricante> GetAll()
        {
            if (_context.Fabricantes == null)
                return null;

            return _context.Fabricantes.ToList();
        }

        public async Task<Fabricante> GetByIdAsync(int id)
        {
            if (_context.Fabricantes == null)
                return null;
            
            return await _context.Fabricantes.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
