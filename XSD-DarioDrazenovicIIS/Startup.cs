using IISDarioDrazenovicXSD.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IISDarioDrazenovicXSD.Model.EsportsTeamArray;

namespace IISDarioDrazenovicXSD
{
    public class Startup
    {

        internal static EsportsTeamArray EsportsTeamArray;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            List<EsportsTeam> esportsTeam = new List<EsportsTeam>();
            EsportsTeamArray = new EsportsTeamArray(esportsTeam);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(Options => {
                Options.AddPolicy("Cors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTApiIIS v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /*private List<EsportsTeamArray> listOfTeams;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            listOfTeams = new List<EsportsTeamArray>
            {
                new EsportsTeamArray
                {
                    Id="1",
                    Type="League",
                    Name="G2",
                    Cost=24120.42
                },
                new EsportsTeamArray
                {
                    Id="2",
                    Type="League",
                    Name="Fnatic",
                    Cost=1245.12
                },
                new EsportsTeamArray
                {
                    Id="3",
                    Type="CS:GO",
                    Name="Vitality",
                    Cost=5412
                }
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            services.AddSingleton<List<EsportsTeamArray>>(listOfTeams);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }*/
    }
}