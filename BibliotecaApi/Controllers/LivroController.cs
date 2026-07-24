using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LivroController : ControllerBase
	{
		[Authorize]
		[HttpGet("teste")]
		public IActionResult Teste()
		{
			var claims = User.Claims.Select(c => new
			{
				c.Type,
				c.Value
			});

			return Ok(claims);
		}
	}
}
