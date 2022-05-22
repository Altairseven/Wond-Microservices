using Microsoft.EntityFrameworkCore;

namespace Wond.Params.Data;

public static class FeedDB {

    public static void FeedDb(this IApplicationBuilder app, bool isProduction) {
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(serviceScope.ServiceProvider.GetRequiredService<ParamsDbContext>(), isProduction);

    }

    public static void SeedData(ParamsDbContext _db, bool isProduction) {
        if (isProduction) {
            Console.WriteLine("--> Attempting to apply migrations...");
            try {
                _db.Database.Migrate();
            }
            catch (Exception ex) {
                Console.WriteLine($"--> Failed to apply migrations...{ex.Message}");
            }
        }

        if (!_db.Colors.Any()) {
            Console.WriteLine("=> Seeding Data...");

            _db.Colors.AddRange(
                new Models.Color { Name = "Rojo" },
                new Models.Color { Name = "Azul" },
                new Models.Color { Name = "Verde" }
            );

            _db.SaveChanges();


        }
        else {
            Console.WriteLine("=> Db alreaddy Feedead");
        }
    }


}
