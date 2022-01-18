using Models.BaseModels;

namespace Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}