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
        private int customerId;
        private string paymentDetails;
        #endregion

        #region Constructors
        public Customer() : base() { }

        public Customer(int CustomerID, string name, string address, DateTime dob, string payment) : base()
        {
            // Creates a Customer and fills the Person superclass with values
            this.customerId = CustomerID;
            this.paymentDetails = payment;
            base.Name = name;
            base.Address = address;
            base.DOB = dob;
        }
        #endregion

        #region Property Methods
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string Payment
        {
            get { return paymentDetails; }
            set { paymentDetails = value; }
        }
        #endregion
    }
}
