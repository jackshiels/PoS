using PoS.BusDomain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoS.Presentation
{
    public partial class report : Form
    {
        public report()
        {
            //InitializeComponent();
        }

        public void load()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {

        }

        private void date_Click(object sender, EventArgs e)
        {

        }

        private void titleBox_Click(object sender, EventArgs e)
        {

        }

        private void expiredItems_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //call this after making report
        public void populateChart(Collection<OrderItem> items)
        {
            // both collections should alays be of the same length
            string date = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); //get todays date in the form dd/mm/yy
            dateBox.Text = date; // set text box to todays date

            expiredItems.Series.Add("Expired/Expiring Objects");
            expiredItems.Series["Expired/Expiring Objects"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            expiredItems.Series["Expired/Expiring Objects"].Enabled = true;
            expiredItems.Series["Expired/Expiring Objects"].SetDefault(true);
            // add items to columns
            for (int i = 1; i < items.Count+1; i++)
            {
                expiredItems.Series["Expired/Expiring Objects"].Points.AddXY(items[i].ItemProduct, items[i].Quantity); // add Coke,500 to chart
            }

            Color[] colors = new Color[] {Color.Red, Color.Blue, Color.Yellow, Color.Chartreuse, Color.Fuchsia, Color.SlateBlue, Color.Cyan }; // order of colours in chart

            for (int i = 0; i < expiredItems.Series["Expired/Expiring Objects"].Points.Count; i++)
            {
                expiredItems.Series["Expired/Expiring Objects"].Points[i].Color = colors[i]; //shouldnt have more than 5 items but added padding . Changes colour of data at point i
            }
            expiredItems.Visible = true;
           
        }
        
        public void populateTable(Collection<OrderItem> items)
        {
            for (int i = 0; i < items.Count(); i++)
            {
                reportTable.Rows.Add(items[i].ItemProduct.Name,items[i].ItemProduct.Expiry.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),items[i].Quantity,items[i].ItemProduct.Location,items[i].SubTotal);
            }
            reportTable.Visible = true;
        }
    }
}
