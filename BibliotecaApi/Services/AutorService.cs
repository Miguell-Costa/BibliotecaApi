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
	}
}
