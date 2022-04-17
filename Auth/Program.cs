using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wond.Auth.Data;
using System.Reflection;
using Wond.Shared.Services;

var builder = WebApplication.CreateBuilder(args);


#region services
ConfigurationManager configuration = builder.Configuration;

builder.Host.ConfigureSerilog(configuration["ElasticConfig:Uri"], Assembly.GetEntryAssembly()!.GetName().Name ?? "wond-auth");

var conString = configuration.GetConnectionString("AuthDb");
Console.WriteLine("Mysql Connection String: " + conString);

builder.Services.AddDbContext<AuthDbContext>(options => options.UseMySql(conString, MySqlServerVersion.AutoDetect(conString)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
});

builder.Services.ConfigureJwtAuth(configuration);

builder.Services.AddControllers();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.FeedDb(app.Environment.IsProduction());

#endregion

app.Run();
