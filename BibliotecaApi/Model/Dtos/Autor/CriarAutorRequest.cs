namespace BibliotecaApi.Model.Dtos.Autor
{
	public class CriarAutorRequest
	{
		public string Nome { get; set; } = string.Empty;

		public string? Biografia { get; set; }

		public DateOnly? DataNascimento { get; set; }

		public DateOnly? DataMorte { get; set; }

		public string? OpenLibraryId { get; set; } = string.Empty;
	}
}
