using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PoS.BusDomain;
using System.Windows.Forms;

namespace PoS.DB
{
    public class OrderDB : DB
    {
        #region Members
        private Collection<Order> ordList;
        private string sqlOrd = "SELECT * FROM [Order]; SELECT * FROM OrderItem; SELECT * FROM OrderItemRegister; SELECT * FROM OrderRegister;";
        private string tableOrd = "Table";
        #endregion

        #region Constructors
        // Passes into the base class and works so nice. Ayylmao
        public OrderDB() : base()
        {
            ordList = new Collection<Order>();
            FillDataSet(sqlOrd);
            ReadOrders();
        }
        #endregion

        #region Methods - CREATE/UPDATE
        // CRUD CREATE
        public bool InsertOrder(Order anOrd)
        {
            bool successful = false;

            

            // Add the order into the list anyway
            ordList.Add(anOrd);

            try
            {
                // Add the order into the DataSet via a new row object in each table

                // --- Order ------------------------------------------

                // Create the insert command
                daMain.InsertCommand = new SqlCommand("INSERT INTO Order (OrderID, Total) VALUES (@ORID, @TOTL);", cnMain);

                // Create the parameters to hide data
                CreateInsertParameters("Order");

                // Create a new row based on the table schema
                DataRow newOrderRow = dsMain.Tables["Table"].NewRow();
                // Parse the order object into the row
                FillRow(newOrderRow, anOrd);
                // Submit it to the table
                dsMain.Tables["Table"].Rows.Add(newOrderRow);

                daMain.Update(dsMain, "Table");

                // --- OrderRegister -------------------------------------------

                // Create the insert command
                daMain.InsertCommand = new SqlCommand("INSERT INTO OrderRegister (OrderID, CustomerID) VALUES (@ORID, @CUID);", cnMain);

                // Create the parameters to hide data
                CreateInsertParameters("OrderRegister");

                // Create a new row based on the table schema
                DataRow newOrdRegRow = dsMain.Tables["Table3"].NewRow();
                // Parse the register object into the row
                FillRow(newOrdRegRow, anOrd);
                // Submit it to the table
                dsMain.Tables["Table3"].Rows.Add(newOrdRegRow);

                daMain.Update(dsMain, "Table3");


                // --- OrderItem ---------------------------------

                foreach (OrderItem item in anOrd.ItemList)
                {
                    // Create the insert command
                    daMain.InsertCommand = new SqlCommand("INSERT INTO OrderItem (OrderItemID, Quantity, Subtotal) VALUES (@OIID, @QUAN, @STOT);", cnMain);

                    // Create the parameters to hide data
                    CreateInsertParameters("OrderItem");

                    // Create a new row based on the table schema
                    DataRow newOItemRow = dsMain.Tables["Table1"].NewRow();
                    // Parse the orderitem object into the row
                    FillRow(newOItemRow, item, anOrd);
                    // Submit it to the table
                    dsMain.Tables["Table1"].Rows.Add(newOItemRow);

                    daMain.Update(dsMain, "Table1");

                    // Create the insert command
                    daMain.InsertCommand = new SqlCommand("INSERT INTO OrderItemRegister (OrderID, ProductID, OrderItemID) VALUES (@OIID, @PRID, @OIID);", cnMain);

                    // Create the parameters to hide data
                    CreateInsertParameters("OrderItemRegister");

                    // Create a new row based on the table schema
                    DataRow newOIRtemRow = dsMain.Tables["Table2"].NewRow();
                    // Parse the orderitemregister object into the row
                    FillRow(newOIRtemRow, item, anOrd);
                    // Submit it to the table
                    dsMain.Tables["Table2"].Rows.Add(newOIRtemRow);

                    daMain.Update(dsMain, "Table2");
                }

                // Execute the command CHECK THIS OUT!!! MIGHT NEED TO USE DAUPDATE
                // daMain.InsertCommand.ExecuteNonQuery();
                UpdateDataSource(sqlOrd);

                // Set true
                successful = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }

            return successful;
        }

