using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Mapper;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Dtos.Livro;

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
	}
}
