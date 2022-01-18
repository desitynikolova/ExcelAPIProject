using System;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class OrderViewModel
    {
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public char OrderPrioriy { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public int UnitsSold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalProfit { get; set; }
        public List<SalesViewModel> Sales { get; set; }
    }
}