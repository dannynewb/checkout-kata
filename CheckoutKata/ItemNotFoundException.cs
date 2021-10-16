using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutKata
{
	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException(string item) : base($"Item not found: {item}") { }
	}
}
