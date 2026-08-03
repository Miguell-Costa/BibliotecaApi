
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Dtos.Exemplar;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Services
{
	public class ExemplarService: IExemplarService
	{
		private readonly ILivroRepository _livroRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly ICategoriaRepository _categoriaRepository;
		private readonly IExemplarRepository _exemplarRepository;

		public ExemplarService(ILivroRepository livroRepository, IAutorRepository autorRepository, ICategoriaRepository categoriaRepository, IExemplarRepository exemplarRepository)
		{
			_livroRepository = livroRepository;
			_autorRepository = autorRepository;
			_categoriaRepository = categoriaRepository;
			_exemplarRepository = exemplarRepository;
		}

		public async Task<Result<ExemplarDto>> CriarExemplar(CriarExemplarRequest request)
		{
			var exemplar = request.ToExemplarFromCreate();

			await _exemplarRepository.AddAsync(exemplar);

			return Result<ExemplarDto>.Success(exemplar.ToExemplarDto());
		}

		public async Task<Result<List<ExemplarDto>>> ListarExemplares()
		{
			var exemplares = await _exemplarRepository.GetExemplares();

			var exemplaresDto = exemplares
				.Select(e => e.ToExemplarDto())
				.ToList();

			return Result<List<ExemplarDto>>.Success(exemplaresDto);
		}

		public async Task<Result<List<ExemplarDto>>> ListarExemparesPorLivro(int id)
		{
			var exemplares = await _exemplarRepository.GetExemplaresPorLivroAsync(id);

			var exemplaresDto = exemplares
				.Select(e => e.ToExemplarDto())
				.ToList();

			return Result<List<ExemplarDto>>.Success(exemplaresDto);
		}

		public async Task<Result<ExemplarDto>> GetById(int id)
		{
			var exemplar = await _exemplarRepository.GetByIdAsync(id);
			if (exemplar == null)
				return Result<ExemplarDto>.Failure("Não existe nenhuma exemplar com esse id");

			return Result<ExemplarDto>.Success(exemplar.ToExemplarDto());
		}

		public async Task<Result<ExemplarDto>> AtualizarExemplar(int id, AtualizarExemplarRequest request)
		{
			var exemplarExist = await _exemplarRepository.GetByIdAsync(id);

			if (exemplarExist == null)
				return Result<ExemplarDto>.Failure("Não existe um exemplar com esse id");

			var exemplar = await _exemplarRepository.AtualizarExemplarAsync(request, exemplarExist);

			return Result<ExemplarDto>.Success(exemplar.ToExemplarDto());
		}

		public async Task<Result<MessageResponseDto>> ApagarExemplar(int id)
		{
			var exemplarExist = await _exemplarRepository.GetByIdAsync(id);

			if (exemplarExist == null)
				return Result<MessageResponseDto>.Failure("Não existe uma exemplar com esse id");

			await _exemplarRepository.ApagarExemplarAsync(exemplarExist);

			return Result<MessageResponseDto>.Success(new MessageResponseDto { Message = "Exemplar apagado!" });
		}
	}
}
