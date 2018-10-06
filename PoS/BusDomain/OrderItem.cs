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
        private string orderItemId;
        private float subTotal;
        private int quantity;
        private Product itemProduct;

        IDGen generator = new IDGen();
        #endregion

        #region Constructors
        public OrderItem()
        {
            orderItemId = "OID"+generator.CreateID();
        }

        public OrderItem(Product itemProduct, int quant)
        {
            // Creates an order item and calculates the subtotal
            subTotal = quant * itemProduct.Price;
            orderItemId = "OID"+generator.CreateID();
        }
        #endregion

        #region Methods
        public float CalculateSubTotal()
        {
            // Updates the subtotal value
            float value = (quantity * ItemProduct.Price);
            return value;
        }
        #endregion

        #region Property Methods
        public float SubTotal
        {
            // Live updates the subtotal on request
            get { return CalculateSubTotal(); }
            set { subTotal = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string OrderItemID
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }

        public Product ItemProduct
        {
            get => itemProduct;
            set => itemProduct = value;
        }

        #endregion
    }
}

