using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace prot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        myFunction fc = new myFunction();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (fc.checkUser(txtUser.Text.ToString(), txtPass.Text.ToString()))
            {
                MessageBox.Show("LOGIN SUCCESSFUL");
                var me = new mainForm();
                me.Show();
                this.Close();
            }
            else {
                MessageBox.Show("INVALID");
            }
        }
    }
}
