using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Custo { get; set; }

        public DateTime DataLimite { get; set; }

        [Required]
        public int Ordem { get; set; }
    }
}
