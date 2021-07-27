using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class OrderCompletionEvent : DomainEvent
    {
        public OrderCompletionEvent(Product item,decimal amount)
        {
            Item = item;
            Amount = amount;
        }

        public Product Item { get; }
        public decimal Amount { get; }
    }
}
