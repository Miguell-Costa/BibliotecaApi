namespace BibliotecaApi.Model.Entities
{
	public class Autor
	{
		public int Id { get; set; }

		public string Nome { get; set; } = string.Empty;

		public string? Biografia { get; set; }

		public DateOnly? DataNascimento { get; set; }

		public DateOnly? DataMorte { get; set; }

		public string? OpenLibraryId { get; set; } = string.Empty;

		public ICollection<Livro> Livros { get; } = new List<Livro>();
	}
}
