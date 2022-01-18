using CsvHelper.Configuration.Attributes;
using Services.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Services.ViewModels
{
    public class TransferViewModel
    {
        [Name("Region")]
        [StringLength(Const.MAX_REGION_NAME_CHARS)]
        public string RegionName { get; set; }
        [Name("Country")]
        [StringLength(Const.MAX_COUNTRY_NAME_CHARS)]
        public string CountryName { get; set; }
        [Name("Item Type")]
        public string ItemType { get; set; }
        [Name("Sales Channel")]
        public string SalesChannel { get; set; }
        [Name("Order Priority")]
        public char OrderPrioriy { get; set; }
        [Name("Order Date")]
        public DateTime OrderDate { get; set; }
        [Name("Order ID")]
        public int OrderId { get; set; }
        [Name("Ship Date")]
        public DateTime ShipDate { get; set; }
        [Name("Units Sold")]
        public int UnitsSold { get; set; }
        [Name("Unit Price")]
        public decimal UnitPrice { get; set; }
        [Name("Unit Cost")]
        public decimal UnitCost { get; set; }
        [Name("Total Revenue")]
        public decimal TotalRevenue { get; set; }
        [Name("Total Cost")]
        public decimal TotalCost { get; set; }

        [Name("Total Profit")]
        public decimal TotalProfit { get; set; }
        [Ignore]
        public DateTime CreatedAt { get; set; }
        [Ignore]
        public DateTime FileDate { get; set; }
    }
}