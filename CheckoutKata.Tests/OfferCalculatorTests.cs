using CheckoutKata.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CheckoutKata.Tests
{
	public class OfferCalculatorTests
	{
		private readonly Mock<IItemOffersRepository> mockItemOffersRepository = new Mock<IItemOffersRepository>();

		[Theory]
		[InlineData("B", 15, 1, 3, 5, 0)]
		[InlineData("B", 15, 3, 3, 5, 5)]
		[InlineData("B", 15, 6, 3, 5, 10)]
		[InlineData("B", 15, 5, 3, 5, 5)]
		[InlineData("D", 55, 1, 2, 27.50, 0)]
		[InlineData("D", 55, 2, 2, 27.50, 27.50)]
		[InlineData("D", 55, 4, 2, 27.50, 55)]
		[InlineData("D", 55, 3, 2, 27.50, 27.50)]
		public void GivenAnOfferIsAvailable_ThenTheExpectedTotalPriceReductionIsReturned(
			string itemName, 
			int itemPrice, 
			int amountOfItem,
			int offerMultiple, 
			double offerPriceReduction,
			double expectedPriceReduction)
		{
			this.mockItemOffersRepository.Setup(m => m.GetOffers())
				.Returns(new List<Offer>
				{
					this.CreateOffer(this.CreateItem(itemName, itemPrice), offerMultiple, offerPriceReduction)
				});

			var offerCalculator = new OfferCalculator(this.mockItemOffersRepository.Object);

			var basketItems = new List<Item>();
			for (int i = 0; i < amountOfItem; i++)
			{
				basketItems.Add(this.CreateItem(itemName, itemPrice));
			}

			var totalPriceReduction = offerCalculator.CalculateTotalPriceReduction(basketItems);

			Assert.Equal(expectedPriceReduction, totalPriceReduction);
		}

		[Fact]
		public void GivenMultipleOffersAreAvailable_ThenTheExpectedTotalPriceReductionIsReturned()
		{
			this.mockItemOffersRepository.Setup(m => m.GetOffers())
				.Returns(new List<Offer>
				{
					this.CreateOffer(this.CreateItem("B", 15), 3, 5),
					this.CreateOffer(this.CreateItem("D", 55), 2, 27.50)
				});

			var offerCalculator = new OfferCalculator(this.mockItemOffersRepository.Object);

			var totalPriceReduction = offerCalculator.CalculateTotalPriceReduction(
				new List<Item>
				{
					this.CreateItem("B", 15),
					this.CreateItem("B", 15),
					this.CreateItem("B", 15),
					this.CreateItem("D", 55),
					this.CreateItem("D", 55),
				});

			Assert.Equal(expected: 32.50, totalPriceReduction);
		}

		[Fact]
		public void GivenThereAreNoItemsInTheBasketWithOffers_ThenNoPriceIsDeducted()
		{
			this.mockItemOffersRepository.Setup(m => m.GetOffers())
				.Returns(new List<Offer>
				{
					this.CreateOffer(this.CreateItem("D", 55), 2, 27.50)
				});

			var offerCalculator = new OfferCalculator(this.mockItemOffersRepository.Object);

			var totalPriceReduction = offerCalculator.CalculateTotalPriceReduction(
				new List<Item>
				{
					this.CreateItem("B", 15),
					this.CreateItem("B", 15),
				});

			Assert.Equal(expected: 0, totalPriceReduction);
		}

		private Item CreateItem(string name, double price)
		{
			return new Item
			{
				Name = name,
				Price = price
			};
		}

		private Offer CreateOffer(Item item, int offerMultiple, double priceReduction)
		{
			return new Offer
			{
				Item = item,
				Multiple = offerMultiple,
				PriceReduction = priceReduction
			};
		}
	}
}
