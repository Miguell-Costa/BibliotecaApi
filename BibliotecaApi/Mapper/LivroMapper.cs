using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Mapper
{
	public static class LivroMapper
	{
		public static LivroDto ToLivroDto(this Livro livro)
		{
			return new LivroDto
			{
				Id = livro.Id,
				Titulo = livro.Titulo,
				Descricao = livro.Descricao,
				CategoriaId = livro.CategoriaId,
				AutorId = livro.AutorId,
				NumeroPaginas = livro.NumeroPaginas,
				AnoPublicacao = livro.AnoPublicacao,
				CapaUrl = livro.CapaUrl
			};
		}

		public static Livro ToLivroFromCreateDto(this CriarLivroRequest dto)
		{
			return new Livro
			{
				Titulo = dto.Titulo,
				Descricao = dto.Descricao,
				CategoriaId = dto.CategoriaId,
				AutorId = dto.AutorId,
				NumeroPaginas = dto.NumeroPaginas,
				AnoPublicacao = dto.AnoPublicacao,
				CapaUrl = dto.CapaUrl
			};
		}
	}
}
