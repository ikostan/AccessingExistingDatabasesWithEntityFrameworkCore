using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.SQLite
{
    public partial class OrderItemSQLite
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int? Quantity { get; set; }

        public OrderSQLite OrderSQLite { get; set; }
        public ProductSQLite ProductSQLite { get; set; }
    }
}
