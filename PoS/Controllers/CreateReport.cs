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
    public class CreateReport
    {
        #region Members
        private ProductDB prodDb;
        private Collection<OrderItem> expiredList;
        private ExpiryReport exp;
        #endregion

        #region
        public CreateReport()
        {
            prodDb = new ProductDB();
            exp = new ExpiryReport();
            expiredList = prodDb.ExpiryList();
        }
        #endregion

        #region Property Methods
        public ExpiryReport Exp
        {
            get { return exp; }
            set { exp = value; }
        }
        #endregion
    }
}
