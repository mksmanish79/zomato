using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zomato.Factories.Interfaces;
using zomato.Model;

namespace zomato.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserFactory _userFactory;
        public UserController(ILogger<UserController> logger, IUserFactory userFactory)
        {
            _logger = logger;
            _userFactory = userFactory;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(TRegister model)
        {
            return Ok(await _userFactory.RegisterUser(model));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(TRegister model)
        {
            return Ok(await _userFactory.RegisterUser(model));
        }
    }
}
