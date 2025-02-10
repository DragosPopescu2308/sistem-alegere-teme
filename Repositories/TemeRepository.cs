using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Repositories
{
    public class TemeRepository : ITemeRepository
    {
        private readonly MyAppContext _context;

        public TemeRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task<List<DificultyLevel>> GetDificultyLevels()
        {
            return await _context.DificultyLevels.ToListAsync();
        }

        public async Task<List<Item>> GetTemeDisponibileAsync()
        {
            return await _context.Items.Include(i => i.DificultyLevel).ToListAsync();
        }

        public async Task<List<TemaAleasa>> GetTemeAleseAsync(string userEmail)
        {
            return await _context.TemeAlese.Where(t => t.UserEmail == userEmail).ToListAsync();
        }

        public async Task<Item?> GetTemaByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddTemaAsync(Item tema)
        {
            _context.Items.Add(tema);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTemaAsync(Item tema)
        {
            _context.Update(tema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTemaAsync(int id)
        {
            var tema = await _context.Items.FindAsync(id);
            if (tema != null)
            {
                _context.Items.Remove(tema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TemaEsteAleasaAsync(int idTema, string userEmail)
        {
            return await _context.TemeAlese.AnyAsync(t => t.IdTema == idTema && t.UserEmail == userEmail);
        }

        public async Task AddTemaAleasaAsync(TemaAleasa temaAleasa)
        {
            _context.TemeAlese.Add(temaAleasa);
            await _context.SaveChangesAsync();
        }
    }
}
