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
        private CreateAnOrder createOrder;
        private CreateACustomer createCust;
        private GeneratePickingList genPicking = new GeneratePickingList();
        private CreateReport createRep;
        private CancelAnItem cancel = new CancelAnItem();
        private OrderDB orderDB = new OrderDB();
        private Order order;
        private Customer aCust;
        private Employee emp;
        private Collection<OrderItem> ordItems;
        private Point showLocation = new Point(259,107);
        #endregion

        #region Constructor
        public Main()
        {
            InitializeComponent();
            PopulateFunctions();
            hideAll();
            //moveForward();
            grpFunction.Location = showLocation; ;
        }

        public Main(Employee anEmp)
        {
            InitializeComponent();
            lblUserName.Text = anEmp.Name;
            lblUserRole.Text = anEmp.Role.ToString();
            emp = anEmp;
            PopulateFunctions();
            //moveForward();
            hideAll();
            grpFunction.Location = showLocation;
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
            grpPickingList.Location = showLocation; ;
            populatePickingList(order);
        }

        // go back to the picking list screen
        private void btnPickingBack_Click(object sender, EventArgs e)
        {
            grpPickingList.Hide();
            fillLists(); // Auxilary method
            grpPickingSelect.Location = showLocation; ;
        }
        #endregion

        #region Update Order
        //choose order to update
        private void btnUpdateSelect_Click(object sender, EventArgs e)
        {
            string orderID = lstOrderItems.Text.Split()[2];
            order = null;
            order = cancel.OrdDB.FindOrder(orderID);
            lblUpdateName.Text = order.Owner.Name;
            cmbUpdateProducts.Text = "";
            ordItems = new Collection<OrderItem>();
            ordItems = order.ItemList;
            foreach (OrderItem orderitem in ordItems)
            {
                lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
            }
            grpUpdate.Hide();
            grpUpdateOrder2.Location = showLocation; ;
        }

        private void btnUpdateAddToOrder_Click(object sender, EventArgs e)
        {
            int number;
            Int32.TryParse(txtUpdateQuantity.Text, out number);
            if (cmbUpdateProducts.Text.Equals("") || number >= 0)
            {
                MessageBox.Show("Please input valid data");
            }
            else if (number > cancel.ProdDB.FindNumProduct(cmbUpdateProducts.Text))
            {
                MessageBox.Show("Unfortunately we only have " + createOrder.ProdDB.FindNumProduct(cmbOrderProducts.Text) + " available");
            }
            else
            {
                OrderItem item = new OrderItem(cancel.ProdDB.FindNonReservedProduct(cmbUpdateProducts.Text.Split()[0].TrimEnd()), Convert.ToInt32(txtUpdateQuantity));
                ordItems.Add(item);
                foreach (OrderItem orderitem in ordItems)
                {
                    lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
                }
            } 
        }

        private void btnUpdateRemoveButton_Click(object sender, EventArgs e)
        {
            string id = lstUpdateList.Text.Split()[3].TrimEnd();

            for (int i = 0; i < ordItems.Count(); i++)
            {
                if (ordItems[i].OrderItemID.Equals(id))
                {
                    ordItems.RemoveAt(i);
                    break;
                }
            }

            foreach (OrderItem orderitem in ordItems)
            {
                lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
            }
        }

        private void btnUpdateCreateOrder_Click(object sender, EventArgs e)
        {
            cancel.UpdateOrder(order, ordItems);
            hideAll();
            grpFunction.Location = showLocation;
            
        }
        #endregion

        #region Create an Order
        // select that customer for the order
        private void btnSelect_Click(object sender, EventArgs e)
        {
            createOrder = new CreateAnOrder();
            String[] text = lstOrderCustList.Text.Split();
            aCust = createOrder.CustDB.FindCustomerObject(text[text.Length]);
            hideAll();
            grpOrderManagement.Location = showLocation;
            lblOrderCustName.Text = aCust.Name;
            order = new Order(aCust);
        }

        private void btnOrderBack_Click(object sender, EventArgs e) //go back to select customer order is for
        {
            hideAll();
            grpOrderSelect.Location = showLocation;
        }

        private void btnOrderAddItem_Click(object sender, EventArgs e) // add item to the order
        {
            int number;
            Int32.TryParse(txtOrderQuantity.Text, out number);
            if (cmbOrderProducts.Text.Equals("") || number >= 0) //invalid input error
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
            hideAll();
            fillLists();
            grpOrderSubmitted.Location = showLocation;
            Thread.Sleep(5000);
            hideAll();
            grpFunction.Location = showLocation;
        }
         //cancel the order  and go back to home screen
        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            lstOrderItems.Text = "";
            hideAll();
            grpFunction.Show();
        }
        // remove an item from the order
        private void btnOrderRemoveItem_Click(object sender, EventArgs e)
        {
            string item = cmbOrderProducts.Text.Split()[3];
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
            if ( (txtCustName.Text.Equals("")) || (txtCustStreet.Text.Equals("")) 
                || (txtCustSuburb.Text.Equals("")) || (txtCustPostal.Text.Equals("")) ||
                (txtCustCity.Text.Equals("")) || (txtCustProvince.Text.Equals("")) || 
                ( cmbCustPayment.Text.Equals("Credit Card") && ( txtCustCardName.Text.Equals("") 
                || txtCustCardNum.Text.Equals("") || txtCustCVV.Text.Equals("") ) ) )
            {
                MessageBox.Show("Invalid customer data has been entered.\nPlease re enter any missing data and try again.");
            }
            else
            {
                string name = txtCustName.Text;
                string address = txtCustStreet.Text + " " + txtCustSuburb.Text + " " + txtCustPostal.Text + " " +
                    txtCustCity.Text + " " + txtCustProvince.Text;
                Customer customer;
                createCust = new CreateACustomer();

                string[] payment = null;

                if (cmbCustPayment.Text.Equals("EFT"))
                {
                    customer = new Customer(name, address, payment);
                }
                else
                {
                    payment = new string[] { txtCustCardNum.Text, txtCustCVV.Text, txtCustCardName.Text };
                    customer = new Customer(name, address, payment);
                }

                bool success = createCust.SubmitCustomer(customer.Name, customer.Address, customer.CardHolderDetails);

                fillLists();
                hideAll();
                grpSuccessfulCustomer.Location = showLocation;
                Thread.Sleep(5000); // let the code sleep for 5 seconds before moving onto the next line
                hideAll();
                grpFunction.Location = showLocation;
            }           
        }

        //payment method
        private void cmbCustPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustPayment.Text.Equals("EFT"))
            {
                txtCustCardName.Enabled = false;
                txtCustCardNum.Enabled = false;
                txtCustCVV.Enabled = false;
                txtCustCardName.Text = "";
                txtCustCardNum.Text = "";
                txtCustCVV.Text = "";
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
            createOrder = new CreateAnOrder();
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
                    cmbUpdateProducts.Items.Add(product.Name + " (Available: " + createOrder.ProdDB.FindNumProduct(product.Name) + ")");
                    seen.Add(product.Name);
                }
            }
            foreach (Order x in orders)
            {
                lstOrderCustList.Items.Add("Order ID: "+x.OrderID +" Customer: "+x.Owner.Name);
                lstOrderItems.Items.Add("Order ID: " + x.OrderID + " Customer: " + x.Owner.Name);
                lstUpdateList.Items.Add("Order ID: " + x.OrderID + " Customer: " + x.Owner.Name);
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
            // no need to fill twice. It's a big operation
            fillLists();
            switch (lstFunctions.Text)
            {
                case ("Create a New Customer"):
                    hideAll();
                    grpNewCustomer.Location = showLocation;
                    break;
                case ("Create a New Order"):
                    hideAll();
                    grpOrderSelect.Location = showLocation;
                    break;
                case ("Update an Order"):
                    hideAll();
                    grpUpdate.BringToFront();
                    grpUpdate.Location = showLocation;
                    break;
                case ("Generate a Picking List"):
                    hideAll();
                    grpPickingSelect.Location = showLocation;
                    break;
                case ("Generate Stock Report"):
                    createRep = new CreateReport();
                    lblReportNum.Text = createRep.Exp.ReportID;
                    reportTable = createRep.Exp.DataGrid; //table
                    expiredItems = createRep.Exp.Chart; //chart
                    hideAll();
                    grpReport.Location = showLocation;
                    break;
                default:
                    MessageBox.Show("Invalid Entry, please try again");
                    break;
            }
        }

        private void hideAll()
        {
            Point hiddenLocation = new Point(257,524);

            grpFunction.Location = hiddenLocation;

            grpSuccessfulCustomer.Location = hiddenLocation;
            grpOrderSubmitted.Location = hiddenLocation;

            grpUpdateOrder.Location = hiddenLocation; //ghost in the shell
            grpUpdate.Location = hiddenLocation;
            grpUpdateOrder2.Location = hiddenLocation;

            grpNewCustomer.Location = hiddenLocation;

            grpOrderSelect.Location = hiddenLocation;
            grpOrderManagement.Location = hiddenLocation;

            grpReport.Location = hiddenLocation;

            grpPickingList.Location = hiddenLocation;
            grpPickingSelect.Location = hiddenLocation;
        }

        private void moveForward()
        {
            grpFunction.BringToFront();

            grpSuccessfulCustomer.BringToFront();
            grpOrderSubmitted.BringToFront();

            grpUpdateOrder.BringToFront(); //ghost in the shell
            grpUpdate.BringToFront();
            grpUpdateOrder2.BringToFront();

            grpNewCustomer.BringToFront();

            grpOrderSelect.BringToFront();
            grpOrderManagement.BringToFront();

            grpReport.BringToFront();

            grpPickingList.BringToFront();
            grpPickingSelect.BringToFront();
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

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
