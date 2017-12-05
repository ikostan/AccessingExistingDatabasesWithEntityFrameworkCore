using System;
using System.Collections.Generic;

namespace Project.SQLite
{
    public partial class PerishableProductSQLite : ProductSQLite
    {
        public int? ExpirationDays { get; set; }
        public bool? Refrigerated { get; set; }
    }
}
