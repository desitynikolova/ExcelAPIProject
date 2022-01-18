using AutoMapper;
using System.Collections.Generic;
using System.Linq;

using Date;
using Models;
using Services.ModelServices.Interfaces;
using Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services.ModelServices.Implementation
{
    public class RegionService : IRegionService
    {
        public RegionService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            ApplicationDbContext = applicationDbContext;
            Mapper = mapper;
        }

        public ApplicationDbContext ApplicationDbContext { get; }
        public IMapper Mapper { get; }

        public List<RegionViewModel> GetAllRegions()
        {
            List<Region> region = ApplicationDbContext.Regions
                                  .Select(x => new Region()
                                  {
                                      RegionName = x.RegionName
                                  }).ToList();

            List<RegionViewModel> regions = Mapper.Map<List<RegionViewModel>>(region);
            return regions;
        }

        public RegionViewModel SearchRegionByName(string name)
        {
            var region = ApplicationDbContext.Regions.Include(x=>x.Countries).FirstOrDefault(x => x.RegionName.ToUpper() == name.ToUpper());

            var result = Mapper.Map<RegionViewModel>(region);

            return result;
        }

        // Get sales by total profit
        public List<SalesViewModel> TopSalesByTotalProfit()
        {
            var sales = ApplicationDbContext.Sales.GroupBy(x => x.TotalProfit).ToList();

            var result = Mapper.Map<List<SalesViewModel>>(sales);
            return result;
        }

    }
}
