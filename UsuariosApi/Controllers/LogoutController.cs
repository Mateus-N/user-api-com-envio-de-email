using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
	private readonly LogoutService logoutService;

	public LogoutController(LogoutService logoutService)
	{
		this.logoutService = logoutService;
	}
	
	[HttpPost]
	public IActionResult DeslogarUsuario()
	{
		Result resultado = logoutService.DeslogarUsuario();
		if (resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
		return Ok(resultado.Successes.FirstOrDefault());
	}
}
