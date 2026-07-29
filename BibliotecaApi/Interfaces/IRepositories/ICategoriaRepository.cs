using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface ICategoriaRepository
	{
		Task<Categoria> GetById(int CategoriaId);
		Task<Categoria> GetByNome(string Nome);
		Task<CategoriaDto> AddAsync(Categoria categoria);
		Task<Categoria> ApagarAsync(Categoria categoria);
		Task<bool> TemLivrosAssociadosAsync(int id);
		Task<List<Categoria>> GetLivrosAsync();
		Task<Categoria> AtualizarCategoriaAsync(AtualizarCategoriaRequest dto, Categoria categoria);
	}
}
