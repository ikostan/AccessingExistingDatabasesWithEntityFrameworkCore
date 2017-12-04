using System;
using System.Collections.Generic;

namespace Project.EFClasses
{
    public partial class SalesGroup
    {
        public SalesGroup()
        {
            SalesPeople = new HashSet<Salesperson>();
        }

        public int Id { get; set; }
        public string State { get; set; }
        public int Type { get; set; }

        //Collection of SalesPeople, added since we have to create navigation between this two entities
        public virtual ICollection<Salesperson> SalesPeople{get; set;}
    }
}
