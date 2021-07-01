using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sells.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitValue { get; private set; }

        //EF Rel.
        public Order Order { get; set; }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Amount * UnitValue;
        }

        internal void AddAmount(int amount)
        {
            Amount += amount;
        }

        internal void UpdateAmount(int amount)
        {
            Amount = amount;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}