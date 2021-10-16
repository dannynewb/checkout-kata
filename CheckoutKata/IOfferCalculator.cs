using CheckoutKata.Models;
using System.Collections.Generic;

namespace CheckoutKata
{
	public interface IOfferCalculator
	{
		double CalculateTotalPriceReduction(List<Item> items);
	}
}
