using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PoS.DB
{
    public class DB
    {
        #region Members
        // Initialises the connection string
        private string connStr = Properties.Settings.Default.DBMainConnectionString;
        protected SqlConnection cnMain;
        protected DataSet dsMain;
        protected SqlDataAdapter daMain;
        #endregion

        #region Constructors
        // Connection upon initialise, creates a data set.
        public DB(string sql, string table)
        {
            // Initialise the components of the DB class
            cnMain = new SqlConnection(connStr);
            daMain = new SqlDataAdapter(sql, cnMain);
            dsMain = new DataSet();

            // Fills the data set by default
            try
            {
                FillDataSet(sql, table);
            }
            catch
            {
                MessageBox.Show("Connection Error Exception");
            }
        }
        #endregion

        #region Methods
        protected void FillDataSet(string sql, string table)
        {
            // Open the connection and fill the data set
            cnMain.Open();
            daMain.Fill(dsMain, table);

            // Close the connection
            cnMain.Close();
        }

        protected bool UpdateDataSource(string sql, string table)
        {
            // Success bool
            bool success = false;

            try
            {
                // Open the connection and update the data set
                cnMain.Open();
                daMain.Update(dsMain, table);
                cnMain.Close();

                // Refresh
                FillDataSet(sql, table);

                // Update the success bool
                success = true;
            }
            catch
            {
                MessageBox.Show("Connection Error Exception");
            }

            return success;
        }
        #endregion
    }
}
