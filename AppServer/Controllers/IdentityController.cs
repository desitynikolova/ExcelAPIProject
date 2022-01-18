using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IIdentityUser Identityuser { get; }
        public IdentityController(IIdentityUser identityuser)
        {
            Identityuser = identityuser;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Identityuser.Register(model).Result;
                if (result.Length > 0)
                {
                    return Ok(result);
                }
                return BadRequest("Register attempt is failed. Check email and password!");
            }
            return BadRequest("Incorrect register!");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserViewModel request)
        {
            var result = Identityuser.Login(request).Result;
            if (result.Length > 0)
            {
                Request.Headers.Add("Authorization", result);
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
