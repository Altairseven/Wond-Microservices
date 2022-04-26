using Microsoft.AspNetCore.Builder;
using Serilog.AspNetCore;
using Serilog.Sinks.File;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics;

namespace Wond.Shared.Services;

public static class LogConfiguration {

    public static void ConfigureSerilog(this ConfigureHostBuilder Host, string elasticUri, string assemblyName) {
        
        Host.UseSerilog((context, configuration) => {
            configuration.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                  new ElasticsearchSinkOptions(
                      new Uri(elasticUri)
                  ) {
                      IndexFormat = $"{$"{assemblyName}-logs-{context.HostingEnvironment.EnvironmentName}".ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
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
                )
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });

        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

    }


}
