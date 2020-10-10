using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class Population
    {
        private RequestDelegate next;

        public Population() { }

        public Population(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    string[] parts = context.Request.Path.ToString()
        //        .Split("/", StringSplitOptions.RemoveEmptyEntries);
        //    if (parts.Length == 2 && parts[0] == "population")
        //    {
        //        string city = parts[1];
        //        int? pop = null;
        //        switch (city.ToLower())
        //        {
        //            case "london":
        //                pop = 8_136_000;
        //                break;
        //            case "paris":
        //                pop = 2_141_000;
        //                break;
        //            case "monaco":
        //                pop = 39_000;
        //                break;
        //        }
        //        if (pop.HasValue)
        //        {
        //            await context.Response
        //                .WriteAsync($"City: {city}, Population: {pop}");
        //            return;
        //        }
        //    }
        //    if (next != null)
        //    {
        //        await next(context);
        //    }
        //}

        public static async Task EndPoint(HttpContext context)
        {
            string city = context.Request.RouteValues["city"] as string;
            int? population = null;

            switch ((city ?? "").ToLower())
            {
                case "london":
                    population = 8_343_0000;
                    break;
                case "paris":
                    population = 3_242_000;
                    break;
                case "monaco":
                    population = 45_000;
                    break;
            }

            if (population.HasValue)
            {
                await context.Response.WriteAsync($"City: {city}, Population of {population}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
