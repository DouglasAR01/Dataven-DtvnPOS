using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtvnPOS.SchemaClasses
{
    public class Schema
    {
        public Head SchemaHeader { get; set; }
        public Content SchemaContent { get; set; }
        public Footer SchemaFooter { get; set; }
    }
}
