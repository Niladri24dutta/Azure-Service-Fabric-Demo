using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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
        public async Task<bool> Log(Object location)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("order/{orderId}/lastseen")]
        public async Task<DateTime?> LastSeen(Guid orderId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("order/{orderId}/lastlocation")]
        public async Task<object> LastLocation(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
