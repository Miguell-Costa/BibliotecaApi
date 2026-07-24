using BibliotecaApi.Model;

namespace BibliotecaApi.Interfaces
{
	public interface IPublicKeyService
	{
		Task<Result<string>> GetPublicKeyAsync();
	}
}
