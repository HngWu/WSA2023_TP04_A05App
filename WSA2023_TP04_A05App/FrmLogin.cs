using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSA2023_TP04_A05App
{
    public partial class FrmLogin : Form
    {
        WSA2023_TP04_A05Entities context = new WSA2023_TP04_A05Entities();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var username = tbusername.Text.ToString();
            var password = tbpassword.Text.ToString();
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            var validUser = context.users.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (validUser == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            else
            {
                MessageBox.Show("Login successful");
                FrmAdminPanel frmAdmin = new FrmAdminPanel();
                frmAdmin.Show();
                this.Hide();
            }


        }

        private void btnregister_Click(object sender, EventArgs e)
        {
            FrmRegistration frmRegistration = new FrmRegistration();
            frmRegistration.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
