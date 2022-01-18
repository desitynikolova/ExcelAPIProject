using System;

using Models.Interfaces;

namespace Models.BaseModels
{
    public class BaseModel : IAuditInfo
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}