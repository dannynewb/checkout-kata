using CheckoutKata.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Repositories
{
	public class ItemPricesRepository : IItemPricesRepository
	{
		private readonly List<Item> Items = new List<Item>
		{
			new Item
			{
				Name = "A",
				Price = 10
			},
			new Item
			{
				Name = "B",
				Price = 15
			},
			new Item
			{
				Name = "C",
				Price = 40
			},
			new Item
			{
				Name = "D",
				Price = 55
			}
		};

		public Item GetPrice(string itemName)
		{
			return this.Items.FirstOrDefault(i => i.Name == itemName);
		}
	}
}
