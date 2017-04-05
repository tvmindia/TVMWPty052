


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
                config.CreateMap<LogDetailsViewModel, LogDetails>().ReverseMap();
                config.CreateMap<OperationsStatusViewModel, OperationsStatus>().ReverseMap();
                config.CreateMap<ProductViewModel, Product>().ReverseMap();
                config.CreateMap<ProductDetailViewModel, ProductDetail>().ReverseMap();
                config.CreateMap<AttributeValuesViewModel, AttributeValues>().ReverseMap();
                config.CreateMap<ProductImagesViewModel, ProductImages>().ReverseMap();
                config.CreateMap<AttributesViewModel, Attributes>().ReverseMap();
                config.CreateMap<AttributeSetViewModel, AttributeSet>().ReverseMap();
                config.CreateMap<AttributeSetLinkViewModel, AttributeSetLink>().ReverseMap();
                config.CreateMap<CategoriesViewModel, Categories>().ReverseMap();
                config.CreateMap<CategoriesListAppViewModel, Categories>().ReverseMap();
                config.CreateMap<TopProductsOfCategoryAppViewModel, Product>().ReverseMap();
                config.CreateMap<NavigationalCatsOfCategoryAppViewModel, Categories>().ReverseMap();
                config.CreateMap<ManufacturerViewModel,Manufacturer >().ReverseMap();
                config.CreateMap<SupplierViewModel,Supplier>().ReverseMap();
                config.CreateMap<CountryViewModel, Country>().ReverseMap();
                config.CreateMap<EventViewModel, Event>().ReverseMap();
                config.CreateMap<EventTypeAppViewModel, Event>().ReverseMap();
                config.CreateMap<ProductCategoryLinkViewModel, ProductCategoryLink>().ReverseMap();
                config.CreateMap<EventRequestsViewModel, EventRequests>().ReverseMap();
                config.CreateMap<CustomerViewModel, Customer>().ReverseMap();
                config.CreateMap<CustomerAddressViewModel, CustomerAddress>().ReverseMap();
                config.CreateMap<OrderViewModel, Order>().ReverseMap();
                config.CreateMap<OrderStatusViewModel, OrderStatusMaster>().ReverseMap();
                config.CreateMap<Cart_WishlistViewModel, Cart_Wishlist>().ReverseMap();
                config.CreateMap<ProductReviewViewModel, ProductReview>().ReverseMap();
                config.CreateMap<OtherImagesViewModel, OtherImages>().ReverseMap();
                config.CreateMap<ShippingLocationViewModel, ShippingLocations>().ReverseMap();
                config.CreateMap<SupplierLocationsViewModel, SupplierLocations>().ReverseMap();
            });
        }


    }
}