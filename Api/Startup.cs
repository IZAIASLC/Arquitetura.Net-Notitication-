using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Persistence.EF;
using Infra.Persistence.Repositories;
using Infra.Transactions;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security.Security;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using static Security.Security.Security;

namespace Api
{
    /// <summary>
    /// Classe de inicialização e configuração da api.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Contrutor padrão.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }


        /// <summary>
        /// Método executado pelo runtime. Usando para adicionar serviços ao container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //aplicar injeção de dependência
            services.AddScoped<EFContexto, EFContexto>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IServiceCanal, ServiceCanal>();
            services.AddTransient<IServiceUsuario, ServiceUsuario>();
            services.AddTransient<IServiceVideo, ServiceVideo>();
            services.AddTransient<IServicePlayList, ServicePlayList>();

            services.AddTransient<IServiceImagem, ServiceImagem>();

            services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
            services.AddTransient<IRepositoryCanal, RepositoryCanal>();
            services.AddTransient<IRepositoryVideo, RepositoryVideo>();
            services.AddTransient<IRepositoryPlayList, RepositoryPlayList>();
            services.AddTransient<IRepositoryImagem, RepositoryImagem>();
            services.AddHttpContextAccessor();

            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddDbContext<EFContexto>(options => options.UseSqlServer(Configuration["connectionString"]));

            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            // services.AddScoped<AccessToken>();
            services.AddScoped<AccessManager>();

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(tokenConfigurations);
            services.AddScoped<AuthenticatedUser>();

            
 
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvcCore();
             services.AddMvcCore().AddApiExplorer();
            //aplicando documentação com swagger

            //services.AddSwaggerGen(swagger =>
            //{
            //    swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My First Swagger", Version = "v1" });
            //    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
            //});
            services.AddSwaggerGen(c =>
            {
                //The generated Swagger JSON file will have these properties.
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Documentação para consumo da API 2",
                    Version = "v1",
                });
                c.SwaggerDoc("v2", new Info
                {
                    Title = "Documentação para consumo da API",
                    Version = "v2",
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                c.IncludeXmlComments(filePath);
 
            });

        }


        /// <summary>
        /// Método chamado pelo runtime. Usando para configurar o pipeline da requisição http.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Api 1");
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v2/swagger.json", "Api 2");
            });

        }
    }
}
