using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface ILivroRepository
	{
		Task<Livro?> GetByISBN(string ISBN);
		Task<Livro> AddAsync(Livro request);
	}
}
