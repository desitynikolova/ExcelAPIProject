using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ModelServices.Interfaces
{
    public interface ICountryService
    {
        public List<CountryViewModel> GetAllCountries();
        public List<CountryViewModel> GetCountryByRegion(string id);
        public Dictionary<string, int> GetTotalOrdersByCountry();
        public CountryViewModel SearchCountryByName(string name);
    }
}
