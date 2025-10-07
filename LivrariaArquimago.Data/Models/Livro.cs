using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LivrariaArquimago.Data.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [StringLength(255)]
        public string Autor { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        [Range(0.01, 1000.00)]
        public decimal Preco { get; set; }

        public int GeneroId { get; set; }

        public Genero Genero { get; set; }
    }
}