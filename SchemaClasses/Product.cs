using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtvnPOS.SchemaClasses
{
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }
    }
}
