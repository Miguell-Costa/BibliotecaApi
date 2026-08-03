using BibliotecaApi.Data;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repository
{
	public class LivroRepository : ILivroRepository
	{
		private readonly AppDbContext _context;

		public LivroRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Livro?> GetByISBN(string ISBN)
		{
			var livro = await _context.Livros
				.FirstOrDefaultAsync(l => l.ISBN == ISBN);

			return livro;
		}

		public async Task<Livro> AddAsync(Livro request)
		{
			_context.Livros.Add(request);
			await _context.SaveChangesAsync();

			return request;
		}

		public async Task<List<Livro>> GetLivrosAsync()
		{
			return await _context.Livros.ToListAsync();
		}

		public async Task<Livro> GetLivroByIdAsync(int id)
		{
			var livro = await _context.Livros
				.FirstOrDefaultAsync(l => l.Id == id);

			return livro;
		}
	
		public async Task<Livro> UpdateLivroAsync(AtualizarLivroRequest request, Livro livro)
		{
			_context.Entry(livro).CurrentValues.SetValues(request);
			await _context.SaveChangesAsync();

			return livro;
		}
	
		public async Task<Livro> ApagarLivroAsync(Livro livro)
		{
			_context.Livros.Remove(livro);
			await _context.SaveChangesAsync();

			return livro;
		}
	}
}
