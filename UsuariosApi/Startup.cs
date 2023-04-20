using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi;

public class Startup
{
    public ConfigurationManager Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public Startup(ConfigurationManager configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
	{
        Configuration
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Env.EnvironmentName}.json");

		EmailConnectionUrl? emailConnection = Configuration
			.GetSection("EmailConnectionLink")
			.Get<EmailConnectionUrl>();
		services.AddSingleton(emailConnection);

        services.AddDbContext<UserDbContext>(options =>
			options.UseMySQL(Configuration.GetConnectionString("UsuarioConnection")));
		services.AddIdentity<Usuario, IdentityRole<int>>(
			opt => opt.SignIn.RequireConfirmedEmail = true
			)
			.AddEntityFrameworkStores<UserDbContext>()
			.AddDefaultTokenProviders();

		services.AddScoped<SendEmailService, SendEmailService>();
		services.AddScoped<CadastroService, CadastroService>();
		services.AddScoped<LogoutService, LogoutService>();
		services.AddScoped<LoginService, LoginService>();
		services.AddScoped<TokenService, TokenService>();
		services.AddScoped<HttpClient, HttpClient>();
		
		services.AddControllers();
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app)
	{
		if (Env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseHttpsRedirection();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
