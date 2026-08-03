using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface IAutorRepository
	{
		Task<Autor> GetById(int autorId);
		Task<Autor> GetByOpenLibraryId(string OpenLibraryId);
		Task<Autor> AddAsync(Autor autor);
		Task<List<Autor>> GetAutoresAsync();
		Task<Autor> ApagarAsync(Autor autor);
		Task<bool> TemLivrosAssociados(int id);
		Task<Autor> AtualizarAutorAsync(CriarAutorRequest request, Autor autor);
	}
}
