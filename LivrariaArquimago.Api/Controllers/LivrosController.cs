using LivrariaArquimago.Data.Context;
using LivrariaArquimago.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class LivrosController : ControllerBase
{
    private readonly AppDbContext _context;

    public LivrosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Livros/Generos (Retorna as Linhas de Produtos)
    [HttpGet("Generos")]
    public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
    {
        return await _context.Generos.ToListAsync();
    }

    // GET: api/Livros/PorGenero/1 (Retorna Livros por ID do Gênero)
    [HttpGet("PorGenero/{id}")]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivrosPorGenero(int id)
    {
        var livros = await _context.Livros
                                    .Where(l => l.GeneroId == id)
                                    .Include(l => l.Genero)
                                    .ToListAsync();

        if (livros == null || !livros.Any())
        {
            return NotFound();
        }
        return livros;
    }
}