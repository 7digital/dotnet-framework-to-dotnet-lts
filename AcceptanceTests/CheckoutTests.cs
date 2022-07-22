using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AcceptanceTests
{
	public class CheckoutTests
	{
		[Test]
		public async Task Buys_items_using_voucher()
		{
			var httpClient = new HttpClient();
			var items = new
			{
				items = new [] { "ab", "ef" },
				paymentType = "voucher"
			};

			var body = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://checkout-api.7digital.local/checkout", body);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public async Task Returns_400_for_unknown_payment_type()
		{
			var httpClient = new HttpClient();
			var items = new
			{
				items = new [] { "ab", "ef" },
				paymentType = "cash"
			};

			var body = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://checkout-api.7digital.local/checkout", body);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task Returns_400_for_unknown_item()
		{
			var httpClient = new HttpClient();
			var items = new
			{
				items = new [] { "ab", "not-an-item", "ef" },
				paymentType = "voucher"
			};

			var body = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://checkout-api.7digital.local/checkout", body);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task Returns_500_for_unexpected_exception()
		{
			var httpClient = new HttpClient();
			var items = new
			{
				items = new [] { "ab", "dodgy", "ef" },
				paymentType = "voucher"
			};

			var body = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://checkout-api.7digital.local/checkout", body);
			var responseBody = await response.Content.ReadAsStringAsync();

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
			Assert.That(responseBody, Is.EqualTo("{\"message\":\"Unexpected internal server error\"}"));
		}
	}
}