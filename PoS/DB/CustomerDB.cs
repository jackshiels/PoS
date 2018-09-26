using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PoS.BusDomain;

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

        #region Methods
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
