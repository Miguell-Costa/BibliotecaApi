using System.Reflection.Metadata;
using BibliotecaApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Livro> Livros { get; set; }
		public DbSet<Autor> Autores { get; set; }
		public DbSet<Categoria> Categorias { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Autor>()
				.HasMany(e => e.Livros)
				.WithOne(e => e.Autor)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Categoria>()
				.HasMany(e => e.Livros)
				.WithOne(e => e.Categoria)
				.OnDelete(DeleteBehavior.Restrict);

			// ------- Livro ---------
			modelBuilder.Entity<Livro>()
				.HasIndex(e => e.ISBN).IsUnique();

			modelBuilder.Entity<Livro>()
				.Property(l => l.ISBN)
				.HasMaxLength(20)
				.IsRequired();

			modelBuilder.Entity<Livro>()
				.Property(l => l.Descricao)
				.HasMaxLength(2000);

			modelBuilder.Entity<Livro>()
				.Property(l => l.Titulo)
				.HasMaxLength(300)
				.IsRequired();

			modelBuilder.Entity<Livro>()
				.Property(l => l.CapaUrl)
				.HasMaxLength(500);

			// ------- Autor --------
			modelBuilder.Entity<Autor>()
				.Property(l => l.Nome)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<Autor>()
				.Property(l => l.Biografia)
				.HasMaxLength(1000);

			modelBuilder.Entity<Autor>()
				.Property(l => l.OpenLibraryId)
				.HasMaxLength(50);

			// -------- Categoria -------
			modelBuilder.Entity<Categoria>()
				.Property(l => l.Nome)
				.HasMaxLength(100)
				.IsRequired();
		}
	}
}
