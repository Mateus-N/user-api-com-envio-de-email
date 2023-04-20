using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Models;

namespace UsuariosApi.Data;

public class UserDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
{
	public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
	{
	}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        Usuario admin = new Usuario
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            Id = 99999,
        };

        PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

        builder.Entity<Usuario>().HasData(admin);

        builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
        );

        builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 99998, Name = "regular", NormalizedName = "REGULAR" }
        );

        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 }
        );
    }
}