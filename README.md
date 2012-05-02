ApEx(plorer) //Learning to use Amazon Product API
=========

A very simple library to grab data from Amazon Product API.

Initial howtotalktoAmzProductApi built  by [Oren Trutner](http://flyingpies.wordpress.com/2009/08/01/17/)

Sample:
=========
	class Program
	{
		class Program
	{
		static void Main(string[] args)
		{
			AmazonProductExplorer apEx = new AmazonProductExplorer("AccessKeyID", "SecretKey", "AssociateTag");

			string[] responseGroup = new string[] { "ItemAttributes", "Small" };

			ItemSearchResponse response = apEx.SearchItemByTitle("Books", "WCF", responseGroup);

			var responseList = response.Items.SelectMany(i => i.Item);

			foreach (Item item in responseList)
			{
				Console.WriteLine(item.ASIN + " : " + item.ItemAttributes.Title);
			}

			Console.WriteLine("Total Results : " + response.Items.Sum(i => Int32.Parse(i.TotalResults)));
		}
	}




