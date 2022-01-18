using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ModelServices.Interfaces;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class CountryConroller : ControllerBase
    {
        public CountryConroller(ICountryService countryService)
        {
            CountryService = countryService;
        }
        public ICountryService CountryService { get; }

        [HttpGet]
        [Route("countries")]
       // [AllowAnonymous]
        public IActionResult GetCompanies()
        {
            var result = CountryService.GetAllCountries();
            return Ok(result);
        }

        [HttpGet]
        [Route("countryByName")]
        public IActionResult GetCountryByName(string name)
        {
            var result = CountryService.SearchCountryByName(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("totalOrder")]
        public IActionResult GetTotalOrdersByCountry()
        {
            var result = CountryService.GetTotalOrdersByCountry();
            return Ok(result);
        }

        [HttpGet]
        [Route("countryByRegionName")]
        public IActionResult GetCountryByRegion(string name)
        {
            var result = CountryService.GetCountryByRegion(name);

            return Ok(result);
        }
    }
}