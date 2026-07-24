using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface ICategoriaService
	{
		Task<Result<CategoriaDto>> CreateCategoria(CriarCategoriaRequest request);
	}
}
