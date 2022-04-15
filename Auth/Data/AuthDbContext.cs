using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wond.Auth.Data;

public class AuthDbContext : IdentityDbContext<IdentityUser> {
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
    }
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }
}