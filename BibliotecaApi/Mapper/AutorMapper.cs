using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Entities;

namespace BibliotecaApi.Mapper
{
	public static class AutorMapper
	{
		public static AutorDto ToRoleDto(this Autor autor)
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
	}
}
