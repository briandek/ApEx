using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApExplorer.Amazon.ECS;

namespace ApExplorer.Tests
{
	[TestClass]
	public class AmazonProductExplorerTests
	{
		private AmazonProductExplorer apex;

		[TestInitialize]
		public void Setup()
		{
			apex = new AmazonProductExplorer("", "", "");
		}
		[TestMethod]
		public void GetABookTest()
		{
			apex._BasicHttpBinding.MaxReceivedMessageSize = 99999;

			string[] responseGroup = new string[] { "ItemAttributes", "Small" };

			ItemSearchResponse response = apex.SearchItemByTitle("Books", "WCF", responseGroup);

			Assert.IsTrue(0 < response.Items.Sum(i => Int32.Parse(i.TotalResults)));

			var responseList = response.Items.SelectMany(i => i.Item);

			foreach (Item item in responseList)
			{
				System.Diagnostics.Debug.WriteLine(item.ASIN + " : " + item.ItemAttributes.Title);
			}

			System.Diagnostics.Debug.WriteLine(response.Items.Sum(i => Int32.Parse(i.TotalResults)));

		}
	}
}
