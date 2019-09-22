using Calender.Api.Controllers;
using Calender.Api.IoC;
using Calender.Api.Middleware;
using Calender.Data;
using LaYumba.Functional;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Calender
{
    using static Calender.Data.ConnectionFunctions;
    using static EventModelFunctions;
    using static F;

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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices(); // make sure our custom controller invoking is used

            // dependency injection
            ConnectionString
                connStr = "Server=(localdb)\\MSSQLLocalDB;Database=Calender;Integrated Security=true;";

            services.AddTransient(_ =>
                new EventsController(
                    GetEventModel.Apply(connStr),
                    GetEventModels.Apply(connStr)));
            services.AddTransient(_ =>
                new EventController(
                    AddEventIoc.WireUpAddEvent(connStr),
                    DeleteEventIoc.WireUpDeleteEvent(connStr)));            

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMvc();

            // Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calender V1");
            });

        }
    }
}
