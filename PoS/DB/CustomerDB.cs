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
        private string sqlProd = "SELECT * FROM CustomerRegister, SELECT * FROM Customer, SELECT * FROM Person";
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
            DataRow myRow = null;
            Customer aCust = new Customer();

            foreach (DataRow dRow in dsMain.Tables["CustomerRegister"].Rows)
            {
                myRow = dRow;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    // Do the conversion stuff here.
                    
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
