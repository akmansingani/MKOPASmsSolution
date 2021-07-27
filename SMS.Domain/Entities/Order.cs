using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : IHasDomainEvent
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
