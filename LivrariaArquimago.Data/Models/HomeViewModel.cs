using System.Collections.Generic;

namespace LivrariaArquimago.Data.Models
{
    // Este modelo será usado pela View da Home
    public class HomeViewModel
    {
        // Livros que vão no Banners ou Carrossel (Destaques)
        public List<Livro> Destaques { get; set; } = new List<Livro>();

        // Linhas de Produtos para exibir na Home (Ex: Fantasia, Ficção)
        public List<Genero> LinhasEmDestaque { get; set; } = new List<Genero>();
    }
}