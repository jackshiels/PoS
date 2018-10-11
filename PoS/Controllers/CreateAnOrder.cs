using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.BusDomain;
using System.Windows.Forms;

namespace PoS.Controllers
{
    public class CreateAnOrder
    {
        #region Members
        private ProductDB prodDb;
        private OrderDB ordDb;
        private Order anOrd;
        private CustomerDB custDb;
        #endregion

        #region Constructors
        public CreateAnOrder()
        {
            OrdDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order();
            custDb = new CustomerDB();
        }

        public CreateAnOrder(Customer aCust)
        {
            OrdDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order(aCust);
            custDb = new CustomerDB();
        }
        #endregion

        #region Methods
        // Takes a collection of orderitems into the order. Called after confirmation.
        public bool InsertIntoOrderDB(Order ord)
        {
            foreach (OrderItem ordItem in ord.ItemList)
            {
                // Add the name of the product to the list
                prodDb.ReserveProduct(ordItem.ItemProduct.Name, ordItem.Quantity);
                // Add it to the order
            }

            // Do the insert
            bool success = OrdDb.InsertOrder(ord);

            return success;
        }

        public Collection<Customer> ValidCustomers()
        {
            Collection<Customer> custList = new Collection<Customer>();

            // Iterate
            foreach(Customer cust in custDb.CustList)
            {
                if (cust.BlackListed == 0)
                {
                    custList.Add(cust);
                }
            }

            return custList;
        }

        public Collection<string> ProductCount()
        {
            Collection<string> prodList = new Collection<string>();

            // Iterate
            foreach(Product prod in prodDb.ProdList)
            {
                 string obj = prod.Name + " (Available: " + prod.Stock + ")";
                 prodList.Add(obj);
            }

            return prodList;
        }
        #endregion

        #region Properties
        public Order ord
        {
            get { return ord; }
            set { ord = value; }
        }
        public ProductDB ProdDB
        {
            get { return prodDb; }
            set { prodDb = value; }
        }
        public CustomerDB CustDB
        {
            get { return custDb; }
            set { custDb = value; }
        }
        public Order AnOrd
        {
            get { return anOrd; }
            set { anOrd = value; }
        }

        public OrderDB OrdDb
        {
            get { return ordDb; }
            set { ordDb = value; }
        }
        #endregion
    }
}
