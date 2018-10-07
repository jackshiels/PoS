using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.BusDomain;

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
        public bool AddOrder()
        {
            // Passed bool
            bool success = false;

            // Do the insert
            success = ordDb.InsertOrder(anOrd);

            // Reserve the correct amount of products
            foreach(string name in toBeReserved)
            {
                prodDb.ReserveProduct(name);
            }

            return success;
        }

        public bool AddToOrder(Product prod)
        {
            bool success = false;

            // Add the name of the product to the list
            toBeReserved.Add(prod.Name);

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
