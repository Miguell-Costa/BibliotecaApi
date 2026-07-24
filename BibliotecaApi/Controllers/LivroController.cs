using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Model.Dtos.Livro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LivroController : ControllerBase
	{
		private readonly ILivroService _livroService;

		public LivroController(ILivroService livroService)
		{
			_livroService = livroService;
		}

		[Authorize]
		[HttpPost("criar-livro")]
		public async Task<IActionResult> CriarLivro(CriarLivroRequest request)
		{
			var result = await _livroService.CriarLivro(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
