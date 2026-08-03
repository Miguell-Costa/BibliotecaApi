using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Autor;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface IAutorService
	{
		Task<Result<AutorDto>> CreateAutor(CriarAutorRequest request);
		Task<Result<List<AutorDto>>> ListarAutores();
		Task<Result<MessageResponseDto>> ApagarAutor(int id);
		Task<Result<AutorDto>> ListarPorId(int id);
		Task<Result<AutorDto>> ListarPorOpenLibraryId(string id);
		Task<Result<AutorDto>> AtualizarAutor(int id, CriarAutorRequest request);
	}
}
