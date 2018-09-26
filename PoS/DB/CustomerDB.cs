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
    class CustomerDB : DB
    {
        #region Members
        private Collection<Customer> custList;
        private string sqlProd = "SELECT * FROM CustomerRegister; SELECT * FROM Customer, SELECT * FROM Person";
        #endregion

        #region Constructors
        // This is the smart one. Passes into the base class and works so nice. Ayylmao
        public CustomerDB(string Sql) : base(Sql)
        {
            custList = new Collection<Customer>();
            FillDataSet(sqlProd);
            ReadCustomers();
        }
        #endregion

        #region Methods - CREATE
        public bool InsertCustomer(Customer aCust)
        {
            bool successful = false;

            // Create the parameters to hide data
            CreateInsertParameters();

            // Create the insert command
            daMain.InsertCommand = new SqlCommand("INSERT INTO Customer (CustomerID, Payment) VALUES (@CUID, @PMNT); INSERT INTO Person (PersonID, Name, Address, DateOfBirth) VALUES (@PEID, @PENM, @ADDR, @DOFB); INSERT INTO CustomerRegister (CustomerID, PersonID) VALUES (@CUID, @PEID);");


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

                // Execute the command
                daMain.InsertCommand.ExecuteNonQuery();

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
        #endregion

        #region Methods - READ
        // CRUD: READ
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
                    aCust.Name = Convert.ToString(temp["Name"]);
                    aCust.Address = Convert.ToString(temp["Address"]);
                    aCust.Name = Convert.ToString(temp["Name"]);
                    aCust.DOB = Convert.ToDateTime(temp["DateOfBirth"]);
                    aCust.Payment = Convert.ToString(temp["Payment"]);
                    // Add to the list
                    custList.Add(aCust);
                }
            }
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
