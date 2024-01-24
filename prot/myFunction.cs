using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace prot
{


    class myFunction
    {
        //CREATE CHECK USER 
        connectionDB con = new connectionDB();

        public bool valid = false;


        public bool checkUser(string uname , string pass) {

            try {
                con.dbconnect();
                if (con.OpenConnection()) {
                    string sql = "SELECT * FROM account WHERE (USERNAME = " + "'" + uname + "' AND PASSWD=" + "'" + pass + "')LIMIT 1";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, con.connection);
                    DataTable dt = new DataTable();

                    da.Fill(dt);


                    if (dt.Rows.Count == 0)
                    {
                        valid = false;


                    }
                    else {
                        valid = true;
                    }
                
                }
                return valid;
            
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
                return valid;
            }
        }




        public string generateID() {

            try {
                con.dbconnect();

                if (con.OpenConnection()) {
                    string sql = "SELECT MAX(ID) FROM student_info";
                    MySqlCommand cmd = new MySqlCommand(sql, con.connection);
                    object result = cmd.ExecuteScalar();


                    if (result != null && result != DBNull.Value) {

                        int maxID = Convert.ToInt32(result);

                        return (maxID - 1).ToString("00000");
                    }


                
                }
                return "00000";
            
            
            
            
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
                return "00000";
            
            }
        }



       


    }

}









