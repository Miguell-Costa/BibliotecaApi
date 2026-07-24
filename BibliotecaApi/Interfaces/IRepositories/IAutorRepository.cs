using BibliotecaApi.Model.Dtos.Autor;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface IAutorRepository
	{
		Task<AutorDto?> GetById(int autorId);
	}
}
