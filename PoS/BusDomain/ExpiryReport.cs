using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.Presentation;

namespace PoS.BusDomain
{
    public class ExpiryReport
    {
        #region Members
        private int reportId;
        ProductDB prodConnect;
        Collection<OrderItem> expiring;
        Collection<OrderItem> expired;
        private IDGen generator;
        #endregion

        #region Constructors

        public ExpiryReport()
        {
            /*
            Tumi-side software dev
            will have to split collections into 2 collections when expired and expiringList methods are complete
            Collection 1 = coke, Fanta, Lays etc. just the names
            Collection 2 = 500, 250 , 70, etc. just the values expired and expiring 
            Collections should obviously be the same length and 1[x] should talk about 2[x]
             */
            generator = new IDGen();
            reportId = generator.CreateID();
            expiring = new Collection<OrderItem>();
            expired = new Collection<OrderItem>();
            prodConnect = new ProductDB();
            expiring = prodConnect.ExpiringList(); //collection of Order Items
            expired = prodConnect.ExpiredList(); // Collection of OrderItems
        }
        #endregion

        #region reportGUI
        public void generate()
        {
            report expiryReport = new report();
            //expiryReport.populateChart();
            //Collection<string> items
            //Collection<int> numericalValues

            //expiryReport.populateTable(); 
            //Collection<string> prodName
            //Collection<DateTime> expiryDate
            //Collection<int> expiringQuantity
            //Collection<int> location
            //Collection<float> writeOff

            expiryReport.load();
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
