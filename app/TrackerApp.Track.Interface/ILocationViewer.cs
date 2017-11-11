using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp.Track.Interface
{
    public interface ILocationViewer : IService
    {
        Task<KeyValuePair<float, float>?> GetLastReportingLocation(Guid orderId);
        Task<DateTime?> GetLastReportingTime(Guid orderId);
    }
}
