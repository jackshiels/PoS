using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class Order
    {
        #region Members
        private int orderId;
        private Collection<OrderItem> itemList;
        private float total;
        private Customer owner;
        private DateTime deliveryDate;
        // Orders may be directed to a different address than that of the Customer's on-file location.
        private string address;
        #endregion

        #region Constructors
        public Order()
        {
            Collection<OrderItem> itemList = new Collection<OrderItem>();
        }

        public Order(Customer Cust, string Address)
        {
            Collection<OrderItem> itemList = new Collection<OrderItem>();
            this.owner = Cust;
            this.address = Address;
        }
        #endregion

        #region Methods
        public float CalculateTotal()
        {
            // This method should be called every time an OrderItem is added
            // Instantiate a float value to work with
            float value = 0;

            // Loop through the OrderItem subtotal list
            if (itemList.Count != 0)
            {
                foreach (OrderItem item in itemList)
                {
                    value = value + item.SubTotal;
                }
            }
            
            // Return the value
            return value;
        }
        #endregion

        #region Property Methods
        // Get-set properties
        public Customer Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        // Get properties
        public int OrderID
        {
            get { return orderId; }
        }
        public Collection<OrderItem> ItemList
        {
            get { return itemList; }
        }
        public float Total
        {
            // Calculates and gets the total value
            get { return CalculateTotal(); }
        }
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
        }
        #endregion
    }
}
