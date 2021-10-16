using CheckoutKata.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
	public class OfferCalculator : IOfferCalculator
	{
		private readonly IItemOffersRepository itemOffersRepository;

		public OfferCalculator(IItemOffersRepository itemOffersRepository)
		{
			this.itemOffersRepository = itemOffersRepository ?? throw new System.ArgumentNullException(nameof(itemOffersRepository));
		}

		public double CalculateTotalPriceReduction(List<Item> items)
		{
			var totalPriceReduction = 0.00;

			var itemOffers = this.itemOffersRepository.GetOffers();
			foreach (var offer in itemOffers)
			{
				var offerItemsInBasket = items.Where(i => i.Name == offer.Item.Name);
				if (offerItemsInBasket.Count() == 0)
				{
					continue;
				}

				var multiplesInBasket = offerItemsInBasket.Count() / offer.Multiple;

				totalPriceReduction += multiplesInBasket * offer.PriceReduction;
			}

			return totalPriceReduction;
		}
	}
}
