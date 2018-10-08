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
        private Collection<string> toBeReserved;
        #endregion

        #region Constructors
        public CreateAnOrder() { }

        public CreateAnOrder(Customer aCust)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order(aCust);
            custDb = new CustomerDB();
            toBeReserved = new Collection<string>();
        }
        #endregion

        #region Methods
        // Takes a collection of orderitems into the order. Called after confirmation.
        public bool InsertIntoOrderDB(Order ord)
        {
            foreach (OrderItem ordItem in ord.ItemList)
            {
                // Add the name of the product to the list
                toBeReserved.Add(ordItem.ItemProduct.Name);
                // Add it to the order
            }

            // Passed bool
            bool success = false;

            // Multiplicity of checks
            bool first = false;
            bool second = false;

            // Do the insert
            first = ordDb.InsertOrder(ord);

            // Reserve the correct amount of products
            foreach (string name in toBeReserved)
            {
                prodDb.ReserveProduct(name);
            }

            second = prodDb.CommitUpdate();

            // Checks if both have been done
            if (first & second)
            {
                success = true;
            }
            else if (first & !second)
            {
                MessageBox.Show("Only the database commit worked.");
            }
            else if (!first & second)
            {
                MessageBox.Show("Only the reserve update worked.");
            }
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
            Collection<string> seen = new Collection<string>();

            // Iterate
            foreach(Product prod in prodDb.ProdList)
            {
                if (seen.Contains(prod.Name))
                {
                    continue;
                }
                else
                {
                    string obj = prod.Name + " (Available: " + prodDb.FindNumProduct(prod.Name) + ")";
                    prodList.Add(obj);
                    seen.Add(prod.Name);
                }
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
        #endregion
    }
}
