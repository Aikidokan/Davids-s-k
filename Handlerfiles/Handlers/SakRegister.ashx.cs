using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handlerfiles.Handlers
{
	/// <summary>
	/// Summary description for SakRegister
	/// </summary>
	public class SakRegister : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write("Hello World");
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}