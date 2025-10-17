using LivrariaArquimago.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class ProdutosController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public ProdutosController(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _apiUrl = config["ApiSettings:BaseUrl"] + "api/Livros/";
    }

    public async Task<IActionResult> Index()
    {
        var responseGeneros = await _httpClient.GetAsync(_apiUrl + "Generos");
        if (!responseGeneros.IsSuccessStatusCode) return View("Error");

        var jsonGeneros = await responseGeneros.Content.ReadAsStringAsync();
        var generos = JsonSerializer.Deserialize<List<Genero>>(jsonGeneros, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (generos != null)
        {
            foreach (var genero in generos)
            {
                var responseLivros = await _httpClient.GetAsync(_apiUrl + $"PorGenero/{genero.Id}");
                if (responseLivros.IsSuccessStatusCode)
                {
                    var jsonLivros = await responseLivros.Content.ReadAsStringAsync();
                    var livros = JsonSerializer.Deserialize<List<Livro>>(jsonLivros, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    genero.Livros = livros;
                }
            }
        }
        return View(generos);
    }
}