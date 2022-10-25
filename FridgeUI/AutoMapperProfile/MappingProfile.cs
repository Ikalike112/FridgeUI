using AutoMapper;
using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeProductsModels;
using FridgeUI.Models.ProductModels;

namespace FridgeUI.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FrdigeModelDto, FridgeModelForManipulateDto>();
            CreateMap<ProductModel, ProductForManipulateDto>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<ProductModel, ProductToCreateWithFridgeViewModel>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.DefaultQuantity));
            CreateMap<ProductToCreateWithFridgeViewModel, FridgeProductToCreateFromFridgeDto>()
                .ForMember(dest=> dest.ProductId,opt => opt.MapFrom(src => src.Id));
        }
    }
}
