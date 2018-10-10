using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.BusDomain;

namespace PoS.BusDomain
{
    public class Employee : Person
    {
        // Inherited class. Enumeration RoleType defines the employee role.
        public enum RoleType { MarketingClerk = 0, PickingClerk = 1, StockControlClerk = 2 };

        #region Members
        private RoleType role;
        private string empId;
        private int hash;
        private IDGen generator;
        #endregion

        #region Constructors
        public Employee() : base()
        {
            generator = new IDGen();
            empId = "EMP" + generator.CreateID();
        }

        public Employee(string name, string address, RoleType roleVal) : base()
        {
            // Creates an Employee and fills the Person superclass with values
            this.role = roleVal;
            base.Name = name;
            base.Address = address;
            empId = "EMP" + generator.CreateID();
        }
        #endregion

        #region Methods
        public bool CompareHash(string value)
        {
            IDGen generator = new IDGen();
            bool validPass = false;

            // Check if valid
            if(hash == generator.Hash(value))
            {
                validPass = true;
            }

            return validPass;
        }
        #endregion

        #region Property Methods
        public RoleType Role
        {
            get { return role; }
            set { role = value; }
        }
        public string EmpID
        {
            get { return empId; }
            set { empId = value; }
        }
        public int Hash
        {
            get { return hash; }
            set { hash = value; }
        }
        #endregion
    }
}
