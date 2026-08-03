using BibliotecaApi.Data;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Model.Dtos.Exemplar;
using BibliotecaApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repository
{
	public class ExemplarRepository: IExemplarRepository
	{
		private readonly AppDbContext _context;

		public ExemplarRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Exemplar> AddAsync(Exemplar exemplar)
		{
			_context.Exemplares.Add(exemplar);
			await _context.SaveChangesAsync();

			return exemplar;
		}

		public async Task<List<Exemplar>> GetExemplares()
		{
			return await _context.Exemplares.ToListAsync();
		}

		public async Task<List<Exemplar>> GetExemplaresPorLivroAsync(int id)
		{
			return await _context.Exemplares.Where(e => e.LivroId == id).ToListAsync();
		}

		public async Task<Exemplar> GetByIdAsync(int id)
		{
			return await _context.Exemplares.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<Exemplar> ApagarExemplarAsync(Exemplar exemplar)
		{
			_context.Exemplares.Remove(exemplar);
			await _context.SaveChangesAsync();

			return exemplar;
		}

		public async Task<Exemplar> AtualizarExemplarAsync(AtualizarExemplarRequest dto, Exemplar exemplar)
		{
			_context.Exemplares.Entry(exemplar).CurrentValues.SetValues(dto);
			await _context.SaveChangesAsync();

			return exemplar;
		}
	}
}
