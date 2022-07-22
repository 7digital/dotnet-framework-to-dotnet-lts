using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api
{
	public interface IItemRepository
	{
		Task<Item> FetchItem(string code);
		Task Checkout(CheckoutRequest request);
	}

	public class ItemRepository : IItemRepository
	{
		private readonly Dictionary<string, Item> _items = new Dictionary<string, Item>
		{
			{"ab", new Item {Description = "Shampoo", Price = 123}},
			{"cd", new Item {Description = "Hairbrush", Price = 456}},
			{"ef", new Item {Description = "Face cream", Price = 789}}
		};
		public async Task<Item> FetchItem(string code)
		{
			await Task.Delay(20); // Imagine it is fetching it from a DB or another service
			if (code == "dodgy")
			{
				throw new Exception("Something went wrong with this item");
			}

			if (_items.ContainsKey(code))
			{
				return _items[code];
			}

			return null;
		}

		public async Task Checkout(CheckoutRequest request)
		{
			if (request.PaymentType != "voucher")
			{
				throw new CheckoutException($"Payment type '{request.PaymentType}' is not valid");
			}

			foreach (var item in request.Items)
			{
				if (await FetchItem(item) == null)
				{
					throw new CheckoutException($"Item '{item}' does not exist");
				}
			}

			await StorePurchase();
		}

		private static async Task StorePurchase()
		{
			await Task.Delay(20); // Imagine it is calling a DB or another service
		}
	}
}