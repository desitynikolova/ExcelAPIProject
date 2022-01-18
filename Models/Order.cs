using System;
using System.Collections.Generic;

using Models.BaseModels;

namespace Models
{
    public class Order : BaseModel
    {
        public Order() : base()
        {
        }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public char OrderPrioriy { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public List<Sales> Sales { get; set; }
    }
}