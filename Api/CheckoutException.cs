using System;

namespace Api
{
	public class CheckoutException : Exception
	{
		public CheckoutException(string message) : base(message)
		{
		}
	}
}