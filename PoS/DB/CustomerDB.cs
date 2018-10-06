﻿using System;
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
    public class CustomerDB : DB
    {
        #region Members
        private Collection<Customer> custList;
        private string sqlProd = "SELECT * FROM CustomerRegister; SELECT * FROM Customer; SELECT * FROM Person";
        #endregion

        #region Constructors
        // This is the smart one. Passes into the base class and works so nice. Ayylmao
        public CustomerDB() : base()
        {
            custList = new Collection<Customer>();
            FillDataSet(sqlProd);
            ReadCustomers();
        }
        #endregion

        #region Methods - CREATE
        // CRUD CREATE
        public bool InsertCustomer(Customer aCust)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateInsertParameters();

            // Create the insert command
            daMain.InsertCommand = new SqlCommand("INSERT INTO Customer (CustomerID, Payment, BlackListed) VALUES (@CUID, @PMNT, @BLCK); INSERT INTO Person (PersonID, Name, Address, DateOfBirth) VALUES (@PEID, @PENM, @ADDR, @DOFB); INSERT INTO CustomerRegister (CustomerID, PersonID) VALUES (@CUID, @PEID);", cnMain);

            // Add the customer into the list anyway
            custList.Add(aCust);

            try
            {
                // Add the customer into the DataSet via a new row object in each table

                // --- CUSTOMER ------------------------------------------

                // Create a new row based on the table schema
                DataRow newCustRow = dsMain.Tables["Customer"].NewRow();
                // Parse the customer object into the row
                FillRow(newCustRow, aCust);
                // Submit it to the table
                dsMain.Tables["Customer"].Rows.Add(newCustRow);

                // --- PERSON -------------------------------------------

                // Create a new row based on the table schema
                DataRow newPersRow = dsMain.Tables["Person"].NewRow();
                // Parse the customer object into the row
                FillRow(newPersRow, aCust);
                // Submit it to the table
                dsMain.Tables["Person"].Rows.Add(newPersRow);

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
            catch(Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }

            return successful;
        }
        public void CreateInsertParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@CUID", SqlDbType.Int, 10, "CustomerID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PMNT", SqlDbType.NVarChar, 50, "Payment");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@BLCK", SqlDbType.Bit, 1, "BlackListed");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PEID", SqlDbType.Int, 10, "PersonID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PENM", SqlDbType.NVarChar, 50, "Name");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ADDR", SqlDbType.NVarChar, 100, "Address");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DOFB", SqlDbType.Date, 50, "DateOfBirth");
            daMain.InsertCommand.Parameters.Add(param);
        }
        // This method fills the given row with appropriate members from a customer object, depending on the table the row comes from
        #endregion

        #region Methods - READ
        public void ReadCustomers()
        {
            Customer aCust = new Customer();

            // Parses the table to get data for each customer
            foreach (DataRow dRow in dsMain.Tables["CustomerRegister"].Rows)
            {
                if (!(dRow.RowState == DataRowState.Deleted))
                {
                    // Do the conversion stuff here.
                    aCust.CustomerID = Convert.ToInt32(dRow["CustomerID"]);
                    // Creates a row from the customer table that shares the same key from CustomerRegister
                    DataRow temp = dsMain.Tables["Customer"].Rows.Find(Convert.ToInt32(dRow["CustomerID"]));
                    aCust.Payment = Convert.ToString(temp["Payment"]);
                    aCust.BlackListed = Convert.ToInt32(temp["BlackListed"]);
                    // Creates a row from the person table that shares the same key from CustomerRegister
                    temp = dsMain.Tables["Person"].Rows.Find(Convert.ToInt32(dRow["PersonID"]));
                    aCust.Name = Convert.ToString(temp["Name"]);
                    aCust.Address = Convert.ToString(temp["Address"]);
                    aCust.DOB = Convert.ToDateTime(temp["DateOfBirth"]);
                    
                    // Add to the list
                    custList.Add(aCust);
                }
            }
        }
        #endregion

        #region Methods - UPDATE
        public bool UpdateCustomer(Customer aCust)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateUpdateParameters();

            // Create the insert command
            daMain.UpdateCommand = new SqlCommand("UPDATE Customer SET Payment = @PMNT WHERE CustomerID = @CUID; UPDATE Person SET Name = @PENM, Address = @ADDR, DateOfBirth = @DOFB WHERE PersonID = @PEID;", cnMain);

            // Checks if the object exists, replace it. Otherwise exit with a failure
            if (ReplaceListItem(aCust) == true)
            {
                try
                {
                    // Update the customer in the DataSet via an existing row object in each table

                    // --- CUSTOMER ------------------------------------------

                    // Update a row based on the table schema
                    DataRow updatedCustRow = dsMain.Tables["Customer"].Rows[FindRowIndex(aCust, "Customer")];
                    // Parse the customer object into the row
                    FillRow(updatedCustRow,aCust);

                    // --- PERSON -------------------------------------------

                    // Update a row based on the table schema
                    DataRow updatedPersRow = dsMain.Tables["Person"].Rows[FindRowIndex(aCust, "Person")];
                    // Parse the customer object into the row
                    FillRow(updatedPersRow, aCust);

                    // --- CUSTOMERREGISTER ---------------------------------

                    // SHOULD AUTOMATICALLY CASCADE. TEST THIS!

                    // Execute the command CHECK THIS OUT!!! MIGHT NEED TO USE DAUPDATE
                    // daMain.UpdateCommand.ExecuteNonQuery();
                    // UpdateDataSource(sqlProd);

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

        public void CreateUpdateParameters()
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@CUID", SqlDbType.Int, 10, "CustomerID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PMNT", SqlDbType.NVarChar, 50, "Payment");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PEID", SqlDbType.Int, 10, "PersonID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PENM", SqlDbType.NVarChar, 50, "Name");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ADDR", SqlDbType.NVarChar, 100, "Address");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@DOFB", SqlDbType.Date, 50, "DateOfBirth");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        public bool ReplaceListItem(Customer aCust)
        {
            bool success = false;

            // Searches for the item, then replaces it
            for (int i = 0; i < custList.Count; i++)
            {
                if (custList[i].CustomerID == aCust.CustomerID)
                {
                    custList.RemoveAt(i);
                    custList[i] = aCust;
                    // Set true for return
                    success = true;
                    // Exit loop
                    break;
                }
            }

            return success;
        }
        #endregion

        #region Methods - DELETE
        public bool DeleteCustomer(Customer aCust)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateDeleteParameters();

            // Create the sql command for deletion
            daMain.DeleteCommand = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @CUID; DELETE FROM Person WHERE PersonID = @PEID;", cnMain);

            // Checks if the object exists, delete it. Otherwise exit with a failure
            if (DeleteListItem(aCust) == true)
            {
                try
                {
                    // Delete the customer in the DataSet via an existing row object in each table

                    // --- CUSTOMER ------------------------------------------

                    // Delete a row based on the table schema
                    DataRow updatedCustRow = dsMain.Tables["Customer"].Rows[FindRowIndex(aCust, "Customer")];
                    // Kill it with fire
                    updatedCustRow.Delete();

                    // --- PERSON -------------------------------------------

                    // Update a row based on the table schema
                    DataRow updatedPersRow = dsMain.Tables["Person"].Rows[FindRowIndex(aCust, "Person")];
                    // End its existence
                    updatedPersRow.Delete();

                    // --- CUSTOMERREGISTER ---------------------------------

                    // SHOULD AUTOMATICALLY CASCADE. TEST THIS!

                    // Execute the command CHECK THIS OUT!!! MIGHT NEED TO USE DAUPDATE
                    // daMain.DeleteCommand.ExecuteNonQuery();
                    // UpdateDataSource(sqlProd);

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
            param = new SqlParameter("@CUID", SqlDbType.Int, 10, "CustomerID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PEID", SqlDbType.Int, 10, "PersonID");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        public bool DeleteListItem(Customer aCust)
        {
            bool success = false;

            // Searches for the item, then replaces it
            for (int i = 0; i < custList.Count; i++)
            {
                if (custList[i].CustomerID == aCust.CustomerID)
                {
                    custList.RemoveAt(i);
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
        public void FillRow(DataRow row, Customer cust)
        {
            if (row.Table.TableName == "Customer")
            {
                row["CustomerID"] = cust.CustomerID;
                row["Payment"] = cust.Payment;
            }
            else if (row.Table.TableName == "Person")
            {
                row["PersonID"] = cust.PersonID;
                row["Name"] = cust.Name;
                row["Address"] = cust.Address;
                row["DateOfBirth"] = cust.DOB;
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
                        break;
                    }
                }
                rowIndex += 1;
            }

            return returnValue;
        }

        public Customer FindCustomerObject(int custId)
        {
            Customer foundCust = new Customer();

            // Search for this thing
            for (int i = 0; i < custList.Count; i++)
            {
                if (custList[i].CustomerID == custId)
                {
                    foundCust = custList[i];
                }
            }

            return foundCust;
        }
        #endregion

        #region Property Methods
        public Collection<Customer> CustList
        {
            get { return custList; }
        }
        #endregion
    }
}
