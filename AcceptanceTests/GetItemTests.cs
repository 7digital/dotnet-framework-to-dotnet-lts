using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AcceptanceTests
{
	public class GetItemTests
	{
		[Test]
		public async Task Fetches_an_item_in_xml()
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
			var response = await httpClient.GetAsync("http://checkout-api.7digital.local/item/ab");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			var body = await response.Content.ReadAsStringAsync();
			Assert.That(body, Is.EqualTo("<?xml version=\"1.0\"?>\r\n<Item xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <Description>Shampoo</Description>\r\n  <Price>123</Price>\r\n</Item>"));
		}

		[Test]
		public async Task Fetches_an_item_in_json()
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			var response = await httpClient.GetAsync("http://checkout-api.7digital.local/item/ab");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			var body = await response.Content.ReadAsStringAsync();
			Assert.That(body, Is.EqualTo("{\"description\":\"Shampoo\",\"price\":123}"));
		}

		[Test]
		public async Task Returns_a_generic_500_when_item_breaks()
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			var response = await httpClient.GetAsync("http://checkout-api.7digital.local/item/dodgy");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

			var body = await response.Content.ReadAsStringAsync();
			Assert.That(body, Is.EqualTo("{\"message\":\"Unexpected internal server error\"}"));
		}

		[Test]
		public async Task Returns_a_404_when_item_does_not_exist()
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			var response = await httpClient.GetAsync("http://checkout-api.7digital.local/item/not-an-item");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

			var body = await response.Content.ReadAsStringAsync();
			Assert.That(body, Is.EqualTo("{\"message\":\"Item does not exist\"}"));
		}

		[Test]
		public async Task Returns_a_404_when_route_does_not_exist()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync("http://checkout-api.7digital.local/not-a-route");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

			var body = await response.Content.ReadAsStringAsync();
			Assert.That(body, Is.EqualTo("Not found"));
		}
	}
}