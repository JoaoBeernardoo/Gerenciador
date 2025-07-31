namespace Api.Dtos
{
    public class UpdateTarefaDto
    {
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public DateTime DataLimite { get; set; }
    }
}