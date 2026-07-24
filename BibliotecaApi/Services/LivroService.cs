using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
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

		public async Task<Results<MessageResponseDto>> CriarLivro(CriarLivroRequest request)
		{
			var existISBN = await _livroRepository.GetByISBN(request.ISBN);
			if (existISBN == null)
				return Result<MessageResponseDto>.Failure("Já existe um livro com esse ISBN");

			var existAutor = await _autorRepository.GetById(request.AutorId);
			if(existAutor == null)
				return Result<MessageResponseDto>.Failure("Não existe um autor com esse id");

			return Result<MessageResponseDto>.Success(new MessageResponseDto { Message = "Livro criado com sucesso"});
		}
	}
}
