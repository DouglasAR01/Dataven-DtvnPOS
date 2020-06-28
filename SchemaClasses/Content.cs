using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DtvnPOS.SchemaClasses
{
    public class Content
    {
        public List<Product> Products { get; set; }

        public Content()
        {
            Products = new List<Product>();
        }
    }
}
