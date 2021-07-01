using System;
using System.Collections.Generic;
using System.Linq;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sells.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; set; }
        public bool UsedVoucher { get; set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        //EF Rel.
        public virtual Voucher Voucher { get; private set; }

        public Order(Guid customerId, bool usedVoucher, decimal discount, decimal totalValue)
        {
            CustomerId = customerId;
            UsedVoucher = usedVoucher;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            UsedVoucher = true;
            CalculateTotalValue();
        }

        public void CalculateTotalValue()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
            CalculateTotalValueAndDiscount();
        }

        public void CalculateTotalValueAndDiscount()
        {
            if (!UsedVoucher) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.DiscountType == DiscountType.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool HasOrderItem(OrderItem item)
        {
            return _orderItems.Any(x => x.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            
            item.AssociateOrder(Id);

            if (HasOrderItem(item))
            {
                var existingItem = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                existingItem.AddAmount(item.Amount);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item.CalculateValue();
            _orderItems.Add(item);
            
            CalculateTotalValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var existingItem = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("Unable to find Item on Order.");
            _orderItems.Remove(existingItem);
            
            CalculateTotalValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existingItem = OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            
            if (existingItem == null) throw new DomainException("Unable to find Item on Order.");
            
            _orderItems.Remove(existingItem);
            _orderItems.Add(item);
            
            CalculateTotalValue();
        }

        public void UpdateAmounts(OrderItem item, int amount)
        {
            item.UpdateAmount(amount);
            UpdateItem(item);
        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Payed;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }
        
        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                return new Order
                {
                    CustomerId = customerId,
                    OrderStatus = OrderStatus.Draft
                };
            }

        }
    }
}