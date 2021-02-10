using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtvnPOS.Helpers
{
    public class Languages
    {
        public static string GetString(string key)
        {
            var lang = ConfigurationManager.AppSettings["lang"];
            switch (lang)
            {
                case "es":

                    break;

                default:

                    break;
            }
            return "";
        }
    }
}
