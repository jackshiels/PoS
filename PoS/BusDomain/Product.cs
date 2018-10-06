using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class Product
    {
        #region Members
        private string prodId;
        private string name;
        private double[] dimensions;
        private double weight;
        private float price;
        private string location;
        private DateTime expiryDate;
        private int reserved;
        #endregion

        #region Constructors
        public Product() { }

        public Product(string prodIdVal, string nameVal, double[] dim, double weightVal, float priceVal, string Location, DateTime ExpiryDate, int Reserved)
        {
            this.prodId = prodIdVal;
            this.name = nameVal;
            this.dimensions = dim;
            this.weight = weightVal;
            this.price = priceVal;
            this.location = Location;
            this.expiryDate = ExpiryDate;
            this.reserved = Reserved;
        }
        #endregion

        #region Property Methods
        public string ProdID
        {
            get { return prodId; }
            set { prodId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double[] Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public DateTime Expiry
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
        public int Reserved
        {
            get { return reserved; }
            set { reserved = value; }
        }
        #endregion
    }
}
