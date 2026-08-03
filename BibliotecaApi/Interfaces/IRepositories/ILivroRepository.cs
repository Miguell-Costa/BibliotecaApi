using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface ILivroRepository
	{
		Task<Livro?> GetByISBN(string ISBN);
		Task<Livro> AddAsync(Livro request);
		Task<List<Livro>> GetLivrosAsync();
		Task<Livro> GetLivroByIdAsync(int id);
		Task<Livro> UpdateLivroAsync(AtualizarLivroRequest request, Livro livro);
		Task<Livro> ApagarLivroAsync(Livro livro);
	}
}
