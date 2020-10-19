using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using contas.api.Infrastructure.Context;
using contas.api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace contas.api.Config
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDepedencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceConfig();
            services.AddDomain();
            services.AddAdapters(configuration);
            services.AddSwagger(configuration);
        }

        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IContasService, ContasService>();
        }

        public static void AddAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContasContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddServiceConfig(this IServiceCollection services)
        {
            services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = false);

            services.AddLogging(a => a.AddConsole(c =>
            {
                c.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff\t";
                c.IncludeScopes = false;
            }));

            services.AddControllers();

            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
                    jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    jsonOptions.JsonSerializerOptions.WriteIndented = false;
                    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddCors(opt =>
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                }));
        }

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var versao = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Claudionei Oliveira - Contas API {versao}",
                    Description = "Contas API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Claudionei Oliveira",
                        Email = "claudionei.oliveira@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/claudionei-oliveira-49599120/")
                    },
                });
            });
        }
    }
}