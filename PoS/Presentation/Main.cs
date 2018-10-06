using PoS.BusDomain;
using PoS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections.ObjectModel;

namespace PoS.Presentation
{
    public partial class Main : Form
    {
        private CustomerDB customerDB = new CustomerDB();
        private ProductDB productDB = new ProductDB();
        private OrderDB orderDB = new OrderDB();
        private Order order;
        private Customer aCust;

        public Main()
        {
            InitializeComponent();
            grpFunction.Show();

            grpUpdateOrder.Hide();            
            
            grpOrderManagement.Hide();

            grpOrderSelect.Hide();
            grpOrderSubmitted.Hide();

            grpReport.Hide();

            grpNewCustomer.Hide();
            grpSuccessfulCustomer.Hide();

            grpPickingList.Hide();
            grpPickingSelect.Hide();
        }

        #region Constant Buttons
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Picking List
        private void btnPickingSelect_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Update Order
        private void btnUpdateSelect_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Create an Order
        private void btnOrderBack_Click(object sender, EventArgs e) //go backto select customer order is for
        {
            grpOrderSelect.Show();
            grpOrderManagement.Show();
        }

        private void btnOrderAddItem_Click(object sender, EventArgs e) // add item to the order
        {
            if (cmbOrderProducts.Text.Equals("") || txtOrderQuantity.Text.Equals("0")) //invalid input error
            {
                MessageBox.Show("Please input valid data");
            }
            else
            {
                int number;
                Int32.TryParse(txtOrderQuantity.Text, out number);
                Product prod = productDB.FindNonResrvedProduct(cmbOrderProducts.Text);
                OrderItem orderItem = new OrderItem(prod, number);
                cmbOrderProducts.Items.Add("Order Item ID: "+orderItem.OrderItemID+" Item Name: "+orderItem.ItemProduct.Name+" Quantity: "+orderItem.Quantity+" Sub-total: "+order.SubTotal);
                Boolean success = order.AddToOrder(prod,number);
            }
        }
        
        // add order to the orderDB
        private void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            orderDB.InsertOrder(order);
            grpOrderManagement.Hide();
            grpOrderSubmitted.Show();
            Thread.Sleep(5000);
            grpOrderSubmitted.Hide();
            grpFunction.Show();
        }
         //cancel the order  and go back to home screen
        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            lstOrderItems.Text = "";
            grpOrderManagement.Hide();
            grpFunction.Show();
        }
        // remove an item from the order
        private void btnOrderRemoveItem_Click(object sender, EventArgs e)
        {
            string item = cmbOrderProducts.Text.Split()[3];
            order.RemoveFromOrder(item);
            fillLists();
        }
        // select that customer for the order
        private void btnSelect_Click(object sender, EventArgs e)
        {
            String[] text = lstOrderCustList.Text.Split();
            aCust = customerDB.FindCustomerObject(text[text.Length]);
            grpOrderSelect.Hide();
            grpOrderManagement.Show();
            lblOrderCustName.Text = aCust.Name;
            order = new Order(aCust);
        }

        #endregion

        #region Create a new Customer
        // Clear all the text Boxes
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustCardName.Text = "";
            txtCustCardNum.Text = "";
            txtCustCity.Text = "";
            txtCustName.Text = "";
            txtCustPostal.Text = "";
            txtCustStreet.Text = "";
            txtCustSuburb.Text = "";
            txtCustProvince.Text = "";
            txtCustCVV.Text = "";
        }

        // Submit and add that cutsomer to the DB
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String name = txtCustName.Text;
            String address = txtCustStreet.Text + " " + txtCustSuburb.Text + " " + txtCustPostal.Text + " " + 
                txtCustCity.Text + " " + txtCustProvince.Text;
            Customer customer;

            if (cmbCustPayment.Text.Equals("EFT"))
            {
                customer = new Customer(name,address);
            }
            else
            {
                string paymentDetails = txtCustCardNum.Text+txtCustCVV+txtCustCardName;
                customer = new Customer(name,address,paymentDetails);
            }

            Boolean success = customerDB.InsertCustomer(customer);
            grpNewCustomer.Hide();
            grpSuccessfulCustomer.Show();
            Thread.Sleep(5000); // let the code sleep for 5 seconds before moving onto the next line
            grpSuccessfulCustomer.Hide();
            grpFunction.Show();
        }

        private void cmbCustPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustPayment.Text.Equals("EFT"))
            {
                txtCustCardName.Enabled = false;
                txtCustCardNum.Enabled = false;
                txtCustCVV.Enabled = false;
            }
            else
            {
                txtCustCardName.Enabled = true;
                txtCustCardNum.Enabled = true;
                txtCustCVV.Enabled = true;
            }
        }
        #endregion

        #region Auxilary
        /**
         * Populate some of the list and combo box objects 
         */
        public void fillLists()
        {
            Collection<Customer> customers = customerDB.CustList;
            Collection<Product> products = productDB.ProdList;
            Collection<String> seen = new Collection<string>();
            foreach (Customer customer in customers)
            {
                lstOrderCustList.Items.Add("Name: "+customer.Name+" Customer ID: "+customer.CustomerID); // Name: Garfielf CustomerID: CUS26656
            }
            
            foreach (Product product in products)
            {
                if (seen.Contains(product.Name))
                    continue;
                else
                {
                    cmbOrderProducts.Items.Add(product.Name);
                    seen.Add(product.Name);
                }
            }
            //lstOrderItems;
            //lstUpdateList;
        }
       

        private void lstFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillLists();
        }
        #endregion
    }
}
