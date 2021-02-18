using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Extensions;
using Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoService.Api.Options;
using Microsoft.OpenApi.Models;
using Infra.Data;
using Core.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringsOptions =
                Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();

            var cosmosDbOptions = Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
            var (serviceEndpoint, authKey) = connectionStringsOptions.ActiveConnectionStringOptions;
            var (databaseName, collectionData) = cosmosDbOptions;
            var collectionNames = collectionData.Select(c => c.Name).ToList();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "teste", Version = "v1" });
            });

            // services.AddControllersWithViews()
            //     .AddNewtonsoftJson(options =>
            //      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);            
            services.AddScoped<IPessoaRepository<Pessoa>, PessoaRepository<Pessoa>>();

            services.AddAutoMapperConfig();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();                
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "teste v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
