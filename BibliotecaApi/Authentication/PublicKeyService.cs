using BibliotecaApi.Interfaces;
using BibliotecaApi.Model;

namespace BibliotecaApi.Authentication
{
	public class PublicKeyService : IPublicKeyService
	{
		private readonly HttpClient _httpClient;
		
		public PublicKeyService(HttpClient httpClient) 
		{
			_httpClient = httpClient;
		}

		public async Task<Result<string>> GetPublicKeyAsync()
		{
			string keyString;
			try
			{
				var key = await _httpClient.GetAsync("key/public-key");
				keyString = await key.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				var error = ex.Message;

				return Result<string>.Failure(new List<String>{ error});
			}

			return Result<string>.Success(keyString);
		}
	}
}
