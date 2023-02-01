using XSD_DarioDrazenovicIIS.Model;
using static XSD_DarioDrazenovicIIS.Model.EsportsTeamArray;

namespace XSD_DarioDrazenovicIIS
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

        public void ConfigureServices(IServiceCollection services)
        {
            // Cross-Origin Resource Sharing -> allows requests from any origin, method, and header.
            services.AddCors(Options => {
                Options.AddPolicy("Cors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            // Support for the XML format when returning responses from the API.
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            // Support for the Model-View-Controller (MVC) pattern
            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable the developer exception page that displays detailed error information when an unhandled exception occurs.
                app.UseDeveloperExceptionPage();
                // Enable the Swagger middleware, which generates and serves the Swagger documents.
                app.UseSwagger();
                // Enable the Swagger UI, which provides an interactive UI for users to read the API documentation.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DarioDrazenovicRestApi"));
            }

            app.UseRouting();

            app.UseAuthorization();

            // "app.UseEndpoints" sets up endpoint routing.
            app.UseEndpoints(endpoints =>
            {
                // "endpoints.MapControllers()" maps the controllers in the application to endpoints.
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