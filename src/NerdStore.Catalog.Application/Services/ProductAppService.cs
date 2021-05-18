using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NerdStore.Catalog.Application.ViewModel;
using NerdStore.Catalog.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, 
                                IMapper mapper, 
                                IInventoryService inventoryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _inventoryService = inventoryService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DecreaseInventory(Guid id, int amount)
        {
            if (!_inventoryService.DecreaseInventory(id, amount).Result)
            {
                throw new DomainException("Error decreasing inventory.");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> IncreaseInventory(Guid id, int amount)
        {
            if (!_inventoryService.IncreaseInventory(id, amount).Result)
            {
                throw new DomainException("Error increasing inventory.");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _inventoryService?.Dispose();
        }
    }
}