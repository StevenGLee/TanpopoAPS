using System.Collections.Generic;
using System;

namespace TanpopoWeb.Data
{
    public class ProductionItem
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Machine { get; set; }
        public string Order { get; set; }
        public int? Quantity { get; set; }
        public string Type { get; set; }
    }
}