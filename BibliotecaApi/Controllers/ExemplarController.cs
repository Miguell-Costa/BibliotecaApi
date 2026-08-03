using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Model.Dtos.Autor;
using BibliotecaApi.Model.Dtos.Exemplar;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExemplarController : ControllerBase
	{
		private readonly IExemplarService _exemplarService;

		public ExemplarController(IExemplarService exemplarService)
		{
			_exemplarService = exemplarService;
		}

		[HttpPost("criar-exemplar")]
		//[Authorize(Policy = "Autor.Create")]
		public async Task<IActionResult> CriarExemplar([FromBody]CriarExemplarRequest request)
		{
			var result = await _exemplarService.CriarExemplar(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-exemplares")]
		//[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarExemplares()
		{
			var result = await _exemplarService.ListarExemplares();

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-exemplar-livro/{id}")]
		//[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarExemplarPorLivro([FromRoute] int id)
		{
			var result = await _exemplarService.ListarExemparesPorLivro(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-id/{id}")]
		//[Authorize(Policy = "Autor.Read")]
		public async Task<IActionResult> ListarPorId([FromRoute] int id)
		{
			var result = await _exemplarService.GetById(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpDelete("apagar-exemplar/{id}")]
		//[Authorize(Policy = "Autor.Delete")]
		public async Task<IActionResult> ApagarExemplar([FromRoute] int id)
		{
			var result = await _exemplarService.ApagarExemplar(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpPost("atualizar-exemplar/{id}")]
		//[Authorize(Policy = "Autor.Delete")]
		public async Task<IActionResult> ApagarExemplar([FromRoute] int id, AtualizarExemplarRequest request)
		{
			var result = await _exemplarService.AtualizarExemplar(id, request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
