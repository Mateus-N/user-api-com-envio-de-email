using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class CadastroService
{
	private readonly IMapper mapper;
	private readonly UserManager<Usuario> userManager;
	private readonly SendEmailService sendEmailService;

    public CadastroService(
        IMapper mapper,
        UserManager<Usuario> userManager,
        SendEmailService sendEmailService)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.sendEmailService = sendEmailService;
    }

    public async Task<Result> CadastraUsuario(CreateUsuarioDto createDto)
	{
		Usuario usuario = mapper.Map<Usuario>(createDto);

		Task<IdentityResult> resultadoIdentity = userManager
			.CreateAsync(usuario, createDto.Password);

		userManager.AddToRoleAsync(usuario, "regular");

		if (resultadoIdentity.Result.Succeeded)
        {
			var code = userManager.GenerateEmailConfirmationTokenAsync(usuario).Result;
			var encodedCode = HttpUtility.UrlEncode(code);
			BodyContent content = new(usuario.Id, encodedCode);

			await sendEmailService.SendToEmailAsync(
				usuario.Email,
				"Código de validação",
				"Clique no link abaixo para ativar sua conta",
				content.Value);

            return Result.Ok().WithSuccess(code);
        }

        return Result.Fail("Falha ao cadastrar usuário");
	}

	public Result AtivaContaUsuario(AtivaContaRequest request)
	{
		var usuario = userManager
			.Users
			.FirstOrDefault(u => u.Id == request.UsuarioId);

		var identityResult = userManager
			.ConfirmEmailAsync(usuario, request.CodigoDeAtivacao).Result;

		if (identityResult.Succeeded)
		{
			return Result.Ok();
		}
		return Result.Fail("Falha ao ativar usuário");
	}
}