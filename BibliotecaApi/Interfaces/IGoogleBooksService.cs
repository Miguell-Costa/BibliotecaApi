using BibliotecaApi.Model.Dtos.GoogleBook;

namespace BibliotecaApi.Interfaces
{
	public interface IGoogleBooksService
	{
		Task<GoogleBookResponse> GetBookByIsbnAsync(string isbn);
	}
}
