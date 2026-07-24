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

		public async Task<LivroDto?> GetByISBN(string ISBN)
		{
			var livro = await _context.Livros
				.FirstOrDefaultAsync(l => l.ISBN == ISBN);

			return livro.ToLivroDto();
		}

		public async Task<Livro> AddAsync(Livro request)
		{
			_context.Livros.Add(request);
			await _context.SaveChangesAsync();

			return request;
		}
	}
}
