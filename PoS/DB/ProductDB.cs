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
        private string tableProd = "Table";
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
                    aProd.Stock = Convert.ToInt32(myRow["Stock"]);
                }

                if (Convert.ToDateTime(aProd.Expiry) <= DateTime.Now || Convert.ToDateTime(aProd.Expiry) <= (DateTime.Now.AddDays(7)) ) //if the product is expired
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

        public Product FindProductObject(string name)
        {
            Product foundProd = new Product();

            // Search for this thing
            for (int i = 0; i < prodList.Count; i++)
            {
                if (prodList[i].Name.Equals(name))
                {
                    foundProd = prodList[i];
                    break;
                } }

            return foundProd;
        }
        #endregion

        #region Methods - READ
        private void ReadProducts()
        {
            DataRow myRow = null;
            Product aProd =  new Product();

            // Sets the PK manually to allow .Find() to function
            DataColumn[] pk1 = new DataColumn[1];
            pk1[0] = dsMain.Tables["Table"].Columns[0];
            dsMain.Tables["Table"].PrimaryKey = pk1;

            try
            {
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
                        aProd.Location = Convert.ToString(myRow["Location"]).TrimEnd();
                        aProd.Stock = Convert.ToInt32(myRow["Stock"]);
                        // Add to the list
                        prodList.Add(aProd);

                        aProd = new Product();
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

        public int FindNumProduct(string name)
        {
            int count = 0;
            foreach (Product product in prodList)
            {
                if (name.Equals(product.Name))
                {
                    count = product.Stock;
                }
            }
            return count;
        }
        #endregion

        #region Methods - UPDATE
        public bool CommitUpdate()
        {
            bool successful = false;
            try
            {
                daMain.Update(dsMain, "Table");
                successful = true;
                FillDataSet(sqlProd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error of type " + ex);
            }
            return successful;
        }

        public void CreateUpdateParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@PRID", SqlDbType.NVarChar, 12, "ProductID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PRNM", SqlDbType.NVarChar, 50, "Name");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PRIC", SqlDbType.Float, 10, "Price");
            daMain.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@DIMS", SqlDbType.NVarChar, 40, "Dimensions");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@WGHT", SqlDbType.Float, 50, "Weight");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@LOCN", SqlDbType.NVarChar, 20, "Location");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@EXPD", SqlDbType.Date, 100, "ExpiryDate");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@STCK", SqlDbType.Int, 1, "Stock");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        public bool ReserveProduct(string name, int quantity)
        {
            Product aProd = new Product();

            foreach (Product prod in prodList)
            {
                if ((prod.Name == name) && prod.Stock >= quantity)
                {
                    aProd = prod;
                    aProd.Stock -= quantity;
                }
            }

            bool successful = false;

            // Create the update command
            daMain.UpdateCommand = new SqlCommand("UPDATE Product SET Stock = @STCK WHERE ProductID = @PRID;", cnMain);

            // Create the parameters to hide data
            CreateUpdateParameters();

            try
            {
                DataRow updatedProdRow = dsMain.Tables["Table"].Rows[FindRowIndex(aProd, "Table")];
                // Parse the product object into the row
                FillRow(updatedProdRow, aProd);

                cnMain.Open();
                daMain.Update(dsMain, "Table");
                cnMain.Close();

                successful = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error of type " + ex);
            }
            return successful;
        }
        #endregion

        #region Methods - Generalised
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
            row["Stock"] = aProd.Stock;
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
