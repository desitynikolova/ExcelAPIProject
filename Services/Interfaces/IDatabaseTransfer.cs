using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDatabaseTransfer
    {
        public Task TransferAsync(List<RegionViewModel> regions, string fileDate);
    }
}
