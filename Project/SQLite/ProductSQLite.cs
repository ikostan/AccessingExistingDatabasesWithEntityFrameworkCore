using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.SQLite
{
    public partial class ProductSQLite
    {
        public ProductSQLite()
        {
            //OrderItem = new HashSet<OrderItem>();
        }

        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Variety { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }

        //Transfered to PerishableProduct.cs
        //public int? ExpirationDays { get; set; }
        //public bool? Refrigerated { get; set; }
        //public bool Perishable { get; set; } //This field hasbeen removed since we do not need it anymore

        //Product does not have to reference orders. For that reason this field is removed
        //public ICollection<OrderItem> OrderItem { get; set; }
    }
}
