using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace Api
{
	public class CheckoutModule : NancyModule
	{
		private readonly IItemRepository _itemRepository;

		public CheckoutModule(IItemRepository itemRepository)
		{
			_itemRepository = itemRepository;

			Post["/checkout", runAsync: true] = async (parameters, token) =>
			{
				var request = this.Bind<CheckoutRequest>();
				try
				{
					await Purchase(request);
				}
				catch (CheckoutException)
				{
					return HttpStatusCode.BadRequest;
				}

				return HttpStatusCode.OK;
			};
		}

		private async Task Purchase(CheckoutRequest request)
		{
			await _itemRepository.Checkout(request);
		}
	}
}