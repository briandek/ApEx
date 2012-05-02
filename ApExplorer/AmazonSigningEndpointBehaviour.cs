using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace ApExplorer
{
	public class AmazonSigningEndpointBehavior : IEndpointBehavior
	{
		private string accessKeyId = "";
		private string secretKey = "";

		public AmazonSigningEndpointBehavior(string accessKeyId, string secretKey)
		{
			this.accessKeyId = accessKeyId;
			this.secretKey = secretKey;
		}

		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new AmazonSigningMessageInspector(accessKeyId, secretKey));
		}

		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher) { return; }
		public void Validate(ServiceEndpoint serviceEndpoint) { return; }
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters) { return; }
	}
}
