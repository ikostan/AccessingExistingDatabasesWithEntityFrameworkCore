using System;
using System.Collections.Generic;

namespace Project.EFClasses
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Variety { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public bool Perishable { get; set; }
        public int? ExpirationDays { get; set; }
        public bool? Refrigerated { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
