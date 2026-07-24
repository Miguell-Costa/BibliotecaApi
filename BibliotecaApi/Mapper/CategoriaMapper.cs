using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Mapper
{
	public static class CategoriaMapper
	{
		public static CategoriaDto ToCategoriaDto(this Categoria categoria)
		{
			return new CategoriaDto
			{
				Id = categoria.Id,
				Nome = categoria.Nome
			};
		}

		public static Categoria ToCategoriaFromCreate(this CriarCategoriaRequest dto)
		{
			return new Categoria
			{
				Nome = dto.Nome
			};
		}
	}
}
