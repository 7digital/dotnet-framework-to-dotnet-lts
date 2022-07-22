using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api
{
	public class CheckoutRequest
	{
		[JsonProperty("items")]
		public List<string> Items { get; set; }

		[JsonProperty("paymentType")]
		public string PaymentType { get; set; }
	}
}