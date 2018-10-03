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
        private int blackListed;
        #endregion

        #region Constructors
        public Customer() : base() { }

        public Customer(int CustomerID, string name, string address, DateTime dob, string payment) : base()
        {
            // Creates a Customer and fills the Person superclass with values
            this.customerId = CustomerID;
            this.paymentDetails = payment;
            this.blackListed = 0;
            base.Name = name;
            base.Address = address;
            base.DOB = dob;
        }
        #endregion

        #region Methods
        public void ChangeBlackListed(bool val)
        {
            if (val == true)
            {
                blackListed = 1;
            }
            else
            {
                blackListed = 0;
            }
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
        public int BlackListed
        {
            get { return blackListed; }
            set { blackListed = value; }
        }
        #endregion
    }
}
