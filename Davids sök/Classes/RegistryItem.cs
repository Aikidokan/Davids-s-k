using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sakregister.Classes
{
    public class RegistryItem
    {
        public string Nr1 { get; set; }
        public string Ar { get; set; }
        public string Ord { get; set; }
        public string Arende { get; set; }
        public string Betankande { get; set; }
        public string Skrivelse { get; set; }
        public string Protokoll { get; set; }
    }
}
/*

 ,[Ar]
      ,[Ord]
      ,[Arende]
      ,[Betankande]
      ,[Skrivelse]
      ,[Protokoll]
*/