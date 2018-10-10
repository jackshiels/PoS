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
using PoS.Controllers;

namespace PoS.Presentation
{
    public partial class Main : Form
    {
        #region Members
        private CustomerDB customerDB = new CustomerDB();
        private ProductDB productDB = new ProductDB();
        private CreateAnOrder createOrder = new CreateAnOrder();
        private CreateACustomer createCust = new CreateACustomer();
        private GeneratePickingList genPicking = new GeneratePickingList();
        private CreateReport createRep;
        private CancelAnItem cancel = new CancelAnItem();
        private OrderDB orderDB = new OrderDB();
        private Order order;
        private Customer aCust;
        private Employee emp;
        private Collection<OrderItem> ordItems; //
        #endregion

        #region Constructor
        public Main()
        {
            InitializeComponent();
            PopulateFunctions();
            hideAll();
            grpFunction.Show();
        }

        public Main(Employee anEmp)
        {
            lblUserName.Text = anEmp.Name;
            lblUserRole.Text = anEmp.Role.ToString();
            InitializeComponent();
            PopulateFunctions();
            hideAll();
            grpFunction.Show();
        }
        #endregion

        #region Constant Buttons
        // Constant Logout Button top of display back to login screen
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            Hide();
        }

        // Completely exit the code
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Picking List
        //Method to select the order to print an order list for
        private void btnPickingSelect_Click(object sender, EventArgs e)
        {
            //fillLists();
            string orderID = lstOrderCustList.Text.Split()[2];

            order = null;
            order = createOrder.OrdDb.FindOrder(orderID);
            grpPickingSelect.Hide();
            grpPickingList.Show();
            populatePickingList(order);
        }

        // go back to the picking list screen
        private void btnPickingBack_Click(object sender, EventArgs e)
        {
            grpPickingList.Hide();
            fillLists(); // Auxilary method
            grpPickingSelect.Show();
        }
        #endregion

        #region Update Order
        //choose order to update
        private void btnUpdateSelect_Click(object sender, EventArgs e)
        {
            string orderID = lstOrderItems.Text.Split()[2];
            order = null;
            order = createOrder.OrdDb.FindOrder(orderID);
            lblOrderCustName.Text = order.Owner.Name;
            cmbOrderProducts.Text = "";
            ordItems = new Collection<OrderItem>(); //
            foreach (OrderItem orderitem in order.ItemList)
            {
                cmbOrderProducts.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Item Name: " + orderitem.ItemProduct.Name + " Quantity: " + orderitem.Quantity + " Sub-total: " + orderitem.SubTotal);
                ordItems.Add(orderitem);
            }
            grpUpdateOrder.Hide();
            grpOrderManagement.Show();           
        }
        #endregion

        #region Create an Order
        // select that customer for the order
        private void btnSelect_Click(object sender, EventArgs e)
        {
            String[] text = lstOrderCustList.Text.Split();
            aCust = createOrder.CustDB.FindCustomerObject(text[text.Length]);
            grpOrderSelect.Hide();
            grpOrderManagement.Show();
            lblOrderCustName.Text = aCust.Name;
            order = new Order(aCust);
            //ordItems = new Collection<OrderItem>();
        }

        private void btnOrderBack_Click(object sender, EventArgs e) //go back to select customer order is for
        {
            grpOrderSelect.Show();
            grpOrderManagement.Show();
        }

        private void btnOrderAddItem_Click(object sender, EventArgs e) // add item to the order
        {
            int number;
            Int32.TryParse(txtOrderQuantity.Text, out number);
            if (cmbOrderProducts.Text.Equals("") || txtOrderQuantity.Text.Equals("0")) //invalid input error
            {
                MessageBox.Show("Please input valid data");
            }
            else if (number > createOrder.ProdDB.FindNumProduct(cmbOrderProducts.Text))
            {
                MessageBox.Show("Unfortunately we only have "+ createOrder.ProdDB.FindNumProduct(cmbOrderProducts.Text)+" available");
            }
            else
            {
                Product prod = createOrder.ProdDB.FindNonReservedProduct(cmbOrderProducts.Text);
                OrderItem orderItem = new OrderItem(prod, number);
                ordItems.Add(orderItem); //
                cmbOrderProducts.Items.Add("Order Item ID: "+orderItem.OrderItemID+" Item Name: "+orderItem.ItemProduct.Name+" Quantity: "+orderItem.Quantity+" Sub-total: "+orderItem.SubTotal);
                Boolean success = order.AddToOrder(prod,number);
            }
        }
        