        public void CreateInsertParameters(string table)
        {
            if (table == "Order")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@ORID", SqlDbType.NVarChar, 12, "OrderID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@TOTL", SqlDbType.Money, 15, "Total");
                daMain.InsertCommand.Parameters.Add(param);
            }
            else if (table == "OrderItem")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@OIID", SqlDbType.NVarChar, 12, "OrderItemID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@QUAN", SqlDbType.Int, 8, "Quantity");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@STOT", SqlDbType.Money, 12, "Subtotal");
                daMain.InsertCommand.Parameters.Add(param);
            }
            else if (table == "OrderItemRegister")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@OIID", SqlDbType.NVarChar, 12, "OrderItemID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@PRID", SqlDbType.NVarChar, 12, "ProductID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@ORID", SqlDbType.NVarChar, 12, "OrderID");
                daMain.InsertCommand.Parameters.Add(param);
            }
            else if (table == "OrderRegister")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@ORID", SqlDbType.NVarChar, 12, "OrderID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@CUID", SqlDbType.NVarChar, 12, "CustomerID");
                daMain.InsertCommand.Parameters.Add(param);
            }
        }
        // This method fills the given row with appropriate members from a customer object, depending on the table the row comes from
        #endregion

        #region Methods - READ
        private void ReadOrders()
        {
            // Sets the PK manually to allow .Find() to function
            DataColumn[] pk1 = new DataColumn[1];
            pk1[0] = dsMain.Tables["Table3"].Columns[0];
            dsMain.Tables["Table3"].PrimaryKey = pk1;

            // Sets the PK manually to allow .Find() to function
            DataColumn[] pk2 = new DataColumn[1];
            pk2[0] = dsMain.Tables["Table1"].Columns[0];
            dsMain.Tables["Table1"].PrimaryKey = pk2;

            CustomerDB findCust = new CustomerDB();
            ProductDB findProd = new ProductDB();

            foreach (DataRow dRow in dsMain.Tables[tableOrd].Rows)
            {
                Order anOrd = new Order();

                if (!(dRow.RowState == DataRowState.Deleted))
                {
                    // Do the Order conversion stuff here.
                    anOrd.OrderID = Convert.ToString(dRow["OrderID"]).TrimEnd();
                    anOrd.Total = (float)Convert.ToDouble(dRow["Total"]);

                    DataRow temp = dsMain.Tables["Table3"].Rows.Find(Convert.ToString(dRow["OrderID"]));
                    string cust = Convert.ToString(temp["CustomerID"]);

                    // Connect to the customer DB to get the customer
                    Customer owner = findCust.FindCustomerObject(Convert.ToString(temp["CustomerID"]));

                    // check for null
                    if (owner.Name != null)
                    {
                        anOrd.Owner = owner;
                    }
                    else
                    {
                        // If it can't find, throw an error
                        MessageBox.Show("Could not find an owner for order " + Convert.ToString(temp["OrderID"]));
                    }

                    // Now the big one - get every orderitem and insert it into the order object
                    foreach (DataRow rRow in dsMain.Tables["Table2"].Rows)
                    {
                        if (Convert.ToString(rRow["OrderID"]) == Convert.ToString(dRow["OrderID"]))
                        {
                            OrderItem item = new OrderItem();

                            item.OrderItemID = Convert.ToString(rRow["OrderItemID"]).TrimEnd();

                            // Find orderitem
                            DataRow tempRow = dsMain.Tables["Table1"].Rows.Find(Convert.ToString(rRow["OrderItemID"]));
                            // Convert orderitem
                            item.Quantity = Convert.ToInt32(tempRow["Quantity"]);
                            item.SubTotal = (float)Convert.ToDouble(tempRow["Subtotal"]);

                            Product itemProd = findProd.FindProductObject(Convert.ToString(rRow["ProductID"]));
                            item.ItemProduct = itemProd;

                            // Add item to the order's list
                            anOrd.ItemList.Add(item);
                        }
                    }

                    // Add the completed order
                    ordList.Add(anOrd);
                }
            }
        }
        #endregion

        #region Methods - DELETE
        // CRUD CREATE
        public bool Delete(Order anOrd)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateDeleteParameters();

            // Create the sql command for deletion
            daMain.DeleteCommand = new SqlCommand("DELETE FROM Order WHERE OrderID = @ORID; DELETE FROM OrderItem WHERE OrderItemID = @OIID;", cnMain);

            // Checks if the object exists, delete it. Otherwise exit with a failure
            if (DeleteListItem(anOrd) == true)
            {
                try
                {
                    // Delete the order in the DataSet via an existing row object in each table

                    // --- Order ------------------------------------------

                    // Delete a row based on the table schema
                    DataRow updatedOrderRow = dsMain.Tables["Order"].Rows[FindRowIndex(anOrd, "Order")];
                    // Kill it with fire
                    updatedOrderRow.Delete();

                    // --- OrderItem -------------------------------------------

                    foreach(OrderItem item in anOrd.ItemList)
                    {
                        // Update a row based on the table schema
                        DataRow updatedOrderItemRow = dsMain.Tables["OrderItem"].Rows[FindRowIndex(item, "OrderItem")];
                        // End its existence
                        updatedOrderItemRow.Delete();
                    }

                    // SHOULD AUTOMATICALLY CASCADE. TEST THIS!

                    // Execute the command CHECK THIS OUT!!! MIGHT NEED TO USE DAUPDATE
                    // daMain.DeleteCommand.ExecuteNonQuery();
                    UpdateDataSource(sqlOrd);

                    // Set true
                    successful = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error of type " + ex);
                }
            }

            return successful;
        }

        public void CreateDeleteParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@ORID", SqlDbType.NVarChar, 12, "OrderID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@OIID", SqlDbType.NVarChar, 12, "OrderItemID");
            daMain.InsertCommand.Parameters.Add(param);
        }

        public bool DeleteListItem(Order ord)
        {
            bool success = false;

            // Searches for the item, then replaces it
            for (int i = 0; i < ordList.Count; i++)
            {
                if (ordList[i].OrderID == ord.OrderID)
                {
                    ordList.RemoveAt(i);
                    // Set true for return
                    success = true;
                    // Exit loop
                    break;
                }
            }

            return success;
        }
        #endregion

        #region Methods - GENERALISED
        // Used by INSERT and UPDATE
        public void FillRow(DataRow row, Order ord)
        {
            if (row.Table.TableName == "Order")
            {
                row["OrderID"] = ord.OrderID;
                row["Total"] = ord.Total;
            }
            else if (row.Table.TableName == "OrderRegister")
            {
                row["OrderID"] = ord.OrderID;
                row["CustomerID"] = ord.Owner.CustomerID;
            }
        }

        public void FillRow(DataRow row, OrderItem item, Order ord)
        {
            if (row.Table.TableName == "OrderItem")
            {
                row["OrderItemID"] = item.OrderItemID;
                row["Quantity"] = item.Quantity;
                row["Subtotal"] = item.SubTotal;
            }
            else if (row.Table.TableName == "OrderItemRegister")
            {
                row["OrderID"] = ord.OrderID;
                row["ProductID"] = item.ItemProduct.ProdID;
                row["OrderItemID"] = item.OrderItemID;
            }
        }

        // Used by UPDATE and DELETE
        private int FindRowIndex(Order ord, string table)
        {
            int rowIndex = 0;
            int returnValue = -1;

            foreach (DataRow row in dsMain.Tables[table].Rows)
            {
                if (!(row.RowState == DataRowState.Deleted))
                {
                    if (ord.OrderID == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["Order"]))
                    {
                        returnValue = rowIndex;
                    }
                }
                rowIndex += 1;
            }

            return returnValue;
        }

        private int FindRowIndex(OrderItem ord, string table)
        {
            int rowIndex = 0;
            int returnValue = -1;

            foreach (DataRow row in dsMain.Tables[table].Rows)
            {
                if (!(row.RowState == DataRowState.Deleted))
                {
                    if (ord.OrderItemID == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["OrderItem"]))
                    {
                        returnValue = rowIndex;
                    }
                }
                rowIndex += 1;
            }

            return returnValue;
        }

        public Order FindOrder(string orderId)
        {
            Order ord = new Order();

            // Iterate
            foreach(Order orda in ordList)
            {
                if (ord.OrderID == orderId)
                {
                    ord = orda;
                }
            }

            return ord;
        }
        #endregion

        #region Property Methods
        public Collection<Order> OrdList
        {
            get { return ordList; }
        }
        #endregion
    }
}
