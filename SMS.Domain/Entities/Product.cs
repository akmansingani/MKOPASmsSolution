using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product : IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Portions { get; set; }
        public ICollection<Order> orderList { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
