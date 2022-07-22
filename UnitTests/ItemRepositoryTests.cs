using System.Collections.Generic;
using System.Threading.Tasks;
using Api;
using NUnit.Framework;

namespace UnitTests
{
	public class ItemRepositoryTests
	{
		private ItemRepository _repository;

		[SetUp]
		public void SetUp()
		{
			_repository = new ItemRepository();
		}

		[Test]
		public async Task Fetches_item()
		{
			var item = await _repository.FetchItem("cd");
			Assert.That(item.Price, Is.EqualTo(456));
			Assert.That(item.Description, Is.EqualTo("Hairbrush"));
		}

		[Test]
		public async Task Returns_null_when_item_does_not_exist()
		{
			var item = await _repository.FetchItem("not-an-item");
			Assert.That(item, Is.Null);
		}

		[Test]
		public void Does_not_throw_for_valid_checkout()
		{
			var request = new CheckoutRequest{ Items = new List<string> { "ef" }, PaymentType = "voucher"};
			Assert.DoesNotThrowAsync(() => _repository.Checkout(request));
		}

		[Test]
		public void Throws_exception_when_item_does_not_exist()
		{
			var request = new CheckoutRequest {Items = new List<string> {"ef", "nope"}, PaymentType = "voucher"};
			var e = Assert.ThrowsAsync<CheckoutException>(() => _repository.Checkout(request));
			Assert.That(e.Message, Is.EqualTo("Item 'nope' does not exist"));
		}

		[Test]
		public void Throws_exception_when_payment_type_is_invalid()
		{
			var request = new CheckoutRequest {Items = new List<string> {"ef", "ab" }, PaymentType = "something else"};
			var e = Assert.ThrowsAsync<CheckoutException>(() => _repository.Checkout(request));
			Assert.That(e.Message, Is.EqualTo("Payment type 'something else' is not valid"));
		}
	}
}