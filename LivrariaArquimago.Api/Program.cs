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
                new LivrariaArquimago.Data.Models.Livro { Titulo = "Duna", Autor = "Frank Herbert", Preco = 59.90M, GeneroId = aventurasCosmicas.Id }
            };

            context.Livros.AddRange(livros);
            context.SaveChanges();
        }
    }
}