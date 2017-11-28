using System;
using System.Collections.Generic;

namespace Project.EFClasses
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? TotalDue { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SalespersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] LastUpdate { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
        public Customer Customer { get; set; }
        public Salesperson Salesperson { get; set; }
    }
}
