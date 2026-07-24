using BibliotecaApi.Interfaces.IServices;
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
		[Authorize(Policy = "Categoria.Create")]
		public async Task<IActionResult> CriarCategoria(CriarCategoriaRequest request)
		{
			var result = await _categoriaService.CreateCategoria(request);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return Ok(result.Data);
		}
	}
}
