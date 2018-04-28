using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebAPI1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            /*  //Remove Xml
              var MyXml = config.Formatters.XmlFormatter;//get it first
            //  config.Formatters.Remove(MyXml);//then remove it

              //Change Json Format

              var MyJson = config.Formatters.JsonFormatter;

              MyJson.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              MyJson.SerializerSettings.DateFormatString = "dd-MM-yyyy";
              */
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));

            //To solve 
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            json.SerializerSettings.DateFormatString = "dd-MM-yyyy";

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "Api1",
               routeTemplate: "api/Emps/"
              // ,defaults: new { name = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "Api2",
               routeTemplate: "api/Emps/Emps"
           // ,defaults: new { name = RouteParameter.Optional }
           );

           

             config.Routes.MapHttpRoute(
                name: "Api2_1",
                routeTemplate: "api/Emps/Emps/{name}/details",
                defaults: new { name = RouteParameter.Optional }
            );

             config.Routes.MapHttpRoute(
             name: "Api3",
             routeTemplate: "api/Emps/Emps/{hiredate}"
          , defaults: new { hiredate = RouteParameter.Optional }
         );

             config.Routes.MapHttpRoute(
             name: "Api4",
             routeTemplate: "Depts/{id}/Employees"
          , defaults: new { id = RouteParameter.Optional }
         );


            config.Routes.MapHttpRoute(
            name: "Api8",
            routeTemplate: "Depts/All"
          , defaults: new { }

        );

            config.Routes.MapHttpRoute(
             name: "Api5",
             routeTemplate: "api/Emps/Emps/Add/Name={Name}/Salary={Salary}/Department={DepartmentId}/HDate={HireDate}"
         , defaults: new { Salary = RouteParameter.Optional,
             DepartmentId=RouteParameter.Optional,
             HireDate=RouteParameter.Optional,
            Name=RouteParameter.Optional

             }
         );

            config.Routes.MapHttpRoute(
            name: "Api6",
            routeTemplate: "api/Emps/Emps/Edit/Name={Name}/Salary={Salary}/Department={DepartmentId}/HDate={HireDate}"
        , defaults: new {
          //  OldId = RouteParameter.Optional,
            Salary = RouteParameter.Optional,
             DepartmentId=RouteParameter.Optional,
             HireDate=RouteParameter.Optional,
            Name=RouteParameter.Optional
            }
        );

            config.Routes.MapHttpRoute(
           name: "Api7",
           routeTemplate: "api/Emps/Emps/Edit/{id}"
       , defaults: new { id = RouteParameter.Optional }

       );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
