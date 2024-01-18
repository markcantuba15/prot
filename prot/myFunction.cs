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

        public bool valid;

        public bool checkUser(string uname, string pass) {

            con.dbconnect();

            if (con.OpenConnection() == true) {
                string sql = "SELECT * FROM account WHERE (USERNAME = '" + uname + "' AND PASSWD = '" + pass + "') LIMIT 1";

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

            con.CloseConnection();
            return valid;
        
        }
    }
}
