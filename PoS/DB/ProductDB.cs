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
        // This is the smart one. Passes into the base class and works so nice. Ayylmao
        public ProductDB(string Sql, string Table) : base(Sql, Table)
        {
            prodList = new Collection<Product>();
            FillDataSet(sqlProd, tableProd);
            AddProducts(tableProd);
        }
        #endregion

        #region Methods
        private void AddProducts(string table)
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

        #region Property Methods
        public Collection<Product> ProdList
        {
            get { return prodList; }
        }
        #endregion
    }
}
