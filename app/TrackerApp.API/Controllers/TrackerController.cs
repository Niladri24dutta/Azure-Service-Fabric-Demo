using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TrackerApp.Track.Interface;

namespace TrackerApp.API.Controllers
{
    [RoutePrefix("Tracker")]
    public class TrackerController : ApiController 
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to Tracker 1.0.0 - The order tracking system";
        }

        [HttpPost]
        [Route("locations")]
        public async Task<bool> Log(Location location)
        {
            var reporter = TrackerConnectionFactory.CreateLocationReporter();
            await reporter.ReportLocation(location);
            return true;
        }

        [HttpGet]
        [Route("order/{orderId}/lastseen")]
        public async Task<DateTime?> LastSeen(Guid orderId)
        {
            var viewer = TrackerConnectionFactory.CreateLocationViewer();
            return await viewer.GetLastReportingTime(orderId);
        }

        [HttpGet]
        [Route("order/{orderId}/lastlocation")]
        public async Task<object> LastLocation(Guid orderId)
        {
            var viewer = TrackerConnectionFactory.CreateLocationViewer();
            var location = await viewer.GetLastReportingLocation(orderId);
            if(location == null)
            {
                return null;
            }
            return new { Latitude = location.Value.Key, Longitude = location.Value.Value };
        }
    }
}
