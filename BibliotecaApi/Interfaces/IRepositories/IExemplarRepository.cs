using BibliotecaApi.Model.Dtos.Exemplar;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface IExemplarRepository
	{
		Task<Exemplar> AddAsync(Exemplar exemplar);
		Task<List<Exemplar>> GetExemplares();
		Task<List<Exemplar>> GetExemplaresPorLivroAsync(int id);
		Task<Exemplar> GetByIdAsync(int id);
		Task<Exemplar> ApagarExemplarAsync(Exemplar exemplar);
		Task<Exemplar> AtualizarExemplarAsync(AtualizarExemplarRequest dto, Exemplar exemplar);
	}
}
