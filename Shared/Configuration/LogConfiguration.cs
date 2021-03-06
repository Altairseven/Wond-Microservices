using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Wond.Shared.Configuration;

public static class LogConfiguration {

    public static void ConfigureSerilog(this ConfigureHostBuilder Host, ConfigurationManager configuration, string assemblyName) {
        var elasticUri = configuration["ElasticConfig:Uri"];
        var enableElastic = bool.Parse(configuration["ElasticConfig:Enabled"] ?? "false");


        Host.UseSerilog((context, loggerConfig) => {
            var enviromentName = context.HostingEnvironment.EnvironmentName;
            loggerConfig.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteToElasticSearchIfEnabled(enableElastic, enviromentName, elasticUri, assemblyName)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });

        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

    }

    private static LoggerConfiguration WriteToElasticSearchIfEnabled(
        this LoggerConfiguration logger, bool enabled, string enviromentName, string elasticUri, string assemblyName
    ) {
        if (!enabled) return logger;


        return logger.WriteTo.Elasticsearch(
            new ElasticsearchSinkOptions(
                new Uri(elasticUri)
            ) {
                IndexFormat = $"{$"{assemblyName}-logs-{enviromentName}".ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                NumberOfShards = 2,
                NumberOfReplicas = 1,
                //FailureCallback = e => Console.WriteLine("Unable to submit event to ElasticSearch" + e.MessageTemplate),
                FailureCallback = e => { },
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                        EmitEventFailureHandling.WriteToFailureSink |
                                        EmitEventFailureHandling.RaiseCallback,
            }
        );
    }

}
