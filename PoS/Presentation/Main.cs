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
        private void btnOrderBack_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderAddItem_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderSubmit_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderRemoveItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Create a new Customer
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
            Thread.Sleep(5000);
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
        public void fillLists()
        {
            Collection<Customer> customers = customerDB.CustList;
            foreach (Customer customer in customers)
            {
                lstOrderCustList.Items.Add("Name: "+customer.Name+" Customer ID: "+customer.CustomerID);
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
