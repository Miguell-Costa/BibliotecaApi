using Azure.Core;
using BibliotecaApi.Interfaces;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Dtos.Livro;
using BibliotecaApi.Services;
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
		[Authorize(Policy = "Livro.Create")]
		public async Task<IActionResult> CriarLivro(CriarLivroRequest request)
		{
			var result = await _livroService.CriarLivro(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-livroos")]
		[Authorize(Policy = "Livro.Read")]
		public async Task<IActionResult> ListarLivros()
		{
			var result = await _livroService.ListarLivros();

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-livro/{id}")]
		[Authorize(Policy = "Livro.Read")]
		public async Task<IActionResult> ListarLivroPorId([FromRoute] int id)
		{
			var result = await _livroService.ListarLivroPorId(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-livro-ISBN/{ISBN}")]
		[Authorize(Policy = "Livro.Read")]
		public async Task<IActionResult> ListarLivroPorISBN([FromRoute] string ISBN)
		{
			var result = await _livroService.ListarLivroPorISBN(ISBN);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpDelete("apagar-livro/{id}")]
		[Authorize(Policy = "Livro.Delete")]
		public async Task<IActionResult> ApagarLivro([FromRoute] int id)
		{
			var result = await _livroService.ApagarLivro(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpPost("atualizar-livro/{id}")]
		[Authorize(Policy = "Livro.Delete")]
		public async Task<IActionResult> AtualizarLivro([FromRoute] int id, [FromBody]AtualizarLivroRequest request)
		{
			var result = await _livroService.AtualizarLivro(id, request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpPost("importar")]
		public async Task<IActionResult> teste(string isbn)
		{
			var result = await _livroService.ImportarLivro(isbn);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

	}
}
