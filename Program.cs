using DtvnPOS.SchemaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESC_POS_USB_NET.Printer;
using DtvnPOS.PrinterClasses;
using DtvnPOS.Enums;

namespace DtvnPOS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string encoded = args[0].Substring(10);
                string decoded = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(encoded));
                Schema data = Newtonsoft.Json.JsonConvert.DeserializeObject<Schema>(decoded);

                //Inicio de impresión
                Printer pos = new Printer("POS-58");
                SchemaPrinter print = new SchemaPrinter(pos, PrinterPaperSize.P58MM);
                print.Print(data);
            } else
            {
                Console.WriteLine("Error en el envío del parámetro.");
            }
        }
    }
}
