using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.BusDomain;
using System.Windows.Forms;

namespace PoS.Controllers
{
    public class LogIn
    {
        #region Members
        private EmployeeDB empDB;
        private IDGen generator;
        #endregion

        #region Constructors
        public LogIn()
        {
            empDB = new EmployeeDB();
        }
        #endregion

        #region Methods
        public bool LogInCheck(string empId, string pass)
        {
            bool success = false;

            // Check for the user
            foreach(Employee emp in empDB.EmpList)
            {
                if (empId == emp.EmpID)
                {
                    // Check the hash data
                    generator = new IDGen();
                    int hashCheck = generator.Hash(pass);

                    if (hashCheck == emp.Hash)
                    {
                        success = true;
                    }
                }
            }

            return success;
        }
        #endregion
    }
}
