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
        private string description;
        private double[] dimensions;
        private double weight;
        private float price;
        private DateTime expiryDate;
        private int stockLevel;
        private int reserved;
        #endregion

        #region Constructors
        public Product() { }

        public Product(string prodIdVal, string nameVal, string descr, double[] dim, double weightVal, float priceVal, DateTime ExpiryDate, int StockLevel, int Reserved)
        {
            this.prodId = prodIdVal;
            this.name = nameVal;
            this.description = descr;
            this.dimensions = dim;
            this.weight = weightVal;
            this.price = priceVal;
            this.expiryDate = ExpiryDate;
            this.stockLevel = StockLevel;
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
        public DateTime Expiry
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
        public int StockLevel
        {
            get { return stockLevel; }
            set { stockLevel = value; }
        }
        public int Reserved
        {
            get { return reserved; }
            set { reserved = value; }
        }
        #endregion
    }
}
