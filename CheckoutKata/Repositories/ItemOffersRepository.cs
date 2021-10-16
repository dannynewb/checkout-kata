using CheckoutKata.Models;
using System.Collections.Generic;

namespace CheckoutKata.Repositories
{
	public class ItemOffersRepository : IItemOffersRepository
	{
		public List<Offer> GetOffers()
		{
			return new List<Offer>
			{
				new Offer
				{
					Item = new Item
					{
						Name = "B",
						Price = 15
					},
					Multiple = 3,
					PriceReduction = 5
				},
				new Offer
				{
					Item = new Item
					{
						Name = "D",
						Price = 55
					},
					Multiple = 2,
					PriceReduction = 27.50
				}
			};
		}
	}
}
