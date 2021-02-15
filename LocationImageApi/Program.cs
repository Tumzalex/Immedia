using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context,configuration)=>
                {
                    var elasticSearchSink = new ElasticsearchSinkOptions(new Uri(context.Configuration.GetValue<string>("ElasticsearchUri")));
                    elasticSearchSink.IndexFormat = $"LocationImageApi-{DateTime.UtcNow:yyyy-MM}";

                    configuration.Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(elasticSearchSink)
                        .ReadFrom.Configuration(context.Configuration);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
