using Microsoft.EntityFrameworkCore;
using Wond.Params.Models;

namespace Wond.Params.Data;

public class ParamsDbContext : DbContext {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ParamsDbContext(DbContextOptions<ParamsDbContext> options) : base(options) {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }


    public DbSet<Material> Materials { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Category> Categories { get; set; }


}

