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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        connectionDB opencon = new connectionDB();
        myFunction fc = new myFunction();
        public bool addrec;
        public bool check;
        private void mainForm_Load(object sender, EventArgs e)
        {
            loadGrid();
        }


        public void loadGrid() {

            try {

                opencon.dbconnect();

                if (opencon.OpenConnection()) {
                    dataGridView1.Refresh();
                    string sql = "SELECT STUDENTID,FIRSTNAME,MIDDLENAME,LASTNAME,AGE FROM student_info LIMIT 10";
                    MySqlCommand cmd = new MySqlCommand(sql, opencon.connection);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    dataGridView1.DataSource = (dt);
                    opencon.CloseConnection();
                
                
                }
            
            }


            catch (MySqlException ex){

                MessageBox.Show(ex.Message);
            }
        
        }

 

        public bool valu;
        private bool checkID(string id) {
            try {

                opencon.dbconnect();

                if (opencon.OpenConnection()) {
                    string sql = "SELECT STUDENTID FROM student_info WHERE STUDENTID = " + "'" + txtId.Text + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql,opencon.connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {

                       valu = false;

                    }
                    else {
                    valu =  true;
                    }

          
                }
                return valu;
            }
            catch (MySqlException ex) {

                MessageBox.Show(ex.Message);
                return true;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            addrec = true;
            opencon.dbconnect();

            if (opencon.OpenConnection() == true)
            {
                string id = fc.generateID();
                txtId.Text = "Student-" + id;
                opencon.CloseConnection();
            }
            else {
                txtId.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (addrec == true) {
                try
                {
                    opencon.dbconnect();
                    if (opencon.OpenConnection())
                    {
                        if (checkID(Convert.ToString(txtId.Text))== false)
                        {
                            string sql = "INSERT INTO student_info (STUDENTID , FIRSTNAME,MIDDLENAME,LASTNAME,AGE) VALUES " + "('" + Convert.ToString(txtId.Text) + "' , '" + Convert.ToString(txtFname.Text) + "' , '" + Convert.ToString(txtMname.Text) + "' , '" + Convert.ToString(txtLname.Text) + "' , '" + Convert.ToString(txtAge.Text) + "')";
                            MySqlCommand cd = new MySqlCommand(sql, opencon.connection); 
                            cd.ExecuteNonQuery();

                            MessageBox.Show("INSERTEDE NAYS");
                        }
                        else
                        {
                            MessageBox.Show("D PUMASOK");
                        }
                    }
                }

                catch (MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtId.Text = row.Cells["STUDENTID"].Value.ToString();
                txtFname.Text = row.Cells["FIRSTNAME"].Value.ToString();
                txtMname.Text = row.Cells["MIDDLENAME"].Value.ToString();
                txtLname.Text = row.Cells["LASTNAME"].Value.ToString();
                txtAge.Text = row.Cells["AGE"].Value.ToString();

            
            }
            catch (MySqlException ex) {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
