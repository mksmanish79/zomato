using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zomato.Factories.Interfaces;
using zomato.Model;
using zomato.Model.JWT;

namespace zomato.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserFactory _userFactory;
        private readonly IJwtAuth _jwtAuth;
        public UserController(ILogger<UserController> logger, IUserFactory userFactory, IJwtAuth jwtAuth)
        {
            _logger = logger;
            _userFactory = userFactory;
            _jwtAuth = jwtAuth;
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
            ApiResponse<int> result = await _userFactory.LoginUser(model);
            if (result.Status)
            {
                JWTTokenModel jWTTokenModel = await _jwtAuth.GetToken(Request, result.Result);
                if (jWTTokenModel.Token == null)
                    return Unauthorized();
                else
                    return Ok(jWTTokenModel);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken(JWTTokenModel jwtToken)
        {
            int Userid = _jwtAuth.GetID(Request);
            if (jwtToken != null && _jwtAuth.ValidateToken(Request, Userid))
            {
                if (await _jwtAuth.CheckIfRefreshTokenIsValid(Userid, jwtToken.RefreshToken))
                    return Ok(await _jwtAuth.GetToken(Request, Userid));
                else
                    return BadRequest("Invalid Request");
            }
            else
                return BadRequest("Invalid Request");

        }

        [HttpGet]
        [Route("get/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            if (_jwtAuth.ValidateToken(Request, userId))
                return Ok(await _userFactory.Get(userId));
            else
                return Unauthorized();
        }
    }
}
