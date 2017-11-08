using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using TrackerApp.API.App_Start;

namespace TrackerApp.API
{
    public class StartUp : IOwinAppBuilder
    {
        
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            FormatterConfig.ConfigureFormatters(config.Formatters);
            appBuilder.UseWebApi(config);
        }
    }
}
