using System;
using System.Web;

namespace Api
{
	public class Global : HttpApplication
	{
		protected void Application_Error(object sender, EventArgs e)
		{
			var ex = Context.Server.GetLastError();
		}
	}
}