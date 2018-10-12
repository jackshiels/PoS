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
    public class GeneratePickingList
    {
        #region Member
        private OrderDB ordDb;
        #endregion

        #region Constructor
        public GeneratePickingList()
        {
            ordDb = new OrderDB();
        }
        #endregion

        #region Methods
        public Collection<string> GetPickingList(Order anOrd)
        {
            Collection<string> pickingList = new Collection<string>();

            // Iterate and generate the strings
            foreach(OrderItem item in anOrd.ItemList)
            {
                pickingList.Add("Product ID: "+item.OrderItemID + " Product Name: " + item.ItemProduct.Name +" Quantity: " + item.Quantity + " Product Location: " + item.ItemProduct.Location);
            }

            return pickingList;
        }
        #endregion
    }
}
