using Api.Dtos;
using Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetAll()
        {
            var tarefas = await _tarefaService.GetAllAsync();
            return Ok(tarefas);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaDto>> GetById(int id)
        {
            var tarefa = await _tarefaService.GetByIdAsync(id);
            if (tarefa == null) return NotFound("Tarefa não encontrada.");
            return Ok(tarefa);
        }

       
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTarefaDto dto)
        {
            var erro = await _tarefaService.CreateAsync(dto);
            if (erro != null) return BadRequest(erro); 

            return CreatedAtAction(nameof(GetById), new { id = dto.Nome }, dto);
        }

      
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTarefaDto dto)
        {
            var sucesso = await _tarefaService.UpdateAsync(id, dto);
            if (!sucesso) return NotFound("Tarefa não encontrada ou nome já está em uso.");
            
            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sucesso = await _tarefaService.DeleteAsync(id);
            if (!sucesso) return NotFound("Tarefa não encontrada.");
            
            return NoContent(); 
        }
    }
}
