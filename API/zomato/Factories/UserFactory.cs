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
    }
}
