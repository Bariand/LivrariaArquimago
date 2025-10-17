using LivrariaArquimago.Data.Context;
using LivrariaArquimago.Data.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("CorsPolicy");

SeedData.Initialize(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedData.Initialize(app.Services);

app.Run();

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LivrariaArquimago.Data.Context.AppDbContext>();
            context.Database.EnsureCreated();

            if (context.Generos.Any()) return;

            var grimoriosMagia = new LivrariaArquimago.Data.Models.Genero { Nome = "Grimórios e Magia (Fantasia Épica)" };
            var lendasMitologia = new LivrariaArquimago.Data.Models.Genero { Nome = "Lendas e Mitologia (Clássicos/História)" };
            var aventurasCosmicas = new LivrariaArquimago.Data.Models.Genero { Nome = "Aventuras Cósmicas (Ficção Científica)" };

            context.Generos.AddRange(grimoriosMagia, lendasMitologia, aventurasCosmicas);
            context.SaveChanges();

            var livros = new List<LivrariaArquimago.Data.Models.Livro>
            {
                // Grimórios e Magia
                new Livro { Titulo = "O Senhor dos Anéis: A Sociedade do Anel", Autor = "J.R.R. Tolkien", Preco = 89.90M, GeneroId = grimoriosMagia.Id },
                new Livro { Titulo = "Harry Potter e a Pedra Filosofal", Autor = "J.K. Rowling", Preco = 59.90M, GeneroId = grimoriosMagia.Id },
                new Livro { Titulo = "A Guerra dos Tronos", Autor = "George R. R. Martin", Preco = 95.00M, GeneroId = grimoriosMagia.Id },
                new Livro { Titulo = "Eragon (O Ciclo da Herança)", Autor = "Christopher Paolini", Preco = 69.50M, GeneroId = grimoriosMagia.Id },

                // Lendas e Mitologia
                new Livro { Titulo = "Mitologia Nórdica", Autor = "Neil Gaiman", Preco = 55.00M, GeneroId = lendasMitologia.Id },
                new Livro { Titulo = "A Ilíada", Autor = "Homero", Preco = 32.00M, GeneroId = lendasMitologia.Id },
                new Livro { Titulo = "Contos de Fadas dos Irmãos Grimm", Autor = "Irmãos Grimm", Preco = 45.00M, GeneroId = lendasMitologia.Id },
                new Livro { Titulo = "As Brumas de Avalon", Autor = "Marion Zimmer Bradley", Preco = 79.90M, GeneroId = lendasMitologia.Id },

                // Aventuras Cósmicas
                new Livro { Titulo = "Duna", Autor = "Frank Herbert", Preco = 59.90M, GeneroId = aventurasCosmicas.Id },
                new Livro { Titulo = "Dagon", Autor = "H.P. Lovecraft", Preco = 35.00M, GeneroId = aventurasCosmicas.Id },
                new Livro { Titulo = "Neuromancer", Autor = "William Gibson", Preco = 62.50M, GeneroId = aventurasCosmicas.Id },
                new Livro { Titulo = "Fundação", Autor = "Isaac Asimov", Preco = 71.00M, GeneroId = aventurasCosmicas.Id }
            };

            context.Livros.AddRange(livros);
            context.SaveChanges();
        }
    }
}