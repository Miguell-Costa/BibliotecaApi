using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface ICategoriaService
	{
		Task<Result<CategoriaDto>> CreateCategoria(CriarCategoriaRequest request);
		Task<Result<MessageResponseDto>> ApagarCategoria(int id);
		Task<Result<List<CategoriaDto>>> ListarCategorias();
		Task<Result<CategoriaDto>> AtualizarCategoria(int id, AtualizarCategoriaRequest dto);
		Task<Result<CategoriaDto>> GetById(int id);
	}
}
