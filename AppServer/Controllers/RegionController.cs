using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ModelServices.Interfaces;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        public RegionController(IRegionService region)
        {
            Region = region;

        }
        public IRegionService Region { get; }

        [HttpGet]
        public IActionResult GetRegions()
        {
            var result = Region.GetAllRegions();
            return Ok(result);
        }

        [HttpGet]
        [Route("regionByName")]
        public IActionResult GetRegionByName(string name)
        {
            var result = Region.SearchRegionByName(name);
            return Ok(result);
        }
    }
}
