using Models;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransfer
    {
        public List<RegionViewModel> ModelHandler(List<TransferViewModel> models);
    }
}
