using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<MessageOptions>(options => options.CityName = "Albany");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//, IOptions<MessageOptions> msgOptions)
        {
            app.UseDeveloperExceptionPage();

            // app.UseMiddleware<Population>();
            // app.UseMiddleware<Capital>();

            app.UseRouting();

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("{first}/{second}/{third}", async context =>
                {
                    await context.Response.WriteAsync("Request was Routed. \n");
                    foreach (var pair in context.Request.RouteValues)
                    {
                        await context.Response.WriteAsync($"{pair.Key} : {pair.Value} \n");
                    }
                });

                //endPoints.MapGet("capital/uk", new Population().Invoke);
                endPoints.MapGet("capital/{country}", Capital.EndPoint);
                //endPoints.MapGet("population/paris", new Population().Invoke);
                endPoints.MapGet("population/{city}", Population.EndPoint).WithMetadata(new RouteNameMetadata("population"));
            });

            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Terminal Middleware Reached");
            });
        }
    }
}
