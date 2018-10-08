using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.Presentation;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Globalization;


namespace PoS.BusDomain
{
    public class ExpiryReport
    {
        #region Members
        private int reportId;
        ProductDB prodConnect;
        Collection<OrderItem> expiredAndExpiring;
        private IDGen generator;
        private DataGridView dataGrid;
        private Chart chart;
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

        #region Methods
        public void generateReport()
        {
            report expiryReport = new report();
            expiryReport.populateChart(expiredAndExpiring);
            expiryReport.populateTable(expiredAndExpiring); 
            expiryReport.load();
        }

        public void populateChart(Collection<OrderItem> items)
        {
            // both collections should alays be of the same length
            string date = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); //get todays date in the form dd/mm/yy

            chart.Series.Add("Expired/Expiring Objects");
            chart.Series["Expired/Expiring Objects"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart.Series["Expired/Expiring Objects"].Enabled = true;
            chart.Series["Expired/Expiring Objects"].SetDefault(true);
            // add items to columns
            for (int i = 1; i < items.Count + 1; i++)
            {
                //expiredItems.Series["Expired/Expiring Objects"].Points.AddXY(items[i].ItemProduct, items[i].Quantity); // add Coke,500 to chart
                chart.Series["Expired/Expiring Objects"].Points.AddY(items[i].Quantity);
                chart.Series["Expired/Expiring Objects"].Points[i].AxisLabel = items[i].ItemProduct.Name;
            }

            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Chartreuse, Color.Fuchsia, Color.SlateBlue, Color.Cyan }; // order of colours in chart

            for (int i = 0; i < chart.Series["Expired/Expiring Objects"].Points.Count; i++)
            {
                chart.Series["Expired/Expiring Objects"].Points[i].Color = colors[i]; //shouldnt have more than 5 items but added padding . Changes colour of data at point i
            }
            chart.Visible = true;
        }

        public void populateTable(Collection<OrderItem> items)
        {
            for (int i = 0; i < items.Count(); i++)
            {
                dataGrid.Rows.Add(items[i].ItemProduct.Name, items[i].ItemProduct.Expiry.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), items[i].Quantity, items[i].ItemProduct.Location, items[i].SubTotal);
            }
            dataGrid.Visible = true;
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
        public DataGridView DataGrid
        {
            get { return dataGrid; }
            set { dataGrid = value; }
        }
        public Chart Chart
        {
            get { return chart; }
            set { chart = value; }
        }
        #endregion
    }
}
