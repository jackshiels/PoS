using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.BusDomain;

namespace PoS.Controllers
{
    public class CreateACustomer
    {
        #region Members
        private CustomerDB custDB;
        private Customer aCust;
        #endregion

        #region Constructors
        public CreateACustomer() {}

        public CreateACustomer(string name, string address, string[] paymentDetails)
        {
            CustDB = new CustomerDB();
            SubmitCustomer(name, address, paymentDetails);
        }
        #endregion

        #region Methods
        // Create the customer object to submit. Send an empty string[] if EFT
        public bool SubmitCustomer(string name, string address, string[] paymentDetails)
        {
            // Passed bool
            bool success = false;
            // Create a new customer object
            Customer aCust = new Customer();

            // Add the values
            aCust.Name = name;
            aCust.Address = address;
            // Not blacklisted by default
            aCust.BlackListed = 0;
            // Not in debt by default
            aCust.Debt = 0;

            // Check the payment method
            if (paymentDetails == null)
            {
                // Creates empty values for an EFT to avoid data issues on the DB
                string[] empty = new string[3]{"0", "0", "0"};
                aCust.CardHolderDetails = empty;
                aCust.Payment = Customer.PaymentMethod.EFT;
            }
            else
            {
                aCust.CardHolderDetails = paymentDetails;
                aCust.Payment = Customer.PaymentMethod.CreditCard;
            }

            // Insert the customer
            success = CustDB.InsertCustomer(aCust);
            return success;
        }
        #endregion

        #region Properties
        public Customer Cust
        {
            get { return aCust; }
            set { aCust = value; }
        }

        public CustomerDB CustDB
        {
            get { return custDB; }
            set { custDB = value; }
        }
        #endregion
    }
}
