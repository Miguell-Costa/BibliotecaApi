namespace BibliotecaApi.Model.Dtos.Livro
{
	public class AtualizarLivroRequest
	{
		public string Titulo { get; set; } = string.Empty;
		public string? Descricao { get; set; } = string.Empty;
		public int CategoriaId { get; set; }
		public int AutorId { get; set; }
		public int? NumeroPaginas { get; set; }
		public int? AnoPublicacao { get; set; }
		public string? CapaUrl { get; set; }
	}
}
