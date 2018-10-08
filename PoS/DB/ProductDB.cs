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

        #region Methods - Generalised
        public Collection<OrderItem> ExpiryList()
        {
            Collection<OrderItem> expiryList = new Collection<OrderItem>();
            DataRow myRow = null;

            foreach (DataRow dRow in dsMain.Tables[tableProd].Rows) //Iterate through every row in the product table
            {
                myRow = dRow;
                Product aProd = new Product();
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    // Fill in the product Item with all the appropriate details
                    aProd.ProdID = Convert.ToString(myRow["ProductID"]).TrimEnd();
                    aProd.Name = Convert.ToString(myRow["Name"]).TrimEnd();
                    aProd.Price = (float)Convert.ToDecimal(myRow["Price"]);
                    aProd.Dimensions = DimensionParser(Convert.ToString(myRow["Dimensions"]).TrimEnd());
                    aProd.Weight = (float)Convert.ToDecimal(Convert.ToString(myRow["Weight"]));
                    aProd.Expiry = Convert.ToDateTime(myRow["ExpiryDate"]);
                    aProd.Location = Convert.ToString(myRow["Location"]);
                    aProd.Reserved = Convert.ToByte(myRow["Reserved"]);
                }

                if (aProd.Expiry <= DateTime.Now || aProd.Expiry <= (DateTime.Now.AddDays(7)) ) //if the product is expired
                {
                    for (int i = 0; i < expiryList.Count(); i++)  //iterate through all orderItems already in the Collection
                    {
                        if (expiryList[i].ItemProduct.Name.Equals(aProd.Name)) //if it finds its product within the list add to quantity
                        {
                            expiryList[i].Quantity += 1;
                        }
                        else if (!(expiryList[i].ItemProduct.Name.Equals(aProd.Name)) && expiryList[i + 1] == null) //if it hasen't matched yet and its at thelast item on the list, create an order item for it in the list
                        {
                            expiryList.Add(new OrderItem(aProd,1));
                        }
                        else //otherwise just continue looping through the list
                            continue;
                    }
                }
            }

            return expiryList;
        }
        #endregion

        #region Methods - READ
        private void ReadProducts()
        {
            DataRow myRow = null;
            Product aProd;

            try
            {
                foreach (DataRow dRow in dsMain.Tables[tableProd].Rows)
                {
                    aProd = new Product();
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
                        aProd.Location = Convert.ToString(myRow["Location"]).TrimEnd();
                        aProd.Reserved = Convert.ToInt32(myRow["Reserved"]);
                        // Add to the list
                        prodList.Add(aProd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }

        }

        private double[] DimensionParser(string input)
        {
            // Creates an array of double to represent dimensions
            // Dimensions are entered in the format x y z (note spacing)
            string[] splitStrings = input.Split(' ');
            double[] dimArr = new double[3];

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    double dim = Convert.ToDouble(splitStrings[i]);
                    dimArr[i] = dim;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There has been an error of type " + ex);
                }
            }

            // Finally, return this guy
            return dimArr;
        }

        public Product FindNonReservedProduct(string name)
        {
            Product aProd = null;
            foreach(Product product in prodList)
            {
                if (name.Equals(product.Name) && product.Reserved == 0)
                {
                    aProd = product;
                    break;
                }
            }
            return aProd;
        }

        public int FindNumProduct(string name)
        {
            int count = 0;
            foreach (Product product in prodList)
            {
                if (name.Equals(product.Name) && product.Reserved == 0)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region Methods - UPDATE
        public void CreateUpdateParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@PRID", SqlDbType.Int, 30, "ProductID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PRNM", SqlDbType.NVarChar, 50, "Name");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PRIC", SqlDbType.Float, 10, "Price");
            daMain.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@DIMS", SqlDbType.NVarChar, 40, "Dimensions");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@WGHT", SqlDbType.Float, 53, "Weight");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@LOCN", SqlDbType.NVarChar, 20, "Location");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@EXPD", SqlDbType.Date, 100, "ExpiryDate");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@RSVD", SqlDbType.Int, 1, "Reserved");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        public bool ReserveProduct(string name)
        {
            Product aProd = FindNonReservedProduct(name);
            aProd.Reserved = 1;
            bool successful = false;
            // Create the parameters to hide data
            CreateUpdateParameters();
            // Create the insert command
            daMain.UpdateCommand = new SqlCommand("UPDATE Product SET Reserved = @RSVD WHERE ProductID = @PRID;", cnMain);
            try
            {
                DataRow updatedProdRow = dsMain.Tables["Product"].Rows[FindRowIndex(aProd, "Product")];
                // Parse the product object into the row
                FillRow(updatedProdRow, aProd);
                UpdateDataSource(sqlProd);

                successful = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error of type " + ex);
            }
            return successful;
        }
        #endregion

        #region Property Methods
        public Collection<Product> ProdList
        {
            get { return prodList; }
        }
        #endregion

        #region Methods - Generalized
        private int FindRowIndex(Product aProd, string table)
        {
            int rowIndex = 0;
            int returnValue = -1;

            foreach (DataRow row in dsMain.Tables[table].Rows)
            {
                if (!(row.RowState == DataRowState.Deleted))
                {
                    if (aProd.ProdID == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["ProductID"]))
                    {
                        returnValue = rowIndex;
                        break;
                    }
                }
                rowIndex += 1;
            }
            return returnValue;
        }

        public void FillRow(DataRow row, Product aProd)
        {
            row["Reserved"] = aProd.Reserved;
        }
        #endregion
    }
}
