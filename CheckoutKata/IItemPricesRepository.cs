using CheckoutKata.Models;

namespace CheckoutKata
{
	public interface IItemPricesRepository
	{
		Item GetPrice(string itemName);
	}
}
