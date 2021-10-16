using CheckoutKata.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CheckoutKata.Tests
{
	public class CheckoutServiceTests
	{
		private readonly Mock<IItemPricesRepository> mockItemPricesRepository = new Mock<IItemPricesRepository>();
		private readonly Mock<IOfferCalculator> mockOfferCalculator = new Mock<IOfferCalculator>();

		[Fact]
		public void GivenAnItemIsAddedToTheBasket_WhenThatItemDoesntExist_ThenAnExceptionIsThrown()
		{
			var checkoutService = new CheckoutService(this.mockItemPricesRepository.Object, this.mockOfferCalculator.Object);

			Assert.Throws<ItemNotFoundException>(() => checkoutService.AddToBasket("InvalidItem"));
		}

		[Theory]
		[InlineData("A", 10, "B", 15, "C", 40, 65)]
		[InlineData("A", 10, "B", 15, "D", 55, 80)]
		[InlineData("A", 10, "C", 40, "D", 55, 105)]
		public void GivenAnItemIsAddedToTheBasket_ThenTheTotalCostIsCorrectlyCalculated(
			string item1, 
			double item1Price,
			string item2, 
			double item2Price,
			string item3, 
			double item3Price,
			int expected)
		{
			this.mockItemPricesRepository.Setup(m => m.GetPrice(item1)).Returns(new Item { Name = item1, Price = item1Price });
			this.mockItemPricesRepository.Setup(m => m.GetPrice(item2)).Returns(new Item { Name = item2, Price = item2Price });
			this.mockItemPricesRepository.Setup(m => m.GetPrice(item3)).Returns(new Item { Name = item3, Price = item3Price });

			var checkoutService = new CheckoutService(this.mockItemPricesRepository.Object, this.mockOfferCalculator.Object);

			var totalCost = checkoutService.AddToBasket(item1, item2, item3);

			Assert.Equal(expected, totalCost);
		}

		[Fact]
		public void GivenAnOfferIsAvailableOnAnItemInTheBasket_ThenTheOfferTotalIsReducedFromTheBasketTotal()
		{
			this.mockItemPricesRepository.Setup(m => m.GetPrice("B")).Returns(new Item { Name = "B", Price = 15 });

			this.mockOfferCalculator.Setup(m => m.CalculateTotalPriceReduction(It.IsAny<List<Item>>()))
				.Returns(5);

			var checkout = new CheckoutService(this.mockItemPricesRepository.Object, this.mockOfferCalculator.Object);

			var totalCost = checkout.AddToBasket("B", "B", "B");

			Assert.Equal(expected: 40, totalCost);
		}

		[Fact]
		public void GivenItemsAreAlreadyInTheBasket_WhenMoreItemsAreAdded_TheTotalPriceGetsRecalculated()
		{
			this.mockItemPricesRepository.Setup(m => m.GetPrice("A")).Returns(new Item { Name = "A", Price = 10 });
			this.mockItemPricesRepository.Setup(m => m.GetPrice("B")).Returns(new Item { Name = "B", Price = 15 });
			this.mockItemPricesRepository.Setup(m => m.GetPrice("C")).Returns(new Item { Name = "C", Price = 40 });

			var checkoutService = new CheckoutService(this.mockItemPricesRepository.Object, this.mockOfferCalculator.Object);

			checkoutService.AddToBasket("A", "A", "A");

			var totalCost = checkoutService.AddToBasket("B");

			Assert.Equal(expected: 45, totalCost);
		}
	}
}
