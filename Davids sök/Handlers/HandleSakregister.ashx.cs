using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Sakregister.Classes;
using Sakregister.Classes.Data;

namespace Sakregister.Handlers
{
	/// <summary>
	/// Summary description for Handler1
	/// </summary>
	public class HandleSakregister : IHttpHandler
	{

		public void ProcessRequest( HttpContext context )
		{

			
			var jsonResponse = new SearchResult();
			if ( !string.IsNullOrEmpty(context.Request["Ord"]) )
			{
				var ord = context.Request["Ord"];
				jsonResponse = SearchForOrd(ord);
			}
			context.Response.ContentType = "text/json";
			var retval = JsonConvert.SerializeObject(jsonResponse);
			context.Response.Write(retval);
		}


		private SearchResult SearchForOrd( string s )
		{
			using ( var db = new CSImportDataContext() )
			{
				var result = new SearchResult
				             {
					             Items = (db.Sakregistrets.Where(x => x.Ord.Contains(s)).
					                         Select(
						                         x =>
							                         new RegistryItem
							                         {
								                         Ord = x.Ord ?? "",
								                         Ar = x.Ar ?? "",
								                         Arende = x.Arende ?? "",
								                         Betankande = x.Betankande ?? "",
								                         Id = x.Id ,
								                         Protokoll = x.Protokoll ?? "",
								                         Skrivelse = x.Skrivelse ?? ""
													 })).ToList()
				             };
				return result;
			}
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