using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TrackTruck.App_Start;

namespace TrackTruck
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Mapper.Initialize(c => c.AddProfile<MappingProfile>());//inicializar el automapping
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
