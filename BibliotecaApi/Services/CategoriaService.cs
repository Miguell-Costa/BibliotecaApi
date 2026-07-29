using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Services
{
	public class CategoriaService: ICategoriaService
	{
		private readonly ILivroRepository _livroRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly ICategoriaRepository _categoriaRepository;

		public CategoriaService(ILivroRepository livroRepository, IAutorRepository autorRepository, ICategoriaRepository categoriaRepository)
		{
			_livroRepository = livroRepository;
			_autorRepository = autorRepository;
			_categoriaRepository = categoriaRepository;
		}

		public async Task<Result<CategoriaDto>> CreateCategoria(CriarCategoriaRequest request)
		{
			var existeCategoria = await _categoriaRepository.GetByNome(request.Nome);
			if (existeCategoria != null)
				return Result<CategoriaDto>.Failure("Já existe uma categoria com esse nome");

			var categoria = request.ToCategoriaFromCreate();
			await _categoriaRepository.AddAsync(categoria);

			return Result<CategoriaDto>.Success(categoria.ToCategoriaDto());
		}

		public async Task<Result<MessageResponseDto>> ApagarCategoria(int id)
		{
			var categoria = await _categoriaRepository.GetById(id);

			if (categoria == null)
				return Result<MessageResponseDto>.Failure("Não existe nenhuma categoria com esse id");

			var temLivrosAssociados = await _categoriaRepository.TemLivrosAssociadosAsync(categoria.Id);

			if(temLivrosAssociados)
				return Result<MessageResponseDto>.Failure("Não é possivel apagar uma categoria que tem livros associados");

			await _categoriaRepository.ApagarAsync(categoria);

			return Result<MessageResponseDto>.Success(new MessageResponseDto { Message = "Categoria apagada com sucesso" });
		}
	
		public async Task<Result<List<CategoriaDto>>> ListarCategorias()
		{
			var categorias = await _categoriaRepository.GetLivrosAsync();

			var categoriasDto = categorias
				.Select(c => c.ToCategoriaDto())
				.ToList();

			return Result<List<CategoriaDto>>.Success(categoriasDto);
		}
		
		public async Task<Result<CategoriaDto>> AtualizarCategoria(int id, AtualizarCategoriaRequest dto)
		{
			var categoriaExist = await _categoriaRepository.GetById(id);

			if (categoriaExist == null)
				return Result<CategoriaDto>.Failure("Não existe nenhuma categoria com esse id");

			var categoria = await _categoriaRepository.AtualizarCategoriaAsync(dto, categoriaExist);

			return Result<CategoriaDto>.Success(categoria.ToCategoriaDto());
		}

	}
}
