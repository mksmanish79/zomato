using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zomato.Model.JWT;

namespace zomato.Factories.Interfaces
{
    public interface IJwtAuth
    {
        Task<JWTTokenModel> GetToken(HttpRequest request, int userid);
        bool ValidateToken(HttpRequest request, int userid);
        int GetID(HttpRequest request);
        Task SaveOrUpdateUserRefreshToken(UserRefreshToken userRefreshToken);
        ValueTask<bool> CheckIfRefreshTokenIsValid(int userId, string refreshToken);
    }
}
