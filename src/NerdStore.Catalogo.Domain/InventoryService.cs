using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;

        public InventoryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DecreaseInventory(Guid productId, int amount)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HasInventory(amount)) return false;

            product.DecreaseInventory(amount);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> IncreaseInventory(Guid productId, int amount)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            product.IncreaseInventory(amount);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
