using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoS.BusDomain;
using PoS.Controllers;
using PoS.Presentation;

namespace PoS
{
    public partial class LogIn : Form
    {
        private LogInController login = new LogInController();

        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // only if password matches user name
            if (login.LogInCheck(txtLoginEmpId.Text, txtLoginPass.Text))
            {
                Employee anEmp = login.EmpDB.findEmp(txtLoginEmpId.Text);
                Main main = new Main(anEmp);
                main.Show();
                Hide();
            }
            else
                MessageBox.Show("Invalid Login Credentials");
            
        }
    }
}
