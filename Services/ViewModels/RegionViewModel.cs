using System.Collections.Generic;

namespace Services.ViewModels
{
    public class RegionViewModel
    {
        public string RegionName { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        public List<SalesViewModel> Sales { get; set; }
    }
}