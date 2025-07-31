namespace Api.Dtos
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public DateTime DataLimite { get; set; }
    }
}