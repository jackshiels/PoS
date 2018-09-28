using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class Employee : Person
    {
        // Inherited class. Enumeration RoleType defines the employee role.
        public enum RoleType { MarketingClerk = 0, PickingClerk = 1, StockControlClerk = 2 };

        #region Members
        private RoleType role;
        #endregion

        #region Constructors
        public Employee() : base() { }

        public Employee(string name, string address, DateTime dob, RoleType roleVal) : base()
        {
            // Creates an Employee and fills the Person superclass with values
            this.role = roleVal;
            base.Name = name;
            base.Address = address;
            base.DOB = dob;
        }
        #endregion

        #region Property Methods
        public RoleType Role
        {
            get { return role; }
            set { role = value; }
        }
        #endregion
    }
}
