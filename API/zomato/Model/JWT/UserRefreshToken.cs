using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zomato.Model.JWT
{
    public class UserRefreshToken
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
