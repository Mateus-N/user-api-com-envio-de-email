using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class LoginService
{
	private readonly SignInManager<Usuario> signInManager;
	private readonly TokenService tokenService;

    public LoginService(SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    public Result LogaUsuario(LoginRequest request)
	{
		var resultadoIdentity = signInManager
			.PasswordSignInAsync(request.Username, request.Password, false, false);
		if (resultadoIdentity.Result.Succeeded)
		{
			var identityUser = signInManager
				.UserManager
				.Users
				.FirstOrDefault(usuario =>
				usuario.NormalizedUserName == request.Username.ToUpper());
			Token token = tokenService
				.CreateToken(identityUser, signInManager
					.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
			return Result.Ok().WithSuccess(token.Value);
		}
		return Result.Fail("Login Falhou");
	}
}