using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtvnPOS.Enums;
using DtvnPOS.Interfaces;
using DtvnPOS.SchemaClasses;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;

namespace DtvnPOS.PrinterClasses
{
    public class SchemaPrinter : ISchemaPrinter
    {
        // Printer object
        public Printer Pos { get; set; }
        // Max number of characters per line.
        // If string lenght > dots the remaining dots will be in a new line
        public int Dots { get; set; }

        public SchemaPrinter(Printer printer, PrinterPaperSize size)
        {
            Pos = printer;

            switch (size)
            {
                case PrinterPaperSize.P58MM:
                    Dots = 32;
                    break;
                case PrinterPaperSize.P80MM:
                    Dots = 48;
                    break;
            }
        }

        public void Print(Schema data)
        {
            AppendHeader(data.SchemaHeader);
            AppendContent(data.SchemaContent);
            AppendFooter(data.SchemaFooter);
            AppendBloat();
            Pos.FullPaperCut();
            Pos.PrintDocument();
        }

        private void AppendHeader(Head header)
        {
            Pos.AlignCenter();
            Pos.BoldMode(header.BusinessName);
            Pos.Append(header.BusinessAddress);
            Pos.Append(header.BusinessID);
            Pos.BoldMode(header.BillID.ToString());
            Pos.AlignLeft();
            Pos.Append(System.DateTime.Today.ToString("g"));
            Pos.Separator();
        }

        private void AppendContent(Content content)
        {
            // Hardcoded table header. It needs to be changed to support multiple languages.
            // Exactly 42 dots.
            string tableHeader = "Descripción         Cantidad         Valor";
            Pos.CondensedMode(PrinterModeState.On);
            Pos.Append(tableHeader);
            foreach (Product product in content.Products)
            {
                Pos.Append(MakeProductLine(product));
            }
            Pos.CondensedMode(PrinterModeState.Off);
            Pos.Separator();
        }

        private void AppendFooter(Footer footer)
        {
            Pos.AlignCenter();
            Pos.Append(footer.BusinessWS);
            Pos.Append(footer.BusinessEmail);
            Pos.Append(footer.BusinessTel);
            Pos.AlignLeft();
            Pos.Separator();
        }

        private void AppendBloat()
        {
            Pos.AlignCenter();
            Pos.Append("Dataven IS");
            Pos.AlignLeft();
            Pos.Separator();
        }

        private string MakeProductLine(Product product)
        {
            // 42 dots in 58MM printers and 64(?) in 80MM printers.
            int lineDots = (Dots == 32) ? 42 : 64;

            float totalPrice = product.ProductQuantity * product.ProductPrice;
            string stringQuantity = product.ProductQuantity.ToString();
            string stringTotalPrice = totalPrice.ToString();

            int nameLimit = (int)(lineDots * 0.45);
            int wsLimit = (int)(lineDots * 0.05);
            int quantityLimit = (int)(lineDots * 0.1);
            int totalPriceLimit = (int)(lineDots * 0.35)-1;

            string wsString = "";
            for (int i = 0; i<wsLimit; i++)
            {
                wsString += " ";
            }
            
            string nameString = 
                (product.ProductName.Length>nameLimit) ? product.ProductName.Substring(0, nameLimit) : product.ProductName;

            string quantityString = (stringQuantity.Length > quantityLimit) ? "+ 1K" : stringQuantity;

            string totalPriceString = "$" + ((stringTotalPrice.Length > totalPriceLimit) ? "+999999999999" : stringTotalPrice);
            return nameString+wsString+quantityString+wsString+totalPriceString;
        }
    }
}
