using Xunit;

namespace CheckoutKata.Tests
{
	public class CheckoutTests
	{
		[Fact]
		public void GivenAnItemIsAddedToTheBasket_WhenThatItemDoesntExist_ThenAnExceptionIsThrown()
		{
			var checkout = new Checkout(new ItemPrices());

			Assert.Throws<ItemNotFoundException>(() => checkout.AddItemsToBasket("InvalidItem"));
		}

		[Theory]
		[InlineData("A", "B", "C", 65)]
		[InlineData("A", "B", "D", 80)]
		[InlineData("A", "C", "D", 105)]
		public void GivenAnItemIsAddedToTheBasket_ThenTheTotalCostIsCorrectlyCalculated(string item1, string item2, string item3, int expected)
		{
			var checkout = new Checkout(new ItemPrices());

			var totalCost = checkout.AddItemsToBasket(item1, item2, item3);

			Assert.Equal(expected, totalCost);
		}
	}
}
