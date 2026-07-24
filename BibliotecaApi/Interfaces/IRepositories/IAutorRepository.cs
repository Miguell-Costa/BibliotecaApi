using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Interfaces.IRepositories
{
	public interface IAutorRepository
	{
		Task<Autor> GetById(int autorId);
		Task<Autor> GetByOpenLibraryId(string OpenLibraryId);
		Task<Autor> AddAsync(Autor autor);
	}
}
