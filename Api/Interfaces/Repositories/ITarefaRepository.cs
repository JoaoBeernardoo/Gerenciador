using Api.Models;
namespace Api.Interfaces.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetAllAsync();
        Task<Tarefa?> GetByIdAsync(int id);
        Task<Tarefa?> GetByNomeAsync(string nome);
        Task AddAsync(Tarefa tarefa);
        void Update(Tarefa tarefa);
        void Delete(Tarefa tarefa);
        Task SaveChangesAsync();
    }
}