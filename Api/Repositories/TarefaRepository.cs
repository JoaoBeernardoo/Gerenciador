using Api.Models;
using Api.Data;
using Api.Interfaces.Repositories; 
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tarefa>> GetAllAsync() =>
        await _context.Tarefas.OrderBy(t => t.Ordem).ToListAsync();

    public async Task<Tarefa?> GetByIdAsync(int id) =>
        await _context.Tarefas.FindAsync(id);

    public async Task<Tarefa?> GetByNomeAsync(string nome) =>
        await _context.Tarefas.FirstOrDefaultAsync(t => t.Nome == nome);

    public async Task AddAsync(Tarefa tarefa) =>
        await _context.Tarefas.AddAsync(tarefa);

    public void Update(Tarefa tarefa) =>
        _context.Tarefas.Update(tarefa);

    public void Delete(Tarefa tarefa) =>
        _context.Tarefas.Remove(tarefa);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}

}