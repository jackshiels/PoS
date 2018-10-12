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
using System.Globalization;

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
        private Order originalOrder;
        private Customer aCust;
        private Employee emp;
        private Collection<OrderItem> ordItems;
        private decimal updateTotal;
        private decimal createTotal;
        private Point showLocation = new Point(259, 107);
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
            string role = anEmp.Role.ToString();

            switch (role)
            {
                case ("MarketingClerk"):
                    role = "Marketing Clerk";
                    break;
                case ("PickingClerk"):
                    role = "Picking Clerk";
                    break;
                case ("StockControlClerk"):
                    role = "Stock Control Clerk";
                    break;
            }

            lblUserRole.Text = role;
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
            if (lstReportOrders.Text.Equals(""))
            {
                MessageBox.Show("Please select a valid order");
            }
            else
            {
                string orderID = lstReportOrders.Text.Split()[2].Trim();
                order = new Order();
                order = createOrder.OrdDb.FindOrder(orderID);
                hideAll();
                grpPickingList.Location = showLocation; ;
                populatePickingList(order);
            }
        }

        // go back to the picking list screen
        private void btnPickingBack_Click(object sender, EventArgs e)
        {
            hideAll();
            fillLists(); // Auxilary method
            grpPickingSelect.Location = showLocation; ;
        }
        #endregion

        #region Update Order
        //choose order to update

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                updateTotal = 0;
                lstUpdateOrderItems.Items.Clear();
                string orderID = listBox2.Text.Split()[2];
                originalOrder = new Order();

                cancel = new CancelAnItem();

                originalOrder = cancel.findOrd(orderID);
                lblUpdateName.Text = originalOrder.Owner.Name;
                cmbUpdateProducts.Text = "";
                ordItems = new Collection<OrderItem>();

                foreach (OrderItem item in originalOrder.ItemList)
                {
                    ordItems.Add(item);
                    updateTotal += Convert.ToDecimal(item.SubTotal);
                }

                foreach (OrderItem orderitem in ordItems)
                {
                    lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
                }
                
                lblUpdateTotal.Text = "Total: R" + updateTotal;
                hideAll();
                grpUpdateOrder2.Location = showLocation;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a valid order");
            }
        }

        private void btnUpdateAddToOrder_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Button Pressed");
            createOrder = new CreateAnOrder();
            int number;
            Int32.TryParse(txtUpdateQuantity.Text, out number);

            string[] txt = cmbUpdateProducts.Text.Split();
            int i;
            for (i = 0; i < txt.Length; i++)
            {
                if (txt[i].Equals("|"))
                {
                    break;
                }
            }
            string prodName = "";
            for (int j = 0; j < i; j++)
            {
                prodName += txt[j] + " ";
            }
            prodName = prodName.TrimEnd();

            if (cmbUpdateProducts.Text.Equals("") || number <= 0)
            {
                MessageBox.Show("Please input valid data");
            }
            else if (number > cancel.ProdDB.FindNumProduct(prodName))
            {
                MessageBox.Show("Unfortunately we only have " + cancel.ProdDB.FindNumProduct(prodName) + " available");
            }
            else if (!Int32.TryParse(txtUpdateQuantity.Text, out number))
            {
                MessageBox.Show("Please enter a valid quantity");
            }
            else
            {
                OrderItem item = new OrderItem(cancel.ProdDB.FindProductObject(prodName), number);
                ordItems.Add(item);
                lstUpdateOrderItems.Items.Clear();
                foreach (OrderItem orderitem in ordItems)
                {
                    lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
                }
                clearText();
                txtUpdateQuantity.Text = "";
                updateTotal += Convert.ToDecimal(item.SubTotal);
                lblUpdateTotal.Text = "Total: R" + updateTotal;
            }
        }

        private void btnUpdateRemoveButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Button Pressed");
            if (lstUpdateOrderItems.Text.Equals(""))
            {
                MessageBox.Show("Please make a valid selection");
            }
            else
            {
                string id = lstUpdateOrderItems.Text.Split()[3].TrimEnd();
                OrderItem temp = null;
                for (int i = 0; i < ordItems.Count(); i++)
                {
                    if (ordItems[i].OrderItemID.Equals(id))
                    {
                        temp = ordItems[i];
                        ordItems.RemoveAt(i);
                        break;
                    }
                }
                clearText();
                updateTotal -= Convert.ToDecimal(temp.SubTotal);
                lblUpdateTotal.Text = "Total: R" + updateTotal;
                lstUpdateOrderItems.Items.Clear();
                foreach (OrderItem orderitem in ordItems)
                {
                    lstUpdateOrderItems.Items.Add("Order Item ID: " + orderitem.OrderItemID + " Product: " + orderitem.ItemProduct.Name + " Subtotal: " + Convert.ToString(orderitem.SubTotal));
                }
            }
        }
  

        private void btnUpdateCreateOrder_Click(object sender, EventArgs e)
        {
            if (ordItems.Count == 0)
            {
                MessageBox.Show("Please enter a valid order with one or more order items");
            }
            else
            {
                bool success = cancel.UpdateOrder(originalOrder, ordItems);
                hideAll();
                grpFunction.Location = showLocation;
            }
        }
        #endregion

        #region Create an Order
        // select that customer for the order
        private void btnSelect_Click(object sender, EventArgs e)
        {
            createOrder = new CreateAnOrder();
            createTotal = 0;
            if (lstOrderCustList.Text.Equals(""))
                MessageBox.Show("Please select a valid customer");
            else
            {
                String[] text = lstOrderCustList.Text.Split();
                aCust = createOrder.CustDB.FindCustomerObject(text[text.Length - 1]);
                hideAll();
                grpOrderManagement.Location = showLocation;
                lblOrderCustName.Text = aCust.Name;
                order = new Order(aCust);
                lblCreateTotal.Text = "Total: R"+createTotal;
            }
        }

        

        private void btnOrderBack_Click(object sender, EventArgs e) //go back to select customer order is for
        {
            hideAll();
            grpNewOrderCust.Location = showLocation;
            lstOrderItems.Items.Clear();
            clearText();
        }

        private void btnOrderAddItem_Click(object sender, EventArgs e) // add item to the order
        {
            int number;
            Int32.TryParse(txtOrderQuantity.Text, out number);

            string[] txt = cmbOrderProducts.Text.Split();
            int i;
            for (i = 0; i < txt.Length; i++)
            {
                if (txt[i].Equals("|"))
                {
                    break;
                }
            }
            string prodName = "";
            for (int j = 0; j < i ; j++)
            {
                prodName += txt[j] + " ";
            }
            prodName = prodName.TrimEnd();

            if (cmbOrderProducts.Text.Equals("") || number <= 0) //invalid input error
            {
                MessageBox.Show("Please input valid data");
            }
            else if (number > createOrder.ProdDB.FindNumProduct(prodName))//createOrder.ProdDB.FindNumProduct(cmbOrderProducts.Text))
            {
                MessageBox.Show("Unfortunately we only have "+ createOrder.ProdDB.FindNumProduct(prodName)+" available");
            }
            else if (!(Int32.TryParse(txtOrderQuantity.Text,out number)))
            {
                MessageBox.Show("Please enter a valid quantity");
            }
            else
            {
                Product prod = createOrder.ProdDB.FindProductObject(prodName);
                OrderItem orderItem = new OrderItem(prod, number);
                lstOrderItems.Items.Add("Order Item ID: "+orderItem.OrderItemID+" Item Name: "+orderItem.ItemProduct.Name+" Quantity: "+orderItem.Quantity+" Sub-total: "+orderItem.SubTotal);
                Boolean success = order.AddToOrder(orderItem);
                clearText();
                createTotal += Convert.ToDecimal(orderItem.SubTotal);
                lblCreateTotal.Text = "Total: R" + createTotal;
            }
        }
        
        // add order to the orderDB
        private void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            if (order.ItemList.Count == 0)
                MessageBox.Show("Please enter a valid order with one or more order items");
            else
            {
                createOrder.InsertIntoOrderDB(order);
                MessageBox.Show("Order successfully created");
                hideAll();
                clearText();
                grpFunction.Location = showLocation;
            }
        }
         //cancel the order  and go back to home screen
        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            lstOrderItems.Items.Clear();
            hideAll();
            clearText();
            grpFunction.Location = showLocation;
        }
        // remove an item from the order
        private void btnOrderRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                string[] text = lstOrderItems.Text.Split();
                string itemID = text[3];

                itemID = itemID.Trim();
                OrderItem temp = null;
                foreach (OrderItem item in order.ItemList)
                {
                    if (itemID.Equals(item.OrderItemID))
                    {
                        temp = item;
                        break;
                    }
                }
                order.RemoveFromOrder(itemID);

                lstOrderItems.Items.RemoveAt(lstOrderItems.SelectedIndex);

                clearText();
                createTotal -= Convert.ToDecimal(temp.SubTotal);
                lblCreateTotal.Text = "Total: R" + createTotal;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a valid item");
            }
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

        // Submit and add that cutsomer to the DB
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int postalCode;
            long cardNum;
            long CVV;
            if ( (txtCustName.Text.Equals("")) || (txtCustStreet.Text.Equals("")) 
                || (txtCustSuburb.Text.Equals("")) || (txtCustPostal.Text.Equals("")) ||
                (txtCustCity.Text.Equals("")) || (txtCustProvince.Text.Equals("")) || 
                ( cmbCustPayment.Text.Equals("Credit Card") && ( txtCustCardName.Text.Equals("") 
                || txtCustCardNum.Text.Equals("") || txtCustCVV.Text.Equals("") ) ) || cmbCustPayment.Text.Equals(""))
            {
                MessageBox.Show("Invalid customer data has been entered.\nPlease re-enter any missing data and try again.");
            }
            else if ( !(Int32.TryParse(txtCustPostal.Text,out postalCode)) )
            {
                MessageBox.Show("Invalid Postal Code\nPlease try again");
            }
            else if ( cmbCustPayment.Text.Equals("Credit Card") && (txtCustCardNum.Text.Length != 16 || txtCustCVV.Text.Length != 3 ))
            {
                MessageBox.Show("Invalid Credit Card Data Length\nPlease try again");
            }
            else if ( cmbCustPayment.Text.Equals("Credit Card") && (!(Int64.TryParse(txtCustCardNum.Text, out cardNum)) || !(Int64.TryParse(txtCustCVV.Text, out CVV))) )
            {
                MessageBox.Show("Invalid Credit Card Data\nPlease try again");
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
                MessageBox.Show("Customer Saved");
                hideAll();
                grpFunction.Location = showLocation;
                clearText();
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
        public void fillLists()
        {
            createOrder = new CreateAnOrder();
            Collection<Customer> customers = createOrder.ValidCustomers();
            Collection<Product> products = createOrder.ProdDB.ProdList;
            Collection<Order> orders = createOrder.OrdDb.OrdList;
            Collection<Product> expired = createOrder.ProdDB.ExpiryList();

            lstExpiredProd.Items.Clear();
            lstOrderCustList.Items.Clear();
            lstOrderItems.Items.Clear();
            lstUpdateList.Items.Clear();
            lstReportOrders.Items.Clear();
            cmbOrderProducts.Items.Clear();
            cmbUpdateProducts.Items.Clear();
            lstPickingList.Items.Clear();
            listBox2.Items.Clear();

            foreach (Customer customer in customers)
            {
                lstOrderCustList.Items.Add("Name: "+customer.Name+" | Customer ID: "+customer.CustomerID); // Name: Garfielf CustomerID: CUS26656
            }
            
            foreach (Product product in products)
            {
                if (product.Expiry >= DateTime.Now)
                {
                    cmbOrderProducts.Items.Add(product.Name + " | (Available: " + product.Stock + ")");
                    cmbUpdateProducts.Items.Add(product.Name + " | (Available: " + product.Stock + ")");
                }
            }

            foreach (Order x in orders)
            {
                string txt = "Order ID: " + x.OrderID + " Customer: " + x.Owner.Name;
                listBox2.Items.Add(txt);

                lstReportOrders.Items.Add("Order ID: " + x.OrderID + " | Customer: " + x.Owner.Name);
            }

            foreach (Product prod in expired)
            {
                lstExpiredProd.Items.Add("Product: "+prod.Name+ " | Amount: "+prod.Stock+" | Expiry Date: "+ prod.Expiry.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)+
                    " | Location: "+prod.Location+" | Write-Off: "+ (prod.Price*prod.Stock) );
            }
        }

        public void populatePickingList(Order ord)
        {
            Collection<string> details = genPicking.GetPickingList(ord);
            foreach (string item in details)
            {
                lstPickingList.Items.Add(item);
            }
        }
       
        // make it open screens
        private void lstFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no need to fill twice. It's a big operation
            fillLists();
            clearText();
            switch (lstFunctions.Text)
            {
                case ("Create a New Customer"):
                    hideAll();
                    grpNewCustomer.Location = showLocation;
                    break;
                case ("Create a New Order"):
                    hideAll();
                    grpNewOrderCust.Location = showLocation;
                    //grpOrderSelect.Location = showLocation; lost code
                    break;
                case ("Update an Order"):
                    hideAll();
                    grpUpdate.Location = showLocation;
                    break;
                case ("Generate a Picking List"):
                    hideAll();
                    grpPickingSelect.Location = showLocation;
                    break;
                case ("Generate an Expiry Report"):
                    hideAll();
                    fillLists();
                    createRep = new CreateReport(expiredItems);
                    lblReportNum.Text = createRep.Exp.ReportID;
                    dateBox.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    grpReport.Show();
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
            grpNewOrderCust.Location = hiddenLocation;
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
                    lstFunctions.Items.Add("Generate an Expiry Report");
                    break;
            }
        }

        private void clearText()
        {
            txtCustCardName.Text = "";
            txtCustCardNum.Text = "";
            txtCustCity.Text = "";
            txtCustCVV.Text = "";
            txtCustName.Text = "";
            txtCustPostal.Text = "";
            txtCustProvince.Text = "";
            txtCustStreet.Text = "";
            txtCustSuburb.Text = "";
            txtOrderQuantity.Text = "";
            txtUpdateQuantity.Text = "";
            lblCreateTotal.Text = "";
            lblUpdateTotal.Text = "";
        }

        private void exitApp(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
