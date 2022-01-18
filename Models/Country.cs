using System.Collections.Generic;

using Models.BaseModels;

namespace Models
{
    public class Country : BaseModel
    {
        public Country() : base()
        {
        }
        public string CountryName { get; set; }
        public string RegionId { get; set; }
        public Region Region { get; set; }
        public List<Order> Orders { get; set; }
    }
}