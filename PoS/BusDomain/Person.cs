using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class Person
    {
        // Superclass for customer and employee objects
        #region Members
        private string name;
        private string address;
        private DateTime dob;
        #endregion

        #region Constructors
        public Person() { }

        public Person(string nameVal, string addressVal, DateTime birthDate)
        {
            this.name = nameVal;
            this.address = addressVal;
            this.dob = birthDate;
        }
        #endregion

        #region Property Methods
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public DateTime DOB
        {
            get { return dob; }
            set { dob = value; }
        }
        #endregion
    }
}
