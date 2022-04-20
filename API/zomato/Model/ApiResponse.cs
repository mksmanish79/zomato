using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zomato.Model
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Result { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
