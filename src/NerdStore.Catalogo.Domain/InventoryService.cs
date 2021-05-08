using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;
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
        private readonly IMediatrHandler _bus;

        public InventoryService(IProductRepository productRepository, IMediatrHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> DecreaseInventory(Guid productId, int amount)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HasInventory(amount)) return false;

            product.DecreaseInventory(amount);

            //TODO: Set minimum inventory amount as parameter
            if (product.InventoryAmount < 10)
            {
                await _bus.PublishEvent(new LowProductInventoryEvent(product.Id, product.InventoryAmount));
            }

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
