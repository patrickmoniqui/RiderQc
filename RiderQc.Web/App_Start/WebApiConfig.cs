using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RiderQc.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //enable cros
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();
        }
    }
}
