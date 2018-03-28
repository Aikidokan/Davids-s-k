using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakregister.Classes.Data
{
	public partial class CSImportDataContext
	{
		public CSImportDataContext() : base(System.Configuration.ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
	}
}
