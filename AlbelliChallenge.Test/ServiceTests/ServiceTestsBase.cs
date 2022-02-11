using AlbelliChallenge.Business.Profiles;
using AutoMapper;

namespace AlbelliChallenge.Test
{
    public class ServiceTestsBase
    {
        protected readonly IMapper _mapper;

        public ServiceTestsBase()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            }).CreateMapper();
        }
    }
}
