using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zomato.Data;
using zomato.Factories.Interfaces;
using zomato.Model;

namespace zomato.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IMapper _mapper;
        private readonly ZomatoContext _zomatoContext;
        public UserFactory(IMapper mapper, ZomatoContext zomatoContext)
        {
            _mapper = mapper;
            _zomatoContext = zomatoContext;
        }

        public async Task<ApiResponse<int>> LoginUser(TRegister model)
        {
            ApiResponse<int> response = new ApiResponse<int> { };
            User user = await Task.FromResult(_zomatoContext.Users.Where(l => l.Email == model.Email && l.Password == model.Password).SingleOrDefault());
            if (user != null)
            {
                response.Status = true;
                response.Message = "Login Successfully";
                response.Result = user.ID;
            }
            return response;
        }

        public async Task<ApiResponse<int>> RegisterUser(TRegister model)
        {
            ApiResponse<int> response = new ApiResponse<int> { };
            User user = _mapper.Map<User>(model);
            await _zomatoContext.Users.AddAsync(user);
            if (await _zomatoContext.SaveChangesAsync() > 0)
            {
                response.Status = true;
                response.Message = "Registered Successfully";
                response.Result = user.ID;
            }
            return response;
        }

        public async Task<ApiResponse<TRegister>> Get(int UserID)
        {
            ApiResponse<TRegister> response = new ApiResponse<TRegister> { };
            User user = await Task.FromResult(_zomatoContext.Users.Where(l => l.ID == UserID).SingleOrDefault());
            if (user != null)
            {
                response.Status = true;
                response.Result = _mapper.Map<TRegister>(user);
            }
            return response;
        }
    }
}
