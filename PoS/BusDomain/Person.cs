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
        protected int personId;
        protected string name;
        protected string address;
        protected DateTime dob;
        #endregion

        #region Constructors
        public Person() { }

        public Person(int PersonID, string nameVal, string addressVal, DateTime birthDate)
        {
            this.personId = PersonID;
            this.name = nameVal;
            this.address = addressVal;
            this.dob = birthDate;
        }
        #endregion

        #region Property Methods
        public string PersonID
        {
            get { return name; }
            set { name = value; }
        }
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