        // add order to the orderDB
        private void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            createOrder.InsertIntoOrderDB(order);
            grpOrderManagement.Hide();
            fillLists();
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
            //OrderItem x = order.FindOrderItem(item);
            //ordItems.Remove(x);
            order.RemoveFromOrder(item);
            fillLists();
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
            string[] payment = null;
            if (cmbCustPayment.Text.Equals("EFT"))
            {
                customer = new Customer(name,address);
                createCust = new CreateACustomer(name,address,payment);
            }
            else
            {
                string[] paymentDetails = new string[] { txtCustCardNum.Text, txtCustCVV.Text, txtCustCardName.Text };
                customer = new Customer(name,address,paymentDetails);
                createCust = new CreateACustomer(name, address, paymentDetails);
            }

            Boolean success = createCust.CustDB.InsertCustomer(customer);
            fillLists();
            grpNewCustomer.Hide();
            grpSuccessfulCustomer.Show();
            Thread.Sleep(5000); // let the code sleep for 5 seconds before moving onto the next line
            grpSuccessfulCustomer.Hide();
            grpFunction.Show();
        }

        //payment method
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
            Collection<Customer> customers = createOrder.ValidCustomers();
            Collection<Product> products = createOrder.ProdDB.ProdList;
            Collection<string> seen = new Collection<string>();
            Collection<Order> orders = createOrder.OrdDb.OrdList;

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
                    cmbOrderProducts.Items.Add(product.Name+" (Available: "+createOrder.ProdDB.FindNumProduct(product.Name)+")");
                    seen.Add(product.Name);
                }
            }
            foreach (Order x in orders)
            {
                lstOrderCustList.Items.Add("Order ID: "+x.OrderID +" Customer: "+x.Owner);
                lstOrderItems.Items.Add("Order ID: " + x.OrderID + " Customer: " + x.Owner);
                lstUpdateList.Items.Add("Order ID: " + x.OrderID + " Customer: " + x.Owner);
            }
            //lstUpdateList;
        }

        public void populatePickingList(Order ord)
        {
            Collection<string> list = genPicking.GetPickingList(ord);
            foreach (string x in list)
            {
                lstReportOrders.Items.Add(x);
            }
        }
       
        // make it open screens
        private void lstFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillLists();
            switch (lstFunctions.Text)
            {
                case ("Create a New Customer"):
                    hideAll();
                    fillLists();
                    grpNewCustomer.Show();
                    break;
                case ("Create a New Order"):
                    hideAll();
                    fillLists();
                    grpOrderSelect.Show();
                    break;
                case ("Update an Order"):
                    hideAll();
                    fillLists();
                    grpUpdateOrder.Show();
                    break;
                case ("Generate a Picking List"):
                    hideAll();
                    fillLists();
                    grpPickingSelect.Show();
                    break;
                case ("Generate a Stock Report"):
                    createRep = new CreateReport();
                    lblReportNum.Text = createRep.Exp.ReportID;
                    reportTable = createRep.Exp.DataGrid; //table
                    expiredItems = createRep.Exp.Chart; //chart
                    hideAll();
                    grpReport.Show();
                    break;
                default:
                    MessageBox.Show("Invalid Entry, please try again");
                    fillLists();
                    break;
            }
        }

        private void hideAll()
        {

            grpFunction.Hide();
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

        private void PopulateFunctions()
        {
            switch (emp.Role)
            {
                case (Employee.RoleType.MarketingClerk):
                    lstFunctions.Items.Add("Create a New Customer");
                    lstFunctions.Items.Add("Create a New Order");
                    lstFunctions.Items.Add("Update an Order");
                    break;
                case (Employee.RoleType.PickingClerk):
                    lstFunctions.Items.Add("Generate a Picking List");
                    break;
                case (Employee.RoleType.StockControlClerk):
                    lstFunctions.Items.Add("Generate Stock Report");
                    break;
            }
        }
        #endregion
    }
}
