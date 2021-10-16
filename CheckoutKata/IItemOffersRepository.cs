using CheckoutKata.Models;
using System.Collections.Generic;

namespace CheckoutKata
{
	public interface IItemOffersRepository
	{
		List<Offer> GetOffers();
	}
}
