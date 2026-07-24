using BibliotecaApi.Data;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model.Dtos.Autor;
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

		public async Task<AutorDto?> GetById(int autorId)
		{
			var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == autorId);
			return autor.ToRoleDto();
		}
	}
}
