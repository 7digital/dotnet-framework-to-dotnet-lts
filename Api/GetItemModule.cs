using Api.Nancy;
using Nancy;

namespace Api
{
	public class GetItemModule : NancyModule
	{
		public GetItemModule(IItemRepository itemRepository)
		{
			Get["/item/{code}", runAsync: true] = async (parameters, token) =>
			{
				var item = await itemRepository.FetchItem((string)parameters.code);

				if (item == null)
				{
					return Negotiate.WithStatusCode(404).WithModel(new Error { Message = "Item does not exist" });
				}
				return Negotiate.WithStatusCode(200).WithModel(item);
			};
		}
	}
}