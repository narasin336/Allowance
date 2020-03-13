using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace EOD_Allowance
{
    public partial class login : Form
    {
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\Database.accdb");

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        private void CheckError()
        {
            MyGlobal.GlobalUserID = "";
            MyGlobal.GlobalUserName = "";

            string sql = " Select * From UserMaster where USERID='" + txtUserID.Text + "' and Password='" + txtPassword.Text + "' ";
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            int checkCount = 0;

            while (dr.Read())
            {
                MyGlobal.GlobalUserID = dr[0].ToString();
                MyGlobal.GlobalUserName = dr[1].ToString();              
                checkCount = +1;
            }
            dr.Close();

            if (checkCount == 0)
            {
                MessageBox.Show("Invalid User and Password");
            }
            else
            {
                this.Hide();
                Allowance SApplication = new Allowance();
                SApplication.ShowDialog();
                Application.Exit();
            }
        }

        private void bttLogin_Click(object sender, EventArgs e)
        {
            CheckError();
        }
    }
}
