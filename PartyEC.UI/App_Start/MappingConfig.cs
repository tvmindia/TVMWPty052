


using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
namespace PartyEC.UI.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                //domain <===== viewmodel
                //viewmodel =====> domain
                //ReverseMap() makes it possible to map both ways.
               
                config.CreateMap<MenuViewModel, Menu>().ReverseMap();
                config.CreateMap<CommonViewModel, LogDetails>().ReverseMap();
                config.CreateMap<OperationsStatusViewModel, OperationsStatus>().ReverseMap();
                config.CreateMap<ProductViewModel, Product>().ReverseMap();
                config.CreateMap<AttributesViewModel, Attributes>().ReverseMap();
            });
        }


    }
}