using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CadastroController : ControllerBase
{
	private readonly CadastroService cadastroService;
	
	public CadastroController(CadastroService cadastroService)
	{
		this.cadastroService = cadastroService;
	}

	[HttpPost]
	public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createDto)
	{
		Result resultado = await cadastroService.CadastraUsuario(createDto);
		if (resultado.IsFailed) return StatusCode(500);
		return Ok(resultado.Successes.FirstOrDefault());
	}

	[HttpGet("/ativa")]
	public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
	{
		Result resultado = cadastroService.AtivaContaUsuario(request);
        if (resultado.IsFailed) return StatusCode(500);
        return Ok();
    }
}
