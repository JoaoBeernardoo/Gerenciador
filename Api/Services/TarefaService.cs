using Api.Dtos;
using Api.Models;
using Api.Interfaces.Repositories;
using Api.Interfaces.Services;

namespace Api.Services
{
public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _repository;

    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TarefaDto>> GetAllAsync()
    {
        var tarefas = await _repository.GetAllAsync();
        return tarefas.Select(t => new TarefaDto
        {
            Id = t.Id,
            Nome = t.Nome,
            Custo = t.Custo,
            DataLimite = t.DataLimite
        });
    }

    public async Task<TarefaDto?> GetByIdAsync(int id)
    {
        var tarefa = await _repository.GetByIdAsync(id);
        if (tarefa == null) return null;

        return new TarefaDto
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Custo = tarefa.Custo,
            DataLimite = tarefa.DataLimite
        };
    }

    public async Task<string?> CreateAsync(CreateTarefaDto dto)
    {
        
        var existente = await _repository.GetByNomeAsync(dto.Nome);
        if (existente != null) return "Já existe uma tarefa com esse nome.";

        
        var todas = await _repository.GetAllAsync();
        int novaOrdem = todas.Any() ? todas.Max(t => t.Ordem) + 1 : 1;

        var novaTarefa = new Tarefa
        {
            Nome = dto.Nome,
            Custo = dto.Custo,
            DataLimite = dto.DataLimite,
            Ordem = novaOrdem
        };

        await _repository.AddAsync(novaTarefa);
        await _repository.SaveChangesAsync();

        return null; 
    }

    public async Task<bool> UpdateAsync(int id, UpdateTarefaDto dto)
    {
        var tarefa = await _repository.GetByIdAsync(id);
        if (tarefa == null) return false;

       
        var duplicado = await _repository.GetByNomeAsync(dto.Nome);
        if (duplicado != null && duplicado.Id != id) return false;

        tarefa.Nome = dto.Nome;
        tarefa.Custo = dto.Custo;
        tarefa.DataLimite = dto.DataLimite;

        _repository.Update(tarefa);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tarefa = await _repository.GetByIdAsync(id);
        if (tarefa == null) return false;

        _repository.Delete(tarefa);
        await _repository.SaveChangesAsync();
        return true;
    }
}

}