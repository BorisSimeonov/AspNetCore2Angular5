using AutoMapper;
using TestMakerFreeWebApp.Data;

namespace TestMakerFreeWebApp.Services.Implementations
{
    public abstract class BaseService
    {
        protected ApplicationDbContext DbContext { get; }

        protected IMapper Mapper { get; }

        public BaseService(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}
