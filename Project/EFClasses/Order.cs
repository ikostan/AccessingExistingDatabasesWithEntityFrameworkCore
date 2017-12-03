using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project.EFClasses
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        [DisplayName("Order Id")]
        public int OrderId { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Total Due")]
        public decimal? TotalDue { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("Sales Person Id")]
        public int SalespersonId { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Last Update")]
        public byte[] LastUpdate { get; set; }

        [DisplayName("Order Has Items")]
        public ICollection<OrderItem> OrderItem { get; set; }

        [DisplayName("Customer")]
        public Customer Customer { get; set; }

        [DisplayName("Salesperson")]
        public Salesperson Salesperson { get; set; }
    }
}
