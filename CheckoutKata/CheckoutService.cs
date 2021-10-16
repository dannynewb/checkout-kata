using CheckoutKata.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
	public class CheckoutService
	{
		private readonly IItemPricesRepository itemPricesRepository;
		private readonly IOfferCalculator offerCalculator;
		private readonly List<Item> basket = new List<Item>();

		public CheckoutService(IItemPricesRepository itemPricesRepository, IOfferCalculator offerCalculator)
		{
			this.itemPricesRepository = itemPricesRepository ?? throw new System.ArgumentNullException(nameof(itemPricesRepository));
			this.offerCalculator = offerCalculator ?? throw new System.ArgumentNullException(nameof(offerCalculator));
		}

		public double AddToBasket(params string[] items)
		{
			foreach (string item in items)
			{
				var itemAndPrice = this.itemPricesRepository.GetPrice(item);
				if (itemAndPrice == null)
				{
					throw new ItemNotFoundException(item);
				}

				this.basket.Add(itemAndPrice);
			}

			var totalCost = this.CalculateTotalItemCost();

			totalCost -= this.offerCalculator.CalculateTotalPriceReduction(this.basket);

			return totalCost;
		}

		private double CalculateTotalItemCost()
		{
			var totalCost = 0.0;

			foreach (var item in this.basket)
			{
				totalCost += item.Price;
			}

			return totalCost;
		}
	}
}
