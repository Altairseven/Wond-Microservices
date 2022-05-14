using System.Reflection;
using Wond.Notifications.BusEvents;
using Wond.Shared.MessageBus;
using Wond.Shared.MessageBus.Subscriber;
using Wond.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Services
ConfigurationManager configuration = builder.Configuration;

builder.Host.ConfigureSerilog(configuration, Assembly.GetEntryAssembly()!.GetName().Name ?? "wond-notifications");

builder.Services.ConfigureDistributedCache(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IEventProcessor, NotificationsEventProcesor>();
builder.Services.AddHostedService<MessageBusSubscriber>();


#endregion

var app = builder.Build();

#region Middleware
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
