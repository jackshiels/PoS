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
        Collection<OrderItem> expiredAndExpiring;
        private IDGen generator;
        #endregion

        #region Constructors

        public ExpiryReport()
        {
            generator = new IDGen();
            reportId = generator.CreateID();
            expiredAndExpiring = new Collection<OrderItem>();
            prodConnect = new ProductDB();
            expiredAndExpiring = prodConnect.ExpiryList();
        }
        #endregion

        #region reportGUI
        public void generate()
        {
            report expiryReport = new report();
            expiryReport.populateChart(expiredAndExpiring);
            expiryReport.populateTable(expiredAndExpiring); 
            expiryReport.load();
        }
        #endregion

        #region Property Methods
        public int ReportID
        {
            get { return reportId; }
            set { reportId = value; }
        }
        public Collection<OrderItem> ExpiredAndExpiring
        {
            get => expiredAndExpiring;
            set => expiredAndExpiring = value;
        }

        #endregion
    }
}
