using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace Api.Nancy
{
	public class ErrorResponseHandler : IApplicationStartup
	{
		public void Initialize(IPipelines pipelines)
		{
			pipelines.OnError.AddItemToEndOfPipeline((context, exception) =>
			{
				return new Negotiator(context).WithStatusCode(HttpStatusCode.InternalServerError)
				.WithModel(new Error { Message = "Unexpected internal server error" });
			});
		}
	}
}