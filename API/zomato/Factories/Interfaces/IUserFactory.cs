using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zomato.Model;

namespace zomato.Factories.Interfaces
{
    public interface IUserFactory
    {
        Task<ApiResponse<int>> RegisterUser(TRegister model);
    }
}
