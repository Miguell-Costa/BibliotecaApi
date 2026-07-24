namespace BibliotecaApi.Model.Dtos.Autor
{
	public class AutorDto
	{
		public int Id { get; set; }

		public string Nome { get; set; } = string.Empty;

		public string? Biografia { get; set; }

		public DateOnly? DataNascimento { get; set; }

		public DateOnly? DataMorte { get; set; }

		public string? OpenLibraryId { get; set; } = string.Empty;
	}
}
