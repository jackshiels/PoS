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
        private Collection<OrderItem> ordItemList;
        private string sqlOrd = "SELECT * FROM Order; SELECT * FROM OrderItem SELECT * FROM OrderItemRegister; SELECT * FROM OrderRegister;";
        private string tableOrd = "Order";
        #endregion

        #region Constructors
        // Passes into the base class and works so nice. Ayylmao
        public OrderDB(string sql) : base(sql)
        {
            ordList = new Collection<Order>();
            FillDataSet(sqlOrd);
            ReadOrders();
        }
        #endregion

        #region Methods - CREATE
        // CRUD CREATE
        public bool InsertOrder(Order anOrd)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateInsertParameters();

            // Create the insert command
            daMain.InsertCommand = new SqlCommand("INSERT INTO Order (OrderID, DeliveryDate, Total) VALUES (@ORID, @DELD, @TOTL); INSERT INTO OrderRegister (OrderID, CustomerID) VALUES (@ORID, @CUID); INSERT INTO OrderItem (OrderItemID, Quantity, Subtotal) VALUES (@OIID, @QUAN, @STOT); INSERT INTO OrderItemRegister (OrderID, ProductID, OrderItemID) VALUES (@ORID, @PRID, @OIID);", cnMain);

            // Add the customer into the list anyway
            ordList.Add(anOrd);

            try
            {
                // Add the order into the DataSet via a new row object in each table

                // --- Order ------------------------------------------

                // Create a new row based on the table schema
                DataRow newOrderRow = dsMain.Tables["Order"].NewRow();
                // Parse the order object into the row
                FillRow(newOrderRow, anOrd);
                // Submit it to the table
                dsMain.Tables["Order"].Rows.Add(newOrderRow);

                // --- OrderRegister -------------------------------------------

                // Create a new row based on the table schema
                DataRow newOrdRegRow = dsMain.Tables["OrderRegister"].NewRow();
                // Parse the customer object into the row
                FillRow(newOrdRegRow, anOrd);
                // Submit it to the table
                dsMain.Tables["OrderRegister"].Rows.Add(newOrdRegRow);

                // --- CUSTOMERREGISTER ---------------------------------

                // Create a new row based on the table schema
                DataRow newRegRow = dsMain.Tables["CustomerRegister"].NewRow();
                // Parse the customer object into the row
                FillRow(newRegRow, aCust);
                // Submit it to the table
                dsMain.Tables["CustomerRegister"].Rows.Add(newRegRow);

                // Execute the command CHECK THIS OUT!!! MIGHT NEED TO USE DAUPDATE
                // daMain.InsertCommand.ExecuteNonQuery();
                // UpdateDataSource(sqlProd);

                // Set true
                successful = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }

            return successful;
        }

        public void CreateInsertParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@ORID", SqlDbType.NVarChar, 12, "OrderID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DELD", SqlDbType.Date, 20, "DeliveryDate");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@TOTL", SqlDbType.Money, 15, "Total");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@OIID", SqlDbType.NVarChar, 12, "OrderItemID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@QUAN", SqlDbType.Int, 12, "Quantity");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@STOT", SqlDbType.Money, 12, "Subtotal");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PRID", SqlDbType.NVarChar, 12, "ProductID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@CUID", SqlDbType.NVarChar, 12, "CustomerID");
            daMain.InsertCommand.Parameters.Add(param);
        }
        // This method fills the given row with appropriate members from a customer object, depending on the table the row comes from
        #endregion

        #region Methods - READ
        private void ReadOrders()
        {
            foreach (DataRow dRow in dsMain.Tables[tableOrd].Rows)
            {
                if (!(dRow.RowState == DataRowState.Deleted))
                {
                    Order anOrd = new Order();

                    // Do the Order conversion stuff here.
                    anOrd.OrderID = Convert.ToInt32(dRow["OrderID"]);
                    anOrd.DeliveryDate = Convert.ToDateTime(dRow["DeliveryDate"]);
                    anOrd.Total = (float)Convert.ToDouble(dRow["Total"]);
                    anOrd.Address = Convert.ToString(dRow["Address"]);

                    // Find the corresponding customer id
                    // THIS MAY THROW AN ERROR, AS THE PK IS TWO-PART. CHECK!
                    DataRow temp = dsMain.Tables["OrderRegister"].Rows.Find(Convert.ToInt32(dRow["OrderID"]));
                    int cust = Convert.ToInt32(temp["CustomerID"]);

                    // Connect to the customer DB to get the customer
                    CustomerDB findCust = new CustomerDB();
                    Customer owner = findCust.FindCustomerObject(Convert.ToInt32(temp["CustomerID"]));

                    // check for null
                    if (owner.Name != null)
                    {
                        anOrd.Owner = findCust.FindCustomerObject(Convert.ToInt32(temp["CustomerID"]));
                    }
                    else
                    {
                        // If it can't find, throw an error
                        MessageBox.Show("Could not find an owner for order " + Convert.ToString(temp["OrderID"]));
                    }

                    // Now the big one - get every orderitem and insert it into the order object
                    foreach (DataRow rRow in dsMain.Tables["OrderItemRegister"].Rows)
                    {
                        if (!(Convert.ToInt32(rRow["OrderID"]) != Convert.ToInt32(dRow["OrderID"])))
                        {
                            // Loop through items
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods - UPDATE    
        #endregion

        #region Methods - DELETE
        #endregion

        #region Methods - GENERALISED
        // Used by INSERT and UPDATE
        public void FillRow(DataRow row, Order ord)
        {
            if (row.Table.TableName == "Order")
            {
                row["OrderID"] = ord.OrderID;
                row["DeliveryDate"] = ord.DeliveryDate;
                row["Total"] = ord.Total;
            }
            else if (row.Table.TableName == "OrderRegister")
            {
                row["OrderID"] = ord.OrderID;
                row["CustomerID"] = ord.Owner.CustomerID;

            }
            else if (row.Table.TableName == "CustomerRegister")
            {
                row["CustomerID"] = cust.CustomerID;
                row["PersonID"] = cust.PersonID;
            }
        }

        // Used by UPDATE and DELETE
        private int FindRowIndex(Customer aCust, string table)
        {
            int rowIndex = 0;
            int returnValue = -1;

            foreach (DataRow row in dsMain.Tables[table].Rows)
            {
                if (!(row.RowState == DataRowState.Deleted))
                {
                    if (aCust.CustomerID == Convert.ToInt32(dsMain.Tables[table].Rows[rowIndex]["CustomerID"]))
                    {
                        returnValue = rowIndex;
                    }
                }
                rowIndex += 1;
            }

            return returnValue;
        }
        #endregion

        #region Property Methods
        public Collection<Order> OrdList
        {
            get { return ordList; }
        }
        public Collection<OrderItem> OrdItemList
        {
            get { return ordItemList; }
        }
        #endregion
    }
}
