using BibliotecaApi.Enums;

namespace BibliotecaApi.Model.Dtos.Exemplar
{
	public class CriarExemplarRequest
	{
		public int LivroId { get; set; }

		public EstadoExemplar Estado { get; set; } = EstadoExemplar.Disponivel;
	}
}
