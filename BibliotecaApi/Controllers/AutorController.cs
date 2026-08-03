using Azure.Core;
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

		[HttpGet("listar-autores")]
		[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarAutores()
		{
			var result = await _autorService.ListarAutores();

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-autor/{id}")]
		[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarAutorPorId([FromRoute] int id)
		{
			var result = await _autorService.ListarPorId(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-autor-openlibrary/{id}")]
		[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarAutorPorOpenLibraryId([FromRoute] string id)
		{
			var result = await _autorService.ListarPorOpenLibraryId(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpDelete("apagar-autor/{id}")]
		[Authorize(Policy = "Autor.Delete")]
		public async Task<IActionResult> ApagarAutor([FromRoute]int id)
		{
			var result = await _autorService.ApagarAutor(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpPost("atualizar-autor/{id}")]
		[Authorize(Policy = "Autor.Delete")]
		public async Task<IActionResult> AtualizarAutor(int id, CriarAutorRequest request)
		{
			var result = await _autorService.AtualizarAutor(id, request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
