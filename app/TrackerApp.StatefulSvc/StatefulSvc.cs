using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using TrackerApp.Track.Interface;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace TrackerApp.StatefulSvc
{
    
    internal sealed class StatefulSvc : StatefulService,ILocationReporter,ILocationViewer
    {
        public StatefulSvc(StatefulServiceContext context)
            : base(context)
        { }

        public Task<KeyValuePair<float, float>?> GetLastReportingLocation(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<DateTime?> GetLastReportingTime(Guid orderId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var timestamps = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, DateTime>>("timestamps");
                var time = await timestamps.TryGetValueAsync(tx, orderId);
                await tx.CommitAsync();
                return time.HasValue ? (DateTime?)time.Value : null;
            }
        }

        public async Task ReportLocation(Location location)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var timestamps = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, DateTime>>("timestamps");
                var timestamp = DateTime.UtcNow;
                await timestamps.AddOrUpdateAsync(tx, location.OrderId, DateTime.UtcNow, (guid, time) => timestamp);
                await tx.CommitAsync();
            }
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[] {
                new ServiceReplicaListener(
                    initParams => this.CreateServiceRemotingListener(initParams) // call the extension method here which accepts StatefulServiceContext
                )
            };
        }

        
    }
}
