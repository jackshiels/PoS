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
        private Collection<string> toBeReserved;
        #endregion

        #region Constructors
        public CreateAnOrder() { }

        public CreateAnOrder(Customer aCust)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order(aCust);
            toBeReserved = new Collection<string>();
        }
        #endregion

        #region Methods
        // Takes a collection of orderitems into the order. Called after confirmation.
        public bool InsertIntoOrderDB(Collection<OrderItem> items)
        {
            foreach (OrderItem ordItem in items)
            {
                // Add the name of the product to the list
                toBeReserved.Add(ordItem.ItemProduct.Name);
                // Add it to the order
                anOrd.ItemList.Add(ordItem);
            }

            // Passed bool
            bool success = false;

            // Multiplicity of checks
            bool first = false;
            bool second = false;

            // Do the insert
            first = ordDb.InsertOrder(anOrd);

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
        #endregion

        #region Properties
        public Order ord
        {
            get { return ord; }
            set { ord = value; }
        }
        #endregion
    }
}
