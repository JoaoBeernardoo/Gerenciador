using Api.Dtos;
namespace Api.Interfaces.Services
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaDto>> GetAllAsync();
        Task<TarefaDto?> GetByIdAsync(int id);
        Task<string?> CreateAsync(CreateTarefaDto dto);
        Task<bool> UpdateAsync(int id, UpdateTarefaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}