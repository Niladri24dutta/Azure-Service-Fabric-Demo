using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp.Order.Interfaces
{
    public class OrderConnectionFactory
    {
        private static readonly Uri OrderServiceUrl = new Uri("fabric:/TrackerApplication/Order");

        public static IOrder GetOrder(ActorId id)
        {
            return ActorProxy.Create<IOrder>(id, OrderServiceUrl);
        }
    }
}
