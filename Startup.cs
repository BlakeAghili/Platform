using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.Configure<MessageOptions>(options => options.CityName = "Albany");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//, IOptions<MessageOptions> msgOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // conditional branching in middleware.
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), branch => { branch.UseMiddleware<QueryStringMiddleWare>(); });

            // this was for when we pass Options as Lambda
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/location")
            //    {
            //        MessageOptions opts = msgOptions.Value;
            //        await context.Response.WriteAsync($"{opts.CityName} in {opts.CountryName}");
            //        await next();
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"\n Status Code: {context.Response.StatusCode} \n");
                await next();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
                {
                    await context.Response.WriteAsync("Hello from Inline Middleware \n");
                }

                await next();
            });

            //app.UseMiddleware<QueryStringMiddleWare>();
            app.UseMiddleware<LocationMiddleWare>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World From Starutp.cs! \n");
                });
            });
        }
    }
}
