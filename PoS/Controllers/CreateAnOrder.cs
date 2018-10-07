using System;
using System.Collections.Generic;
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
        #endregion

        #region Constructors
        public CreateAnOrder() { }

        public CreateAnOrder(Customer aCust)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order(aCust);
        }
        #endregion

        #region Methods
        public bool AddOrder()
        {
            // Passed bool
            bool success = false;

            // Do the insert
            success = ordDb.InsertOrder(anOrd);

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
