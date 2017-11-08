using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp.API.App_Start
{
    public static class FormatterConfig
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatter)
        {
            formatter.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}
