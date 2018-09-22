using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class Customer : Person
    {
        // Inherited class
        #region Members
        private string paymentDetails;
        #endregion

        #region Constructors
        public Customer() : base() { }

        public Customer(string name, string address, DateTime dob, string payment) : base()
        {
            // Creates a Customer and fills the Person superclass with values

            this.paymentDetails = payment;
            base.Name = name;
            base.Address = address;
            base.DOB = dob;
        }
        #endregion

        #region Property Methods
        public string Payment
        {
            get { return paymentDetails; }
            set { paymentDetails = value; }
        }
        #endregion
    }
}
