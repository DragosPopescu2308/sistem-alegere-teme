using MyApp.Models;

namespace MyApp.Repositories
{
    public interface ITemeRepository
    {
        Task<List<Item>> GetTemeDisponibileAsync();
        Task<List<TemaAleasa>> GetTemeAleseAsync(string userEmail);
        Task<Item?> GetTemaByIdAsync(int id);
        Task AddTemaAsync(Item tema);
        Task UpdateTemaAsync(Item tema);
        Task DeleteTemaAsync(int id);
        Task<bool> TemaEsteAleasaAsync(int idTema, string userEmail);
        Task AddTemaAleasaAsync(TemaAleasa temaAleasa);
        Task<List<DificultyLevel>> GetDificultyLevels();

    }
}
