﻿using System;
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
        private Collection<int> values;
        #endregion
        
        #region Constructors
        public IDGen()
        {
            hashVal = 0; //setting the hashVal variable to 0
            values = new Collection<int>();
        }
        #endregion
        
        #region Methods
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
        private int Hash(string key)
        {
            hashVal = 0;
            int hashTableSize = 1000; //This defines the range of the ID's available to us (between 0 and 999 inclusive)
            char[] splitKey = key.ToCharArray();
            for (int i = 0; i < splitKey.Length; i++)
            {
                char temp = splitKey[i];
                int asciiValue = temp;
                hashVal = ((hashVal * 128) + asciiValue) % hashTableSize; //128 is the encryption value, could be set to anything
            }
            if (AlreadyMade(hashVal))
                hashVal = Hash(Guid.NewGuid().ToString()); // recursive call with a new encrypter string
            else
                values.Add(hashVal); //Add that value to the collection once it is confirmed that it has not been remade
            
            return hashVal;
        }

        /**
         * A method used to check if a particular ID has been made before
         */
        private Boolean AlreadyMade(int key)
        {
            Boolean made = false;
            foreach (int x in values)
            {
                if (key == x)
                    made = true;
            }
            return made;
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
