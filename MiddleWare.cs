using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
}
