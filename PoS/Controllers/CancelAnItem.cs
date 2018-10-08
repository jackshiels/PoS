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
        private CustomerDB custDb;
        private Collection<string> toBeReserved;
        #endregion

        #region Constructors
        public CancelAnItem() { }

        public CancelAnItem(Customer aCust)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = new Order(aCust);
            custDb = new CustomerDB();
            toBeReserved = new Collection<string>();
        }
        #endregion
    }
}
