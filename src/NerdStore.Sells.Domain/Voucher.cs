using System;

namespace NerdStore.Sells.Domain
{
    public class Voucher
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Amount { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }
    }
}