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
        private DateTime deliveryDate;
        private float total;
        private Customer owner;
        // Orders may be directed to a different address than that of the Customer's on-file location.
        private string address;
        #endregion

        #region Constructors
        public Order()
        {
            Collection<OrderItem> itemList = new Collection<OrderItem>();
        }

        public Order(Customer Cust)
        {
            // This constructor takes the address field from the customer
            Collection<OrderItem> itemList = new Collection<OrderItem>();
            this.owner = Cust;
            this.address = Cust.Address;
        }

        public Order(Customer Cust, string Address)
        {
            // This constructor can take a custom address
            Collection<OrderItem> itemList = new Collection<OrderItem>();
            this.owner = Cust;
            this.address = Address;
        }
        #endregion

        #region Methods
        public bool AddToOrder(Product prod, int quant)
        {
            // Adds a new product to the list if the list has been initialised
            bool added = false;

            if (itemList != null)
            {
                OrderItem item = new OrderItem(prod, quant);
                itemList.Add(item);
                added = true;
            }

            return added;
        }

        public bool RemoveFromOrder(int itemId)
        {
            // Checks if the item exists. Removes if it does.
            int index = FindItem(itemId);
            bool removed = false;

            if (index != -1)
            {
                itemList.RemoveAt(index);
                removed = true;
            }

            // Returns the effected var
            return removed;
        }

        public bool UpdateOrderItemQuantity(int itemId, int quant)
        {
            // Finds the order item and updates the quantity
            bool updated = false;
            int index = FindItem(itemId);

            if (index != -1)
            {
                itemList[index].Quantity = quant;
                updated = true;
            }

            // returns the effected var
            return updated;
        }

        public int FindItem(int itemId)
        {
            // Creates an index value. -1 indicates the item is not found
            int index = -1;

            for (int i = 0; i < itemList.Count(); i++)
            {
                if (itemList[i].OrderItemID == itemId)
                {
                    index = i;
                }
            }

            // Returns the effected var
            return index;
        }

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
        public int OrderID
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public Collection<OrderItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
        public float Total
        {
            // Calculates and gets the total value
            get { return CalculateTotal(); }
            set { total = value; }
        }
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }
        #endregion
    }
}
