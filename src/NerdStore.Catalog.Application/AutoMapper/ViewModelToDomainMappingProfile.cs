using AutoMapper;
using NerdStore.Catalog.Application.ViewModel;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p =>
                    new Product(p.Name, p.Description, p.Active, p.Value, p.CategoryId,
                        p.RegisterDate, p.Image, new Dimensions(p.Height, p.Width, p.Depth)));
            
            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
 
        }
    }
}