﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace TripPartner.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(new EnableCorsAttribute("http://localhost:9017", "*", "GET, POST, OPTIONS, PUT, DELETE"));
       
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                 name: "MyIndexRoute",
                 routeTemplate: "api/{controller}/{index}",
                 defaults: new { index = RouteParameter.Optional },
                 constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
                 );

            config.Routes.MapHttpRoute(
            name: "ByUserRoute",
            routeTemplate: "api/User/{creatorId}/{controller}",
            defaults: new { creatorId = RouteParameter.Optional },
            constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
            name: "ByTripRoute",
            routeTemplate: "api/Trip/{tripId}/{controller}",
            defaults: new { tripId = RouteParameter.Optional },
            constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
            name: "ByLocationRoute",
            routeTemplate: "api/Location/{locId}/{controller}",
            defaults: new { locId = RouteParameter.Optional },
            constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
            name: "LatLngRoute",
            routeTemplate: "api/Location/{lat}/{lng}/{controller}",
            defaults: new { lat = RouteParameter.Optional, lng = RouteParameter.Optional },
            constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
        }
    }
}
