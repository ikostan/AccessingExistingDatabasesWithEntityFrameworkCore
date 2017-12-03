using System;
using System.Collections.Generic;

namespace Project.EFClasses
{
    public partial class Salesperson
    {
        public Salesperson()
        {
            Order = new HashSet<Order>();
        }

        public int SalespersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //Shadow properties:
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zipcode { get; set; }

        public string SalesGroupState { get; set; }
        public int SalesGroupType { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public ICollection<Order> Order { get; set; }
        public Salesperson SalespersonNavigation { get; set; }
        public Salesperson InverseSalespersonNavigation { get; set; }
    }
}
