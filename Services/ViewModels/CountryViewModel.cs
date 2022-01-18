using System.Collections.Generic;

namespace Services.ViewModels
{
    public class CountryViewModel
    {
        public string CountryName { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<SalesViewModel> Sales { get; set; }
    }
}