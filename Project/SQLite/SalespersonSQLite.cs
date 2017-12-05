using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.SQLite
{
    public partial class SalespersonSQLite
    {
        public SalespersonSQLite()
        {
            OrderSQLite = new HashSet<OrderSQLite>();
        }

        [Key]
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

        public ICollection<OrderSQLite> OrderSQLite { get; set; }

        //Commented out since those properties navigate to exact same entity
        //public Salesperson SalespersonNavigation { get; set; }
        //public Salesperson InverseSalespersonNavigation { get; set; }

        //Navigation property for SalesGroup
        public virtual SalesGroupSQLite SalesGroupSQLite { get; set; }
    }
}
