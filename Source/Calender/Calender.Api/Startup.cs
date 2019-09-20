using Calender.Api.Controllers;
using Calender.Api.Middleware;
using Calender.Data;
using Calender.Domain.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Calender
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices(); // make sure our custom controller invoking is used

            // dependency injection
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Calender;Integrated Security=true;";

            var repository = new EventRepository(connectionString);

            services.AddTransient(_ => 
                new EventsController(
                    new EventsQuery(connectionString),
                    new EventQuery(connectionString)));
            services.AddTransient(_ =>
                new EventController(
                    new AddEventCommandHandler(repository),
                    new DeleteEventCommandHandler(repository)));            

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
