// TaskPilot.API/Startup.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskPilot.Application;      // Para AddApplicationServices
using TaskPilot.Infrastructure;   // Para AddInfrastructureServices
using TaskPilot.API.Extensions;   // Para ExceptionMiddleware

namespace TaskPilot.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configura os servi�os (DI Container)
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona servi�os das camadas Application e Infrastructure
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration); // Passa a configura��o do BD/APIs

            services.AddControllers();

            // Adicionar configura��o do Swagger/OpenAPI (muito �til)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TaskPilot API", Version = "v1" });
            });
        }

        // Configura o pipeline de requisi��es HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Habilita o Swagger (Documenta��o da API)
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskPilot API v1"));
            }

            // MIDDLEWARE PARA TRATAMENTO GLOBAL DE EXCE��ES
            // Deve vir antes do UseRouting
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}