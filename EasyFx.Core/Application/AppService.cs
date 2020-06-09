using AutoMapper;

namespace EasyFx.Core.Application
{
    public class AppService:IAppService
    {
        public AppService()
        {

        }
        public IMapper Mapper { get; set; }
    }
}