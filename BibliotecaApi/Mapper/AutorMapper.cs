using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Mapper
{
	public static class AutorMapper
	{
		public static AutorDto ToAutorDto(this Autor autor)
		{
			return new AutorDto
			{
				Id = autor.Id,
				Nome = autor.Nome,
				Biografia = autor.Biografia,
				DataNascimento = autor.DataNascimento,
				DataMorte = autor.DataMorte,
				OpenLibraryId = autor.OpenLibraryId
			};
		}

		public static Autor ToAutorFromCreate(this CriarAutorRequest dto)
		{
			return new Autor
			{
				Nome = dto.Nome,
				Biografia = dto.Biografia,
				DataNascimento = dto.DataNascimento,
				DataMorte = dto.DataMorte,
				OpenLibraryId = dto.OpenLibraryId
			};
		}
	}
}
