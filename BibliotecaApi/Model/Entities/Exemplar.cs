using BibliotecaApi.Enums;

namespace BibliotecaApi.Model.Entities
{
	public class Exemplar
	{
		public int Id { get; set; }

		public int LivroId { get; set; }

		public Livro Livro { get; set; } = null!;

		public EstadoExemplar Estado { get; set; } = EstadoExemplar.Disponivel;
	}
}
