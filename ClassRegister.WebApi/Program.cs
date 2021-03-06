using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace ClassRegister.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DIContainerProvider().GetContainer();

            WebHost
                .CreateDefaultBuilder()
                .UseUnityServiceProvider(container)
                .ConfigureServices(services =>
                {
                    services.AddMvc().AddJsonOptions(x => new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
                    services.AddSwaggerGen(SwaggerDocsConfig);
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                    app.UseCors();
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassRegister V1");
                        c.RoutePrefix = string.Empty;
                    });
                })
                .UseUrls("http://*:10500")
                .Build()
                .Run();
        }

        private static void SwaggerDocsConfig(SwaggerGenOptions genOptions)
        {
            genOptions.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ClassRegister",
                    Description = "Aplication of class register - ASP.NET Core Web API",
                    TermsOfService = new Uri("https://ClassRegister.project.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sylwia Ignerowicz",
                        Email = "jagsyl@poczta.onet.pl",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use some license",
                        Url = new Uri("https://ClassRegister.project.com/license")
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            genOptions.IncludeXmlComments(xmlPath);
        }
    }
}