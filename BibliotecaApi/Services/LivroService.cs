using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;
using Microsoft.AspNetCore.Http.HttpResults;

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

		public async Task<Result<List<LivroDto>>> ListarLivros()
		{
			var livros = await _livroRepository.GetLivrosAsync();

			var livrosDto = livros
				.Select(l => l.ToLivroDto())
				.ToList();

			return Result<List<LivroDto>>.Success(livrosDto);
		}

		public async Task<Result<LivroDto>> ListarLivroPorId(int id)
		{
			var livroExist = await _livroRepository.GetLivroByIdAsync(id);

			if (livroExist == null)
				return Result<LivroDto>.Failure("Não existe um livro com esse id");

			return Result<LivroDto>.Success(livroExist.ToLivroDto());
		}

		public async Task<Result<LivroDto>> ListarLivroPorISBN(string ISBN)
		{
			var livroExist = await _livroRepository.GetByISBN(ISBN);

			if (livroExist == null)
				return Result<LivroDto>.Failure("Não existe um livro com esse ISBN");

			return Result<LivroDto>.Success(livroExist.ToLivroDto());
		}
	
		public async Task<Result<LivroDto>> AtualizarLivro(int id, AtualizarLivroRequest request)
		{
			var livroExist = await _livroRepository.GetLivroByIdAsync(id);

			if (livroExist == null)
				return Result<LivroDto>.Failure("Não existe nenhum livro com esse id");

			var existAutor = await _autorRepository.GetById(request.AutorId);
			if (existAutor == null)
				return Result<LivroDto>.Failure("Não existe um autor com esse id");

			var existCategoria = await _categoriaRepository.GetById(request.CategoriaId);
			if (existCategoria == null)
				return Result<LivroDto>.Failure("Não existe uma categoria com esse id");

			var livro = await _livroRepository.UpdateLivroAsync(request, livroExist);

			return Result<LivroDto>.Success(livro.ToLivroDto());
		}
		
		public async Task<Result<MessageResponseDto>> ApagarLivro(int id)
		{
			var livroExist = await _livroRepository.GetLivroByIdAsync(id);

			if (livroExist == null)
				return Result<MessageResponseDto>.Failure("Não existe nenhum livro com esse id");

			await _livroRepository.ApagarLivroAsync(livroExist);

			return Result<MessageResponseDto>.Success(new MessageResponseDto { Message = "Livro apagado com sucesso" });
		}
	}
}
