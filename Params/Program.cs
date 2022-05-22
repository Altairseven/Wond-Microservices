using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Wond.Params.Configuration;
using Wond.Params.Data;
using Wond.Params.Mappers;
using Wond.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Services
ConfigurationManager configuration = builder.Configuration;

builder.Host.ConfigureSerilog(configuration, Assembly.GetEntryAssembly()!.GetName().Name ?? "wond-params");

var conString = configuration.GetConnectionString("ParamsDb");
Console.WriteLine("ParamsDb Mysql Connection String: " + conString);

builder.Services.AddDbContext<ParamsDbContext>(options => options.UseMySql(conString, MySqlServerVersion.AutoDetect(conString)));

builder.Services.ConfigureDistributedCache(configuration);

builder.Services.ConfigureJwtAuth(configuration);

builder.Services.ConfigureAutomapper();
builder.Services.AddAutoMapper(typeof(ParamsServiceProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.ConfigureWondSwagger("Wond Params"));

builder.Services.ConfigureDIForParams();



#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.FeedDb(app.Environment.IsProduction());

#endregion

app.Run();
