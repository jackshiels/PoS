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
    public class CancelAnItem
    {
        #region Members
        private ProductDB prodDb;
        private OrderDB ordDb;
        private Order anOrd;
        private Collection<string> toBeReserved;
        #endregion

        #region Constructors
        public CancelAnItem() { }

        public CancelAnItem(Order updateOrd)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = updateOrd;
            toBeReserved = new Collection<string>();
        }
        #endregion

        #region Methods
        public bool UpdateOrder(Order ord, Customer cust, Collection<OrderItem> items)
        {
            bool success = false;

            // Delete the order
            ordDb.Delete(ord);

            // Retain the orderid
            string orderId = ord.OrderID;

            // Create a new order with the same orderid
            CreateAnOrder inserted = new CreateAnOrder(cust);
            inserted.AnOrd.OrderID = orderId;

            // Insert
            success = inserted.InsertIntoOrderDB(items);

            return success;
        }
        #endregion

        #region Property Methods
        public OrderDB OrdDB
        {
            get { return ordDb; }
            set { ordDb = value; }
        }
        #endregion
    }
}
