﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Platform
{
    public class CountryRouteConstraint: IRouteConstraint
    {
        private static readonly string[] countries = {"uk", "france", "monaco"};
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string segmentValue = values[routeKey] as string ?? "";

            return Array.IndexOf(countries, segmentValue.ToLower()) > -1;
        }
    }
}
