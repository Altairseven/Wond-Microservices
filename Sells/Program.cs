using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Wond.Shared.MessageBus.Client;
using Wond.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);


#region Services
ConfigurationManager configuration = builder.Configuration;

builder.Host.ConfigureSerilog(configuration, Assembly.GetEntryAssembly()!.GetName().Name ?? "wond-sells");

builder.Services.ConfigureDistributedCache(configuration);

builder.Services.ConfigureJwtAuth(configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.ConfigureAutomapper();



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
