using DtvnPOS.SchemaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtvnPOS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio de prueba \n");
            if (args.Length == 1)
            {
                string encoded = args[0].Substring(10);
                string decoded = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(encoded));
                Schema data = Newtonsoft.Json.JsonConvert.DeserializeObject<Schema>(decoded);
            }
            Console.WriteLine("\nFin de prueba");
            Console.ReadLine();
        }
    }
}
