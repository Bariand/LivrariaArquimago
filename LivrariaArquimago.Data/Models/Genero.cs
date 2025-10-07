using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LivrariaArquimago.Data.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}