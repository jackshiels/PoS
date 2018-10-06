using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PoS.BusDomain;

namespace PoS.DB
{
    public class ProductDB : DB
    {
        #region Members
        private Collection<Product> prodList;
        private string sqlProd = "SELECT * FROM Product";
        private string tableProd = "Product";
        #endregion

        #region Constructors
        // Passes into the base class and works so nice. Ayylmao
        public ProductDB() : base()
        {
            prodList = new Collection<Product>();
            FillDataSet(sqlProd);
            ReadProducts();
        }
        #endregion

        #region Methods - READ
        private void ReadProducts()
        {
            DataRow myRow = null;
            Product aProd = new Product();

            foreach (DataRow dRow in dsMain.Tables[tableProd].Rows)
            {
                myRow = dRow;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    // Do the conversion stuff here.
                    aProd.ProdID = Convert.ToString(myRow["ProductID"]).TrimEnd();
                    aProd.Name = Convert.ToString(myRow["Name"]).TrimEnd();
                    aProd.Price = (float)Convert.ToDecimal(myRow["Price"]);
                    aProd.Dimensions = DimensionParser(Convert.ToString(myRow["Dimensions"]).TrimEnd());
                    aProd.Weight = (float)Convert.ToDecimal(Convert.ToString(myRow["Weight"]));
                    aProd.Expiry = Convert.ToDateTime(myRow["ExpiryDate"]);
                    // Add to the list
                    prodList.Add(aProd);
                }
            }
        }

        private double[] DimensionParser(string input)
        {
            // Creates an array of double to represent dimensions
            // Dimensions are entered in the format x y z
            string[] splitStrings = input.Split(' ');
            double[] dimArr = new double[3];

            for(int i = 0; i < 3; i++)
            {
                try
                {
                    double dim = Convert.ToDouble(splitStrings[i]);
                    dimArr[i] = dim;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("There has been an error of type " + ex);
                }
            }

            // Finally, return this guy
            return dimArr;
        }
        #endregion

        #region Methods - Generalised
        public Collection<OrderItem> ExpiringList()
        {
            Collection<OrderItem> expiringList = new Collection<OrderItem>();

            return expiringList;
        }

        public Collection<OrderItem> ExpiredList()
        {
            Collection<OrderItem> expiredList = new Collection<OrderItem>();

            return expiredList;
        }
        #endregion

        #region Property Methods
        public Collection<Product> ProdList
        {
            get { return prodList; }
        }
        #endregion
    }
}
