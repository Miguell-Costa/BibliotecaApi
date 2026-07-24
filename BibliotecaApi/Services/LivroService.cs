using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;

namespace BibliotecaApi.Services
{
	public class LivroService : ILivroService
	{
		private readonly ILivroRepository _livroRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly ICategoriaRepository _categoriaRepository;

		public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository, ICategoriaRepository categoriaRepository)
		{
			_livroRepository = livroRepository;
			_autorRepository = autorRepository;
			_categoriaRepository = categoriaRepository;
		}

		public async Task<Result<LivroDto>> CriarLivro(CriarLivroRequest request)
		{
			var existISBN = await _livroRepository.GetByISBN(request.ISBN);
			if (existISBN != null)
				return Result<LivroDto>.Failure("Já existe um livro com esse ISBN");

			var existAutor = await _autorRepository.GetById(request.AutorId);
			if(existAutor == null)
				return Result<LivroDto>.Failure("Não existe um autor com esse id");

			var existCategoria = await _categoriaRepository.GetById(request.CategoriaId);
			if (existCategoria == null)
				return Result<LivroDto>.Failure("Não existe uma categoria com esse id");

			var livro = request.ToLivroFromCreateDto();
			await _livroRepository.AddAsync(livro);

			return Result<LivroDto>.Success(livro.ToLivroDto());
		}
	}
}
