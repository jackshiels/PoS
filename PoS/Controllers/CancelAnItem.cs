﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.DB;
using PoS.BusDomain;
using System.Windows.Forms;

namespace PoS.Controllers
{
    public class CancelAnItem
    {
        #region Members
        private ProductDB prodDb;
        private OrderDB ordDb;
        private Order anOrd;
        private Collection<string> toBeReserved;
        #endregion

        #region Constructors
        public CancelAnItem()
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            toBeReserved = new Collection<string>();
        }

        public CancelAnItem(Order updateOrd)
        {
            ordDb = new OrderDB();
            prodDb = new ProductDB();
            anOrd = updateOrd;
            toBeReserved = new Collection<string>();
        }
        #endregion

        #region Methods

        public Order findOrd(string id)
        {
            return ordDb.FindOrder(id);
        }
        public bool UpdateOrder(Order ord, Collection<OrderItem> items)
        {
            bool success = false;
            // Delete the order
            ordDb.Delete(ord);

            // Remove reservations
            foreach (OrderItem item in ord.ItemList)
            {
                //prodDb.DereserveProducts(item.ItemProduct.Name, item.Quantity);
            }

            // Retain the orderid
            string orderId = ord.OrderID;

            // Create a new order with the same orderid
            CreateAnOrder inserted = new CreateAnOrder(ord.Owner);
            inserted.AnOrd.OrderID = orderId;

            // Insert
            success = inserted.InsertIntoOrderDB(ord);
            return success;
        }
        #endregion

        #region Property Methods
        public OrderDB OrdDB
        {
            get { return ordDb; }
            set { ordDb = value; }
        }
        public ProductDB ProdDB
        {
            get { return prodDb; }
            set { prodDb = value; }
        }
        #endregion
    }
}
