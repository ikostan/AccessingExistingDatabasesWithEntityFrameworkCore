using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project.EFClasses
{
    public partial class Salesperson
    {
        public Salesperson()
        {
            Order = new HashSet<Order>();
        }

        [DisplayName("Sales Person Id")]
        public int SalespersonId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        //Shadow properties:
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zipcode { get; set; }

        public string SalesGroupState { get; set; }
        public int SalesGroupType { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public ICollection<Order> Order { get; set; }

        //Commented out since those properties navigate to exact same entity
        //public Salesperson SalespersonNavigation { get; set; }
        //public Salesperson InverseSalespersonNavigation { get; set; }

        //Navigation property for SalesGroup
        public virtual SalesGroup SalesGroup { get; set; }
    }
}
