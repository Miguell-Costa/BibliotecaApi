using BibliotecaApi.Model.Dtos.Categoria;
using BibliotecaApi.Model.Dtos.Exemplar;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Mapper
{
	public static class ExemplarMapper
	{
		public static ExemplarDto ToExemplarDto(this Exemplar exemplar)
		{
			return new ExemplarDto
			{
				Id = exemplar.Id,
				LivroId = exemplar.LivroId,
				Estado = exemplar.Estado
			};
		}

		public static Exemplar ToExemplarFromCreate(this CriarExemplarRequest dto)
		{
			return new Exemplar
			{
				LivroId = dto.LivroId,
				Estado = dto.Estado
			};
		}
	}
}
