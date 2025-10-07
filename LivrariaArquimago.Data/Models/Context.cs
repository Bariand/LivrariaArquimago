using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaArquimago.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaArquimago.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Genero)
                .WithMany(g => g.Livros)
                .HasForeignKey(l => l.GeneroId);

            base.OnModelCreating(modelBuilder);
        }
    }
}