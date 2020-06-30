using DtvnPOS.SchemaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESC_POS_USB_NET.Printer;
using DtvnPOS.PrinterClasses;
using DtvnPOS.Enums;
using DtvnPOS.Interfaces;

namespace DtvnPOS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string encoded = args[0].Substring(10);
                string decoded = UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded));
                Schema data = Newtonsoft.Json.JsonConvert.DeserializeObject<Schema>(decoded);
                try
                {
                    Printer pos = new Printer("POS-58");
                    ISchemaPrinter print = new SchemaPrinter(pos, PrinterPaperSize.P58MM);
                    print.Print(data);
                } catch
                {
                    //The only possible exception using the above code is "Unable to access printer".
                    Console.WriteLine("No se encontró la impresora POS.");
                }    
                
            } else
            {
                Console.WriteLine("Error en el envío del parámetro.");
            }
        }
    }
}
