using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;

namespace PoS.BusDomain
{
    public class ExpiryReport
    {
        #region Members
        private int reportId;
        ProductDB prodConnect;
        Collection<Product> expiring;
        Collection<Product> expired;
        private IDGen generator;
        #endregion

        #region Constructors
        public ExpiryReport()
        {
            // Tumi-side software dev
            generator = new IDGen();
            reportId = generator.CreateID();
            expiring = new Collection<Product>();
            expired = new Collection<Product>();
            prodConnect = new ProductDB();
            expiring = prodConnect.ExpiringList();
            expired = prodConnect.ExpiredList();
        }
        #endregion

        #region Property Methods
        public int ReportID
        {
            get { return reportId; }
            set { reportId = value; }
        }
        public Collection<Product> Expiring
        {
            get { return expiring; }
            set { expiring = value; }
        }
        public Collection<Product> Expired
        {
            get { return expired; }
            set { expired = value; }
        }
        #endregion
    }
}
