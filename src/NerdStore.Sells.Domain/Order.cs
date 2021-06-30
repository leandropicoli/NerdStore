using NerdStore.Core.DomainObjects;

namespace NerdStore.Sells.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public string test { get; set; }  
        
    }
}