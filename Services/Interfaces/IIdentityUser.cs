using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIdentityUser
    {
        public Task<string> Register(UserViewModel model);
        public Task<string> Login(UserViewModel model);
    }
}
