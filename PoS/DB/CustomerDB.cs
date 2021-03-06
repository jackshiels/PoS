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
        private string sqlCust = "SELECT * FROM CustomerRegister; SELECT * FROM Customer; SELECT * FROM Person";
        #endregion

        #region Constructors
        // This is the smart one. Passes into the base class and works so nice. Ayylmao
        public CustomerDB() : base()
        {
            custList = new Collection<Customer>();
            FillDataSet(sqlCust);
            ReadCustomers();
        }
        #endregion

        #region Methods - CREATE
        // CRUD CREATE
        public bool InsertCustomer(Customer aCust)
        {
            bool successful = false;

            // Add the customer into the list anyway
            custList.Add(aCust);

            try
            {
                // Add the customer into the DataSet via a new row object in each table

                // --- CUSTOMER ------------------------------------------

                // Create the insert command
                daMain.InsertCommand = new SqlCommand("INSERT INTO Customer (CustomerID, Payment, Debt, BlackListed) VALUES (@CUID, @PMNT, @DEBT, @BLCK);", cnMain);

                // Create the parameters to hide data
                CreateInsertParameters("Customer");

                // Create a new row based on the table schema
                DataRow newCustRow = dsMain.Tables["Table1"].NewRow();
                // Parse the customer object into the row
                FillRow(newCustRow, aCust);
                // Submit it to the table
                dsMain.Tables["Table1"].Rows.Add(newCustRow);

                cnMain.Open();
                daMain.Update(dsMain, "Table1");

                // --- PERSON -------------------------------------------

                // Create the insert command
                daMain.InsertCommand = new SqlCommand("INSERT INTO Person (PersonID, Name, Address) VALUES (@PEID, @PENM, @ADDR);", cnMain);

                // Create the parameters to hide data
                CreateInsertParameters("Person");

                // Create a new row based on the table schema
                DataRow newPersRow = dsMain.Tables["Table2"].NewRow();
                // Parse the customer object into the row
                FillRow(newPersRow, aCust);
                // Submit it to the table
                dsMain.Tables["Table2"].Rows.Add(newPersRow);

                daMain.Update(dsMain, "Table2");

                // --- CUSTOMERREGISTER ---------------------------------

                // Create the insert command
                daMain.InsertCommand = new SqlCommand("INSERT INTO CustomerRegister (CustomerID, PersonID) VALUES (@CUID, @PEID);", cnMain);

                // Create the parameters to hide data
                CreateInsertParameters("CustomerRegister");

                // Create a new row based on the table schema
                DataRow newRegRow = dsMain.Tables["Table"].NewRow();
                // Parse the customer object into the row
                FillRow(newRegRow, aCust);
                // Submit it to the table
                dsMain.Tables["Table"].Rows.Add(newRegRow);

                daMain.Update(dsMain, "Table");
                cnMain.Close();

                FillDataSet(sqlCust);

                // Set true
                successful = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }

            return successful;
        }

        public void CreateInsertParameters(string table)
        {
            daMain.InsertCommand.Parameters.Clear();

            if (table == "Customer")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@CUID", SqlDbType.NVarChar, 12, "CustomerID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@PMNT", SqlDbType.NVarChar, 50, "Payment");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@DEBT", SqlDbType.Money, 50, "Debt");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@BLCK", SqlDbType.Int, 1, "BlackListed");
                daMain.InsertCommand.Parameters.Add(param);
            }
            else if (table == "Person")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@PEID", SqlDbType.NVarChar, 12, "PersonID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@PENM", SqlDbType.NVarChar, 50, "Name");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@ADDR", SqlDbType.NVarChar, 100, "Address");
                daMain.InsertCommand.Parameters.Add(param);
            }
            else if (table == "CustomerRegister")
            {
                SqlParameter param = default(SqlParameter);
                param = new SqlParameter("@CUID", SqlDbType.NVarChar, 12, "CustomerID");
                daMain.InsertCommand.Parameters.Add(param);

                param = new SqlParameter("@PEID", SqlDbType.NVarChar, 12, "PersonID");
                daMain.InsertCommand.Parameters.Add(param);
            }
        }
        #endregion

        #region Methods - READ
        public void ReadCustomers()
        {
            Customer aCust = new Customer();

            // Sets the PK manually to allow .Find() to function
            DataColumn[] pk1 = new DataColumn[1];
            pk1[0] = dsMain.Tables["Table1"].Columns[0];
            dsMain.Tables["Table1"].PrimaryKey = pk1;

            // Sets the PK manually to allow .Find() to function
            DataColumn[] pk2 = new DataColumn[1];
            pk2[0] = dsMain.Tables["Table2"].Columns[0];
            dsMain.Tables["Table2"].PrimaryKey = pk2;

            try
            {
                // Parses the table to get data for each customer
                foreach (DataRow dRow in dsMain.Tables["Table"].Rows)
                {
                    if (!(dRow.RowState == DataRowState.Deleted))
                    {
                        // Do the conversion stuff here.
                        aCust.CustomerID = Convert.ToString(dRow["CustomerID"]).TrimEnd();

                        // Creates a row from the customer table that shares the same key from CustomerRegister
                        DataRow temp = dsMain.Tables["Table1"].Rows.Find(Convert.ToString(dRow["CustomerID"]));

                        if (Convert.ToString(temp["Payment"]) == "EFT")
                        {
                            aCust.Payment = Customer.PaymentMethod.EFT;
                        }
                        else
                        {
                            aCust.Payment = Customer.PaymentMethod.CreditCard;
                            aCust.CardHolderDetails = CreatePaymentArray(Convert.ToString(temp["Payment"]));
                        }

                        aCust.Debt = (float)Convert.ToDouble(temp["Debt"]);
                        aCust.BlackListed = Convert.ToInt32(temp["BlackListed"]);

                        // Creates a row from the person table that shares the same key from CustomerRegister
                        temp = dsMain.Tables["Table2"].Rows.Find(Convert.ToString(dRow["PersonID"]));
                        aCust.Name = Convert.ToString(temp["Name"]).TrimEnd();
                        aCust.Address = Convert.ToString(temp["Address"]).TrimEnd();

                        // Add to the list
                        custList.Add(aCust);

                        aCust = new Customer();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error of type " + ex);
            }
        }

        public string[] CreatePaymentArray(string value)
        {
            string[] newArr = new string[3];

            if (value == "000")
            {
                newArr[0] = "0";
                newArr[1] = "0";
                newArr[2] = "0";
            }
            else
            {
                string passed = value;

                newArr[0] = passed.Substring(0, 16);
                newArr[1] = passed.Substring(16, 3);
                newArr[2] = passed.Substring(19, (passed.Length - 19));
            }

            return newArr;
        }
        #endregion

        #region Methods - UPDATE
        public bool UpdateCustomer(Customer aCust)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateUpdateParameters();

            // Create the insert command
            daMain.UpdateCommand = new SqlCommand("UPDATE Customer SET Payment = @PMNT, Debt = @DEBT, BlackListed = @BLCK WHERE CustomerID = @CUID; UPDATE Person SET Name = @PENM, Address = @ADDR WHERE PersonID = @PEID;", cnMain);

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
                    UpdateDataSource(sqlCust);

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
            param = new SqlParameter("@CUID", SqlDbType.NVarChar, 12, "CustomerID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PMNT", SqlDbType.NVarChar, 50, "Payment");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DEBT", SqlDbType.Money, 50, "Debt");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@BLCK", SqlDbType.Int, 1, "BlackListed");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PEID", SqlDbType.NVarChar, 12, "PersonID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PENM", SqlDbType.NVarChar, 50, "Name");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ADDR", SqlDbType.NVarChar, 100, "Address");
            daMain.InsertCommand.Parameters.Add(param);

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
                    UpdateDataSource(sqlCust);

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
            if (row.Table.TableName == "Table1")
            {
                row["CustomerID"] = cust.CustomerID;
                row["BlackListed"] = Convert.ToInt32(cust.BlackListed);
                row["Debt"] = Convert.ToDecimal(cust.Debt);
                // Converts the array into a single string
                row["Payment"] = Convert.ToString(cust.CardHolderDetails[0] + cust.CardHolderDetails[1] + cust.CardHolderDetails[2]);
            }
            else if (row.Table.TableName == "Table2")
            {
                row["PersonID"] = cust.PersonID;
                row["Name"] = cust.Name;
                row["Address"] = cust.Address;
            }
            else if (row.Table.TableName == "Table")
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
                    if (aCust.CustomerID == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["CustomerID"]))
                    {
                        returnValue = rowIndex;
                        break;
                    }
                }
                rowIndex += 1;
            }

            return returnValue;
        }

        public Customer FindCustomerObject(string custId)
        {
            Customer foundCust = new Customer();

            // Search for this thing
            for (int i = 0; i < custList.Count; i++)
            {
                if (custList[i].CustomerID == custId)
                {
                    foundCust = custList[i];
                    break;
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
