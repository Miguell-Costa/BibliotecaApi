using BibliotecaApi.Data;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repository
{
	public class AutorRepository : IAutorRepository
	{
		private readonly AppDbContext _context;

		public AutorRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Autor> GetById(int autorId)
		{
			var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == autorId);
			return autor;
		}

		public async Task<Autor> GetByOpenLibraryId(string OpenLibraryId)
		{
			var autor = await _context.Autores.FirstOrDefaultAsync(a => a.OpenLibraryId.Equals(OpenLibraryId));
			return autor;
		}

		public async Task<Autor> AddAsync(Autor autor)
		{
			_context.Autores.Add(autor);
			await _context.SaveChangesAsync();

			return autor;
		}
	}
}
