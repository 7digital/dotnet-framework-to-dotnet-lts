using System.Text;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Routing;

namespace Api.Nancy
{
	public class StatusCode404Handler : IStatusCodeHandler
	{
		public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
		{
			return statusCode == HttpStatusCode.NotFound;
		}

		public void Handle(HttpStatusCode statusCode, NancyContext context)
		{
			context.Response.WithStatusCode(HttpStatusCode.NotFound);

			if (context.ResolvedRoute is NotFoundRoute)
			{
				context.Response.Contents = s =>
					s.Write(Encoding.UTF8.GetBytes("Not found"), 0, Encoding.UTF8.GetBytes("Not found").Length);
			}
		}
	}
}