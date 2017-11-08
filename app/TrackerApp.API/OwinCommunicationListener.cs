using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Fabric;
using System.Globalization;
using Microsoft.Owin.Hosting;

namespace TrackerApp.API
{
    public class OwinCommunicationListener : ICommunicationListener
    {
        private readonly IOwinAppBuilder _startUp;
        private readonly string _appRoot;
        private readonly StatelessServiceContext _parameters;
        private string _listeningAddress;
        private IDisposable _serverHandle;

        public OwinCommunicationListener(string appRoot,IOwinAppBuilder startUp, StatelessServiceContext parameters)
        {
            _appRoot = appRoot;
            _startUp = startUp;
            _parameters = parameters;
        }
        public void Abort()
        {
            StopWebserver();
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            StopWebserver();
            return Task.FromResult(true);
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var serviceEndpoint =
            _parameters
            .CodePackageActivationContext
            .GetEndpoint("ServiceEndpoint");

            var port = serviceEndpoint.Port;
            var root =
                String.IsNullOrWhiteSpace(_appRoot)
                ? String.Empty
                : _appRoot.TrimEnd('/') + '/';

            _listeningAddress = String.Format(
                CultureInfo.InvariantCulture,
                "http://+:{0}/{1}",
                port,
                root
            );
            _serverHandle = WebApp.Start(
                _listeningAddress,
                appBuilder => _startUp.Configuration(appBuilder)
            );

            var publishAddress = _listeningAddress.Replace(
                "+",
                FabricRuntime.GetNodeContext().IPAddressOrFQDN
            );

            ServiceEventSource.Current.Message("Listening on {0}", publishAddress);
            return Task.FromResult(publishAddress);
        }

        private void StopWebserver()
        {
            if(_serverHandle == null)
            {
                return;
            }
            try
            {
                _serverHandle.Dispose();
            }
            catch(ObjectDisposedException err) {  }
        }
    }
}
