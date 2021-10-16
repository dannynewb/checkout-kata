using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
	public class Checkout
	{
		private readonly ItemPrices itemPrices;
		private readonly List<Item> basket = new List<Item>();

		public Checkout(ItemPrices itemPrices)
		{
			this.itemPrices = itemPrices ?? throw new System.ArgumentNullException(nameof(itemPrices));
		}

		public int AddItemsToBasket(params string[] items)
		{
			var totalCost = 0;

			foreach (string item in items)
			{
				var itemAndPrice = this.itemPrices.ItemsAndPrices.FirstOrDefault(i => i.Name == item);
				if (itemAndPrice == null)
				{
					throw new ItemNotFoundException(item);
				}

				this.basket.Add(itemAndPrice);

				totalCost += itemAndPrice.Price;
			}

			return totalCost;
		}
	}
}
