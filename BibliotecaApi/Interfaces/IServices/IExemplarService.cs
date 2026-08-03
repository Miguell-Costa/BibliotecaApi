using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Exemplar;

namespace BibliotecaApi.Interfaces.IServices
{
	public interface IExemplarService
	{
		Task<Result<ExemplarDto>> CriarExemplar(CriarExemplarRequest request);
		Task<Result<List<ExemplarDto>>> ListarExemplares();
		Task<Result<List<ExemplarDto>>> ListarExemparesPorLivro(int id);
		Task<Result<ExemplarDto>> GetById(int id);
		Task<Result<MessageResponseDto>> ApagarExemplar(int id);
		Task<Result<ExemplarDto>> AtualizarExemplar(int id, AtualizarExemplarRequest request);
	}
}
