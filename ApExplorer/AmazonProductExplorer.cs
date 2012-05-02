using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ApExplorer.Amazon.ECS;

namespace ApExplorer
{

	public enum Region
	{
		US,
		CA,
		UK,
		DE,
		FR,
		JP
	}

	public class AmazonProductExplorer
	{

		public string AccessKeyId { get; set; }
		public string SecretKey { get; set; }
		public string AssociateTag { get; set; }

		private Region _region;

		public string Destination { get; private set; }

		//      US: ecs.amazonaws.com 
		//      CA: ecs.amazonaws.ca 
		//      UK: ecs.amazonaws.co.uk 
		//      DE: ecs.amazonaws.de 
		//      FR: ecs.amazonaws.fr 
		//      JP: ecs.amazonaws.jp

		public Region Region
		{
			get
			{
				return _region;
			}
			set
			{
				_region = value;
				switch (Region)
				{
					case Region.CA:
						Destination = "https://ecs.amazonaws.ca/onca/soap?Service=AWSECommerceService";
						break;
					case Region.UK:
						Destination = "https://ecs.amazonaws.co.uk/onca/soap?Service=AWSECommerceService";
						break;
					case Region.DE:
						Destination = "https://ecs.amazonaws.de/onca/soap?Service=AWSECommerceService";
						break;
					case Region.FR:
						Destination = "https://ecs.amazonaws.fr/onca/soap?Service=AWSECommerceService";
						break;
					case Region.JP:
						Destination = "https://ecs.amazonaws.jp/onca/soap?Service=AWSECommerceService";
						break;
					case Region.US:
					default:
						Destination = "https://ecs.amazonaws.com/onca/soap?Service=AWSECommerceService";
						break;
				}

				_EndPointAddress = new EndpointAddress(Destination);
			}
		}

		public BasicHttpBinding _BasicHttpBinding { get; set; }
		public EndpointAddress _EndPointAddress { get; set; }

		public AmazonProductExplorer(string accessKeyId, string secretKey, string associateTag)
			: this(Region.US, accessKeyId, secretKey, associateTag)
		{ }

		public AmazonProductExplorer(Region region, string accessKeyId, string secretKey, string associateTag)
		{
			Region = region;
			AccessKeyId = accessKeyId;
			SecretKey = secretKey;
			AssociateTag = associateTag;

			_BasicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
		}

		public ItemSearchResponse SearchItemByTitle(string searchIndex, string title)
		{
			return SearchItemByTitle(searchIndex, title, new string[] { "Small" });
		}

		public ItemSearchResponse SearchItemByTitle(string searchIndex, string title, string[] responseGroup)
		{
			ItemSearchRequest searchRequest = new ItemSearchRequest();
			searchRequest.SearchIndex = searchIndex;
			searchRequest.Title = title;

			return SearchItem(searchRequest, responseGroup);
		}

		public ItemSearchResponse SearchItem(ItemSearchRequest itemSearchRequest)
		{
			return SearchItem(itemSearchRequest, new string[] { "Small" });
		}

		public ItemSearchResponse SearchItem(ItemSearchRequest itemSearchRequest, string[] responseGroup)
		{
			itemSearchRequest.ResponseGroup = responseGroup;

			ItemSearch itemSearch = new ItemSearch();
			itemSearch.Request = new ItemSearchRequest[] { itemSearchRequest };
			itemSearch.AWSAccessKeyId = AccessKeyId;
			itemSearch.AssociateTag = AssociateTag;

			using (AWSECommerceServicePortTypeClient client =
									new AWSECommerceServicePortTypeClient(_BasicHttpBinding, _EndPointAddress))
			{
				client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(AccessKeyId, SecretKey));

				return client.ItemSearch(itemSearch);
			}
		}
	}
}

