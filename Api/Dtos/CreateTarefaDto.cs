namespace Api.Dtos
{
    public class CreateTarefaDto
    {
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public DateTime DataLimite { get; set; }
    }
}
