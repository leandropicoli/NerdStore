using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Catalog.Application.ViewModel;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseInventory(Guid id, int amount);
        Task<ProductViewModel> IncreaseInventory(Guid id, int amount);
    }
}