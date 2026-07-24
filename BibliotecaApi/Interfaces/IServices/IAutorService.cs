using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Autor;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface IAutorService
	{
		Task<Result<AutorDto>> CreateAutor(CriarAutorRequest request);
	}
}
