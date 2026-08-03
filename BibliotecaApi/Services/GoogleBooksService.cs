using System.Text.Json;
using BibliotecaApi.Interfaces;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.GoogleBook;
using BibliotecaApi.Model.Dtos.Livro;

namespace BibliotecaApi.Services
{
	public class GoogleBooksService: IGoogleBooksService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public GoogleBooksService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_apiKey = configuration["GoogleBooks:ApiKey"];
		}

		public async Task<GoogleBookResponse> GetBookByIsbnAsync(string isbn)
		{
			var response = await _httpClient.GetAsync($"volumes?q=isbn:{isbn}&key={_apiKey}");

			var json = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer.Deserialize<GoogleBookResponse>(
			json,
			new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result;

		}
	}
}
