using BibliotecaApi.Enums;

namespace BibliotecaApi.Model.Dtos.Exemplar
{
	public class ExemplarDto
	{
		public int Id { get; set; }

		public int LivroId { get; set; }

		public EstadoExemplar Estado { get; set; } = EstadoExemplar.Disponivel;
	}
}
