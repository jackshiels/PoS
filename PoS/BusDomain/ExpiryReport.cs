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
        private string reportId;
        private ProductDB prodConnect;
        private Collection<Product> expiredAndExpiring;
        private IDGen generator;
        private DataGridView dataGrid;
        private Chart chart;
        #endregion

        #region Constructors
        public ExpiryReport(Chart prodCharta)
        {
            generator = new IDGen();
            reportId = "REP"+generator.CreateID();
            expiredAndExpiring = new Collection<Product>();
            prodConnect = new ProductDB();
            chart = new Chart();
            dataGrid = new DataGridView();
            expiredAndExpiring = prodConnect.ExpiryList();
            populateChart(expiredAndExpiring, prodCharta);
        }
        #endregion

        #region Methods
        public void generateReport()
        {
            //populateChart(expiredAndExpiring);
            //expiryReport.load();
        }

        public void populateChart(Collection<Product> items, Chart prodChart)
        {
            prodChart.Series.Add("Expired/Expiring Objects");
            prodChart.Series["Expired/Expiring Objects"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            prodChart.Series["Expired/Expiring Objects"].Enabled = true;
            prodChart.Series["Expired/Expiring Objects"].SetDefault(true);

            // add items to columns
            for (int i = 0; i < items.Count; i++)
            {
                // expiredItems.Series["Expired/Expiring Objects"].Points.AddXY(items[i].ItemProduct, items[i].Quantity); // add Coke,500 to chart
                prodChart.Series["Expired/Expiring Objects"].Points.AddY(items[i].Stock);
                prodChart.Series["Expired/Expiring Objects"].Points[i].AxisLabel = items[i].Name;
            }

            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Chartreuse, Color.Fuchsia, Color.SlateBlue, Color.Cyan }; // order of colours in chart

            for (int i = 0; i < prodChart.Series["Expired/Expiring Objects"].Points.Count; i++)
            {
                prodChart.Series["Expired/Expiring Objects"].Points[i].Color = colors[i]; //shouldnt have more than 5 items but added padding . Changes colour of data at point i
            }
            prodChart.Visible = true;
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
        public string ReportID
        {
            get { return reportId; }
            set { reportId = value; }
        }
        public Collection<Product> ExpiredAndExpiring
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
