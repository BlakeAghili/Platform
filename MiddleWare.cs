using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate next;
        public QueryStringMiddleWare(RequestDelegate nextDelegate)
        {
            this.next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Hello from Class-based Middleware \n");
            }

            await next(context);
        }
    }

    public class LocationMiddleWare
    {
        private RequestDelegate next;
        private MessageOptions options;

        public LocationMiddleWare(RequestDelegate nextDelegate, IOptions<MessageOptions> opts)
        {
            next = nextDelegate;
            options = opts.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{options.CityName} is in {options.CountryName} I guess.");
            }
            else
            {
                await next(context);
            }
        }
    }
}
