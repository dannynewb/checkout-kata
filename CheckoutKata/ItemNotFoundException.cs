using System;

namespace CheckoutKata
{
	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException(string item) : base($"Item not found: {item}") { }
	}
}
