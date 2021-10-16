using CheckoutKata.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
	public class Checkout
	{
		private readonly IItemPricesRepository itemPricesRepository;
		private readonly IOfferCalculator offerCalculator;
		private readonly List<Item> basket = new List<Item>();

		public Checkout(IItemPricesRepository itemPricesRepository, IOfferCalculator offerCalculator)
		{
			this.itemPricesRepository = itemPricesRepository ?? throw new System.ArgumentNullException(nameof(itemPricesRepository));
			this.offerCalculator = offerCalculator ?? throw new System.ArgumentNullException(nameof(offerCalculator));
		}

		public double AddToBasket(params string[] items)
		{
			var totalCost = this.CalculateTotalItemCost(items.ToList());

			totalCost -= this.offerCalculator.CalculateTotalPriceReduction(this.basket);

			return totalCost;
		}

		private double CalculateTotalItemCost(List<string> items)
		{
			var totalCost = 0.0;

			foreach (string item in items)
			{
				var itemAndPrice = this.itemPricesRepository.GetPrice(item);
				if (itemAndPrice == null)
				{
					throw new ItemNotFoundException(item);
				}

				totalCost += itemAndPrice.Price;
			}

			return totalCost;
		}
	}
}
