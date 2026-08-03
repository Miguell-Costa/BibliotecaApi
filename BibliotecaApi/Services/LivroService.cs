using BibliotecaApi.Interfaces;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaApi.Services
{
	public class LivroService : ILivroService
	{
		private readonly ILivroRepository _livroRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly ICategoriaRepository _categoriaRepository;
		private readonly IGoogleBooksService _googleBookService;

		public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository, ICategoriaRepository categoriaRepository, IGoogleBooksService googleBooksService)
		{
			_livroRepository = livroRepository;
			_autorRepository = autorRepository;
			_categoriaRepository = categoriaRepository;
			_googleBookService = googleBooksService;
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
	
		public async Task<Result<LivroDto>> ImportarLivro(string isbn)
		{
			var googleBook = await _googleBookService.GetBookByIsbnAsync(isbn);

			// Verificar se retornou algum livro
			if (googleBook.Items == null || !googleBook.Items.Any())
				return Result<LivroDto>.Failure("Livro não encontrado.");

			var volume = googleBook.Items.First().VolumeInfo;

			// Verificar se o livro possui autor por que na nossa bd é obrigatorio ter autor 
			if (volume.Authors == null || !volume.Authors.Any())
				return Result<LivroDto>.Failure("O livro não possui autor.");

			// Verificar se o livro possui categoria por que na nossa bd é obrigatorio ter autor 
			if (volume.Categories == null || !volume.Categories.Any())
				return Result<LivroDto>.Failure("O livro não possui categoria.");

			// verificar se ja existe autor com esse nome
			// se sim usa o id do autor existente
			// se não cria um autor novo
			var autor = await _autorRepository.GetByNome(volume.Authors.First());

			if (autor == null)
			{
				autor = new Autor
				{
					Nome = volume.Authors.First()
				};

				await _autorRepository.AddAsync(autor);
			}

			// verificar se ja existe categoria com esse nome
			// se sim usa o id da categoria existente
			// se não cria uma categoria nova
			var categoria = await _categoriaRepository.GetByNome(volume.Categories.First());

			if (categoria == null)
			{
				categoria = new Categoria
				{
					Nome = volume.Categories.First()
				};

				await _categoriaRepository.AddAsync(categoria);
			}

			// extrair ano publicação
			int? anoPublicacao = null;

			if (!string.IsNullOrWhiteSpace(volume.PublishedDate) &&
				volume.PublishedDate.Length >= 4 &&
				int.TryParse(volume.PublishedDate[..4], out var ano))
			{
				anoPublicacao = ano;
			}

			// criar livro
			var criarLivro = new CriarLivroRequest
			{
				ISBN = isbn,
				Titulo = volume.Title,
				Descricao = volume.Description,
				CategoriaId = categoria.Id,
				AutorId = autor.Id,
				NumeroPaginas = volume.PageCount,
				AnoPublicacao = anoPublicacao,
				CapaUrl = volume.ImageLinks?.Thumbnail
			};

			var result = await CriarLivro(criarLivro);

			if (!result.IsSuccess)
				return Result<LivroDto>.Failure(result.Error);

			return Result<LivroDto>.Success(result.Data);
		}
	}
}
