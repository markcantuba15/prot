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
using System.IO;
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
        public bool editrec;
        private void mainForm_Load(object sender, EventArgs e)
        {
            loadGrid();
        }


        public void loadGrid() {

            try {

                opencon.dbconnect();
                if (opencon.OpenConnection()) {
                    string sql = "SELECT STUDENTID,FIRSTNAME,LASTNAME,MIDDLENAME,AGE,PATH FROM student_info LIMIT 20";

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

            try
            {
                opencon.dbconnect();
                if (opencon.OpenConnection())
                {
                    if (addrec)
                    {
                        if (!checkID(Convert.ToString(txtId.Text)))
                        {
                            string sql = "INSERT into student_info (STUDENTID,FIRSTNAME,MIDDLENAME,LASTNAME,AGE,PATH) VALUES ( @StudentId,@FirstName,@MiddleName,@LastName,@Age,@path)";

                            using (MySqlCommand cmd = new MySqlCommand(sql, opencon.connection)) {

                                cmd.Parameters.AddWithValue("@StudentId", Convert.ToString(txtId.Text));
                                cmd.Parameters.AddWithValue("@FirstName", Convert.ToString(txtFname.Text));
                                cmd.Parameters.AddWithValue("@Middlename", Convert.ToString(txtMname.Text));
                                cmd.Parameters.AddWithValue("@Lastname", Convert.ToString(txtLname.Text));
                                cmd.Parameters.AddWithValue("@Age", Convert.ToString(txtAge.Text));
                                cmd.Parameters.AddWithValue("@path", Convert.ToString(txtpath.Text));
                                cmd.ExecuteNonQuery();
                                loadGrid();
                                MessageBox.Show("SUCCESSFULLY ADDEDD");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Duplicate id");
                        }
                    }
                    else if (editrec)
                    {
                        //   string sql = "UPDATE student_info SET FIRSTNAME = @FirstName, MIDDLENAME = @MiddleName, LASTNAME = @LastName, AGE = @Age WHERE STUDENTID = @StudentId";
                        string sql = "UPDATE student_info SET FIRSTNAME = @fname,MIDDLENAME = @mname , LASTNAME = @lname , AGE = @age WHERE STUDENTID = @studentId";

                        using (MySqlCommand cmd = new MySqlCommand(sql, opencon.connection)) {
                            cmd.Parameters.AddWithValue("@studentId", Convert.ToString(txtId.Text));
                            cmd.Parameters.AddWithValue("@fname", Convert.ToString(txtFname.Text));
                            cmd.Parameters.AddWithValue("@mname", Convert.ToString(txtMname.Text));
                            cmd.Parameters.AddWithValue("@lname", Convert.ToString(txtLname.Text));
                            cmd.Parameters.AddWithValue("@age", Convert.ToString(txtAge.Text));

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("SUCCESSFULLY updated");
                            loadGrid();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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
                string path = row.Cells["PATH"].Value.ToString();

                path = path.Replace(" ", "/");


                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {

                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {

                        idPicture.Image = Image.FromStream(stream);
                        txtpath.Text = path;



                    }

                }

                else {

                    idPicture.Image = null;
                }




            }
            catch (MySqlException ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editrec = true;
        }
        // jpg,jpeg,gif,bmp,png

    
     

        private void btnUpload_Click(object sender, EventArgs e)
        {

           
            
            OpenFileDialog photo = new  OpenFileDialog();

            photo.InitialDirectory = "C:\\kabisado\\photo";
            photo.Filter = "Image Files : (.jpg; *.jpeg; *.bmp; *.gif; *.png)|.jpg; *.jpeg; *.bmp; *.gif; *.png";


            if (photo.ShowDialog() == DialogResult.OK) {

                try {
                    string filename = System.IO.Path.GetFileName(photo.FileName);
                    string location = "C:\\kabisado\\photo";
                    string fullpath = location + "\\" + filename;

                    idPicture.ImageLocation = fullpath;
                    fullpath = fullpath.Replace("\\", "\\");
                    txtpath.Text = fullpath;




                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                
                }
            
            }




        }
    }
}
