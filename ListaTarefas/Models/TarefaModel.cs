using System.ComponentModel.DataAnnotations;

namespace ListaTarefas.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite o nome da tarefa!")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Digite a descricao da tarefa!")]
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
