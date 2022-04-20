using AutoMapper;
using zomato.Data;
using zomato.Model;

namespace zomato.Factories
{
    public class AutomapperFactory : Profile
    {
        public AutomapperFactory()
        {
            CreateMap<TRegister, User>().ReverseMap();
        }
    }
}
