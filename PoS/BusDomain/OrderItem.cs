using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.BusDomain
{
    public class OrderItem
    {
        #region Members
        private int orderItemId;
        private float subTotal;
        private int quantity;
        private Product itemProduct;
        #endregion

        #region Constructors
        public OrderItem() { }

        public OrderItem(Product itemProduct, int quant)
        {
            // Creates an order item and calculates the subtotal
            subTotal = quant * itemProduct.Price;
        }
        #endregion

        #region Methods
        public float CalculateSubTotal()
        {
            // Updates the subtotal value
            float value = (quantity * itemProduct.Price);
            return value;
        }
        #endregion

        #region Property Methods
        public float SubTotal
        {
            // Live updates the subtotal on request
            get { return CalculateSubTotal(); }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int OrderItemID
        {
            get { return orderItemId; }
        }
        #endregion
    }
}