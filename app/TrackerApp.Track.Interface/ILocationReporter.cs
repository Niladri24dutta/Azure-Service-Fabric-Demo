﻿using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp.Track.Interface
{
    public interface ILocationReporter :IService
    {
        Task ReportLocation(Location location);
    }
}
