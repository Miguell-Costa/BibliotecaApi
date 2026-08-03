using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Dtos.Categoria;

namespace BibliotecaApi.Services
{
	public class AutorService: IAutorService
	{
		private readonly ILivroRepository _livroRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly ICategoriaRepository _categoriaRepository;

		public AutorService(ILivroRepository livroRepository, IAutorRepository autorRepository, ICategoriaRepository categoriaRepository)
		{
			_livroRepository = livroRepository;
			_autorRepository = autorRepository;
			_categoriaRepository = categoriaRepository;
		}

		public async Task<Result<AutorDto>> CreateAutor(CriarAutorRequest request)
		{
			if(!string.IsNullOrWhiteSpace(request.OpenLibraryId))
			{
				var existeAutor = await _autorRepository.GetByOpenLibraryId(request.OpenLibraryId);
				if (existeAutor != null)
					return Result<AutorDto>.Failure("Já existe um autor com esse OpenLibraryId com esse nome");
			}

			var autor = request.ToAutorFromCreate();
			await _autorRepository.AddAsync(autor);

			return Result<AutorDto>.Success(autor.ToAutorDto());
		}

		public async Task<Result<List<AutorDto>>> ListarAutores()
		{
			var autores = await _autorRepository.GetAutoresAsync();

			var autoresDto = autores
				.Select(a => a.ToAutorDto())
				.ToList();

			return Result<List<AutorDto>>.Success(autoresDto);
		}

		public async Task<Result<MessageResponseDto>> ApagarAutor(int id)
		{
			var autorExist = await _autorRepository.GetById(id);

			if (autorExist == null)
				return Result<MessageResponseDto>.Failure("Não existe um autor com esse id");

			var temLivrosAssociados = await _autorRepository.TemLivrosAssociados(autorExist.Id);

			if (temLivrosAssociados)
				return Result<MessageResponseDto>.Failure("O autor tem livros associados");

			await _autorRepository.ApagarAsync(autorExist);

			return Result<MessageResponseDto>.Success(new MessageResponseDto { Message = "Autor apgado!" });
		}
	
		public async Task<Result<AutorDto>> ListarPorId(int id)
		{
			var autorExist = await _autorRepository.GetById(id);

			if (autorExist == null)
				return Result<AutorDto>.Failure("Não existe um autor com esse id");

			return	Result<AutorDto>.Success(autorExist.ToAutorDto());
		}

		public async Task<Result<AutorDto>> ListarPorOpenLibraryId(string id)
		{
			var autorExist = await _autorRepository.GetByOpenLibraryId(id);

			if (autorExist == null)
				return Result<AutorDto>.Failure("Não existe um autor com esse open library id");

			return Result<AutorDto>.Success(autorExist.ToAutorDto());
		}

		public async Task<Result<AutorDto>> AtualizarAutor(int id, CriarAutorRequest request)
		{
			var autorExist = await _autorRepository.GetById(id);

			if (autorExist == null)
				return Result<AutorDto>.Failure("Não existe um autor com esse id");

			var autor = await _autorRepository.ApagarAsync(autorExist);

			return Result<AutorDto>.Success(autor.ToAutorDto());
		}
	}
}
