namespace BibliotecaApi.Model.Entities
{
	public class Livro
	{
		public int Id { get; set; }
		public string ISBN { get; set; } = string.Empty;
		public string Titulo { get; set; } = string.Empty;
		public string? Descricao { get; set; } = string.Empty;
		public int CategoriaId { get; set; }
		public Categoria Categoria { get; set; } = null!;
		public int AutorId { get; set; }
		public Autor Autor { get; set; } = null!;
		public int? NumeroPaginas { get; set; }
		public int? AnoPublicacao { get; set; }
		public string? CapaUrl { get; set; }
	}
}
