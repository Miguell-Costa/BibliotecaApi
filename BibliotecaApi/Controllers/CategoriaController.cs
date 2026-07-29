using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Model;
using BibliotecaApi.Model.Dtos.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriaController : ControllerBase
	{
		private readonly ICategoriaService _categoriaService;

		public CategoriaController(ICategoriaService categoriaService)
		{
			_categoriaService = categoriaService;
		}

		[HttpPost("criar-categoria")]
		//[Authorize(Policy = "Categoria.Create")]
		public async Task<IActionResult> CriarCategoria(CriarCategoriaRequest request)
		{
			var result = await _categoriaService.CreateCategoria(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpDelete("apagar-categoria-/{id}")]
		public async Task<IActionResult> ApagarCategoria(int id)
		{
			var result = await _categoriaService.ApagarCategoria(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}

		[HttpGet("listar-categorias")]
		public async Task<IActionResult> ListarCategorias()
		{
			var result = await _categoriaService.ListarCategorias();

			return Ok(result.Data);
		}

		[HttpPost("atualizar-categoria/{id}")]
		//[Authorize(Policy = "Categoria.Create")]
		public async Task<IActionResult> AtualizarCategria(int id, AtualizarCategoriaRequest request)
		{
			var result = await _categoriaService.AtualizarCategoria(id, request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
