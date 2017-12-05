using System;
using System.Collections.Generic;

namespace Project.SQLite
{
    public partial class SalesGroupSQLite
    {
        public SalesGroupSQLite()
        {
            SalesPeopleSQLite = new HashSet<SalespersonSQLite>();
        }

        public int Id { get; set; }
        public string State { get; set; }
        public int Type { get; set; }

        //Collection of SalesPeople, added since we have to create navigation between this two entities
        public virtual ICollection<SalespersonSQLite> SalesPeopleSQLite { get; set;}
    }
}
