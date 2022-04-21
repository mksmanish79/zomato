using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using zomato.Data;
using zomato.Factories.Interfaces;
using zomato.Model.JWT;

namespace zomato.Factories
{
    public class JwtAuth : IJwtAuth
    {
        private readonly string _key = "A7CFFB27-5F11-4180-8547-142A935014E8";
        ZomatoContext _dbContext;
        private readonly ILogger<JwtAuth> _logger;

        public JwtAuth(ZomatoContext _dBContext, ILogger<JwtAuth> logger)
        {
            _dbContext = _dBContext;
            _logger = logger;
        }

        public async Task<JWTTokenModel> GetToken(HttpRequest request, int userid)
        {
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = GetipAddress(request),
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("id", userid.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //SaveOrUpdateUserRefreshToken
            // 5. Return Token from method
            JWTTokenModel jWTTokenModel = new JWTTokenModel()
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = new RefreshTokenGenerator().GenerateRefreshToken(32)
            };
            UserRefreshToken userRefreshToken = new UserRefreshToken()
            {
                UserId = userid,
                RefreshToken = jWTTokenModel.RefreshToken
            };
            await SaveOrUpdateUserRefreshToken(userRefreshToken);
            return jWTTokenModel;
        }

        public async ValueTask<bool> CheckIfRefreshTokenIsValid(int userid, string refreshToken)
        {
            User t_User = await _dbContext.Users.FindAsync(userid);
            if (t_User != null)
            {
                if (t_User.RefreshToken.Equals(refreshToken) && (t_User.RefreshTokenExpiryTime.Value - DateTime.UtcNow).Hours <= 6)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task SaveOrUpdateUserRefreshToken(UserRefreshToken userRefreshToken)
        {
            try
            {
                User t_User = await _dbContext.Users.FindAsync(userRefreshToken.UserId);
                t_User.RefreshToken = userRefreshToken.RefreshToken;
                t_User.RefreshTokenExpiryTime = DateTime.UtcNow;
                _dbContext.SaveChanges();

            }
            catch (Exception Ex)
            {
            }
        }

        public bool ValidateToken(HttpRequest request, int userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            try
            {
                string token = request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = GetipAddress(request),
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var _userId = Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "id").Value);
                if (userid == _userId)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetID(HttpRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            try
            {
                string token = request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = GetipAddress(request),
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var _userId = Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "id").Value);
                return _userId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private string GetipAddress(HttpRequest Request)
        {
            try
            {
                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    return Request.Headers["X-Forwarded-For"];
                else
                    return Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            catch { return ""; }
        }
    }
}
