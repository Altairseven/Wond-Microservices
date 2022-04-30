using System.Reflection;
using Wond.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

#region Services
ConfigurationManager configuration = builder.Configuration;

builder.Host.ConfigureSerilog(configuration, Assembly.GetEntryAssembly()!.GetName().Name ?? "wond-stock");

builder.Services.ConfigureJwtAuth(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Middleware
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
