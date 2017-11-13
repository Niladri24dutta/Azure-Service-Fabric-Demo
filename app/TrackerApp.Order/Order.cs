using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using TrackerApp.Order.Interfaces;
using System.Runtime.Serialization;

namespace TrackerApp.Order
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class Order : Actor, IOrder
    {
        /// <summary>
        /// Initializes a new instance of Order
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public Order(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        [DataContract]
        internal sealed class LocationAtTime
        {
            public DateTime TimeStamp { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

        [DataContract]
        internal sealed class OrderState
        {
            public List<LocationAtTime> LocationHistory { get; set; }
        }
        public Task<KeyValuePair<float, float>> GetLatestLocation()
        {
            throw new NotImplementedException();
        }

        public Task SetLocation(DateTime time, float latitude, float longitude)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override async Task OnActivateAsync()
        {
            var state = await this.StateManager.TryGetStateAsync<OrderState>("State");
            if(!state.HasValue)
            {
                await this.StateManager.AddStateAsync<OrderState>("State", new OrderState { LocationHistory = new List<LocationAtTime>() });
            }
        }

    }
}
