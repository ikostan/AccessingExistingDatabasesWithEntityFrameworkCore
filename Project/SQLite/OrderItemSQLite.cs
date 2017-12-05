using System;
using System.Collections.Generic;

namespace Project.SQLite
{
    public partial class OrderItemSQLite
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int? Quantity { get; set; }

        public OrderSQLite OrderSQLite { get; set; }
        public ProductSQLite ProductSQLite { get; set; }
    }
}
