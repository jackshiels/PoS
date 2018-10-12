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
        private string customerId;
        private int blackListed;
        private float debt;
        private IDGen generator;
        private string[] cardholderDetails;
        private PaymentMethod payment;

        public enum PaymentMethod
        {
            EFT = 0,
            CreditCard = 1
        }
        #endregion

        #region Constructors
        public Customer() : base()
        {
            generator = new IDGen();
            customerId = "CUS" + generator.CreateID();
        }

        public Customer(string name, string address) : base(name, address)
        {
            // Creates a Customer and fills the Person superclass with values
            generator = new IDGen();
            customerId = "CUS" + generator.CreateID();
            this.blackListed = 0;
            payment = PaymentMethod.CreditCard;
        }

        public Customer(string name, string address, string[] paymentDetails) : base(name,address)
        {
            // Creates a Customer and fills the Person superclass with values
            generator = new IDGen();
            customerId = "CUS" + generator.CreateID();
            this.blackListed = 0;
            cardholderDetails = paymentDetails;
            payment = PaymentMethod.CreditCard;
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
        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string[] CardHolderDetails
        {
            get { return cardholderDetails; }
            set { cardholderDetails = value; }
        }
        public float Debt
        {
            get { return debt; }
            set { debt = value; }
        }
        public PaymentMethod Payment
        {
            get { return payment; }
            set { payment = value; }
        }
        public int BlackListed
        {
            get { return blackListed; }
            set { blackListed = value; }
        }
        #endregion
    }
}
