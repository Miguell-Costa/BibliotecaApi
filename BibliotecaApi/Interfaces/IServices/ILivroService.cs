using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface ILivroService
	{
		Task<Result<LivroDto>> CriarLivro(CriarLivroRequest request);
		Task<Result<List<LivroDto>>> ListarLivros();
		Task<Result<LivroDto>> ListarLivroPorId(int id);
		Task<Result<LivroDto>> ListarLivroPorISBN(string ISBN);
		Task<Result<LivroDto>> AtualizarLivro(int id, AtualizarLivroRequest request);
		Task<Result<MessageResponseDto>> ApagarLivro(int id);
	}
}
