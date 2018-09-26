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
        private string sqlProd = "SELECT * FROM Product";
        private string tableProd = "Product";
        #endregion

        #region Constructors
        // This is the smart one. Passes into the base class and works so nice. Ayylmao
        public CustomerDB(string Sql, string Table) : base(Sql, Table)
        {
            custList = new Collection<Customer>();
            FillDataSet(sqlProd, tableProd);
            ReadCustomers(tableProd);
        }
        #endregion

        #region Methods
        public void ReadCustomers(string table)
        {
            DataRow myRow = null;
            Customer aCust = new Customer();

            foreach (DataRow dRow in dsMain.Tables[tableProd].Rows)
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
