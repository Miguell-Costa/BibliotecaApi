using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface ICategoriaRepository
	{
		Task<CategoriaDto?> GetById(int CategoriaId);
		Task<Categoria> GetByNome(string Nome);
		Task<CategoriaDto> AddAsync(Categoria categoria);
	}
}
