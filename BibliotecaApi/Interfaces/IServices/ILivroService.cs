using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface ILivroService
	{
		Task<Result<LivroDto>> CriarLivro(CriarLivroRequest request);
	}
}
