using System;

using Models.BaseModels;

namespace Models
{
    public class LastReadedFile : BaseModel
    {
        public LastReadedFile(): base()
        {
        }
        public DateTime LastRead { get; set; }
    }
}