using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project.SQLite
{
    public partial class CustomerSQLite
    {
        public CustomerSQLite()
        {
            OrderSQLite = new HashSet<OrderSQLite>();
        }

        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("First Name")]
        public string StrFldFirstName { get; set; }

        [DisplayName("Last Name")]
        public string StrFldLastName { get; set; }

        //Backing field
        private string _email;

        [DisplayName("Email")]
        public string StrFldEmail {
            get { return _email; }
            set { _email = value.ToLower(); }
        }

        [DisplayName("Phone")]
        public string StrFldPhone { get; set; }

        [DisplayName("Address")]
        public string StrFldAddress { get; set; }

        [DisplayName("City")]
        public string StrFldCity { get; set; }

        [DisplayName("State")]
        public string StrFldState { get; set; }

        [DisplayName("Zip Code")]
        public string StrFldZipcode { get; set; }

        //Not supported by SQLite
        //[DisplayName("Full Name")]
        //public string CmpLastFirst { get; private set; } //Make set as private in order to avoid updates

        public ICollection<OrderSQLite> OrderSQLite { get; set; }
    }
}
