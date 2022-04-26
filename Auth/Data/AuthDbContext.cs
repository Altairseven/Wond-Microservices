using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wond.Auth.Models;

namespace Wond.Auth.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long> {
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
    }
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }
}