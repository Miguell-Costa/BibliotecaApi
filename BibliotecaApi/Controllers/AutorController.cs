using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Model.Dtos.Autor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AutorController : ControllerBase
	{
		private readonly IAutorService _autorService;

		public AutorController(IAutorService autorService)
		{
			_autorService = autorService;
		}

		[HttpPost("criar-autor")]
		[Authorize(Policy = "Autor.Create")]
		public async Task<IActionResult> CriarAutor(CriarAutorRequest request)
		{
			var result = await _autorService.CreateAutor(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
