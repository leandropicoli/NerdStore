using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain
{

    public interface IInventoryService : IDisposable
    {
        Task<bool> DecreaseInventory(Guid productId, int amount);
        Task<bool> IncreaseInventory(Guid productId, int amount);
    }
}
