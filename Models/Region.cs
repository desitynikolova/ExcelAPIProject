using System.Collections.Generic;

using Models.BaseModels;

namespace Models
{
    public class Region : BaseModel
    {
        public Region() :base()
        {
        }
        public string RegionName { get; set; }
        public List<Country> Countries { get; set; }
    }
}