using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PoS.BusDomain
{
    public class IDGen
    {
        #region Members
        private int hashVal; //int value that will be returned by the CreateID method
        private Guid hashKey; //GUID object used to generate the random string used as input for the Hash method
        #endregion
        
        #region Constructors
        public IDGen()
        {
            hashVal = 0; //setting the hashVal variable to 0
        }
        #endregion
        
        #region Methods - General
        /**
         * Method that creates a random ID and then returns it for further use
         */
        public int CreateID()
        {
            hashKey = Guid.NewGuid(); //create new GUID object to decrease chance of recurring IDs
            string x = hashKey.ToString();
            return Hash(x);
        }
         /** Method that uses the fundamentals of hash tables to generate an integer which is used as the ID
          * @param = key
          */
        public int Hash(string key)
        {
            hashVal = 0;
            int hashTableSize = 1000000000; //This defines the range of the ID's available to us (between 0 and 999 999 999 inclusive)
            char[] splitKey = key.ToCharArray();
            for (int i = 0; i < splitKey.Length; i++)
            {
                char temp = splitKey[i];
                int asciiValue = temp;
                hashVal = ((hashVal * 128) + asciiValue) % hashTableSize; //128 is the encryption value, could be set to anything
            }
            return hashVal;
        }
        #endregion
        
        #region Property Methods
        private int HashVal
        {
            get => hashVal;
            set => hashVal = value;
        }
        #endregion
    }
}
