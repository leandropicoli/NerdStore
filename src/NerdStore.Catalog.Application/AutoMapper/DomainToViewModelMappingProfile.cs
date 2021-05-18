using AutoMapper;
using NerdStore.Catalog.Application.ViewModel;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));
            
            CreateMap<Category, CategoryViewModel>();
        }
    }
}