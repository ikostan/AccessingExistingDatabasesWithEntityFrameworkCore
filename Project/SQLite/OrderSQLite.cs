using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project.SQLite
{
    public partial class OrderSQLite
    {
        public OrderSQLite()
        {
            OrderItemSQLite = new HashSet<OrderItemSQLite>();
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

        //Not supported by SQLite
        //[DisplayName("Last Update")]
        //public byte[] LastUpdate { get; set; }

        [DisplayName("Order Has Items")]
        public ICollection<OrderItemSQLite> OrderItemSQLite { get; set; }

        [DisplayName("Customer")] 
        public CustomerSQLite CustomerSQLite { get; set; } //FK

        [DisplayName("Salesperson")]
        public SalespersonSQLite SalespersonSQLite { get; set; } //FK
    }
}
