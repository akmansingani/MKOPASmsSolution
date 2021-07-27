using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ProductPurchaseValidationEvent : DomainEvent
    {
        public ProductPurchaseValidationEvent(Product item,decimal amount)
        {
            Item = item;
            Amount = amount;
        }

        public Product Item { get; }
        public decimal Amount { get; }
    }
}
