using BibliotecaApi.Data;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repository
{
	public class CategoriaRepository: ICategoriaRepository
	{
		private readonly AppDbContext _context;

		public CategoriaRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Categoria> GetById(int CategoriaId)
		{
			var categoria = await _context.Categorias
				.FirstOrDefaultAsync(l => l.Id == CategoriaId);

			return categoria;
		}

		public async Task<Categoria> GetByNome(string Nome)
		{
			var categoria = await _context.Categorias
				.FirstOrDefaultAsync(l => l.Nome.Equals(Nome));

			return categoria;
		}

		public async Task<CategoriaDto> AddAsync(Categoria categoria)
		{
			_context.Categorias.Add(categoria);
			await _context.SaveChangesAsync();

			return categoria.ToCategoriaDto();
		}

		public async Task<Categoria> ApagarAsync(Categoria categoria)
		{
			_context.Categorias.Remove(categoria);
			await _context.SaveChangesAsync();

			return categoria;
		}
	
		public async Task<bool> TemLivrosAssociadosAsync(int id)
		{
			return await _context.Livros.AnyAsync(l => l.CategoriaId == id);;
		}

		public async Task<List<Categoria>> GetLivrosAsync()
		{
			return await _context.Categorias.ToListAsync();
		}
	
		public async Task<Categoria> AtualizarCategoriaAsync(AtualizarCategoriaRequest dto, Categoria categoria)
		{
			_context.Entry(categoria).CurrentValues.SetValues(dto);
			await _context.SaveChangesAsync();

			return categoria;
		}
		
	}
}
