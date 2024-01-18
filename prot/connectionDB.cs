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
    class connectionDB
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;



    public void dbconnect() {

            server = "localhost";
            database = "trymunadb";
            uid = "root";
            password = "";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE = " + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        
        }

        public bool OpenConnection() {
            try {
                connection.Open();
                return true;
            }
            catch (MySqlException ex) {

                switch (ex.Number) {
                    case 0:
                        MessageBox.Show("CANNOT CONNECT TO SERVER");
                        break;
                    case 1045:
                        MessageBox.Show("INVALID USERNAME/PASSWORD");
                        break;
                
                }
                return false;
            }
        
        }


        public bool CloseConnection() {
            try {
                connection.Close();
                return true;
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
     
    }

}

