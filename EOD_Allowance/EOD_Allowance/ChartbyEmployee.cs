using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace EOD_Allowance
{
    public partial class ChartbyEmployee : Form
    {
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\Database.accdb");

        public object ChartExample { get; private set; }

        public ChartbyEmployee()
        {
            InitializeComponent();
        }

        private void ChartbyEmployee_Load(object sender, EventArgs e)
        {

            conn.Open();
            SetMyCustomFormat();
            txtSearchMonth.Clear();

            bttSearchMonth_Click(sender, e);
            txtSearchMonth.Visible = false;          

        }

        public void SetMyCustomFormat()
        {
           

            DateTime newDateValue = new DateTime(dtpSearchMonth.Value.Year, 1, 1);
            dtpSearchMonth.Value = newDateValue;
            dtpSearchMonth.Format = DateTimePickerFormat.Custom;
            dtpSearchMonth.CustomFormat = "MMMM";
            dtpSearchMonth.ShowUpDown = true;
            dtpSearchMonth.Value = DateTime.Now;

            string month = dtpSearchMonth.Value.Month.ToString();
            string year = dtpSearchMonth.Value.Year.ToString();


        }
        private void bttSearchMonth_Click(object sender, EventArgs e)
        {
            
            chart1.Series.Clear();
            chart1.Titles.Clear();

            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 500;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 500;
            chart1.Titles.Add("ค่าเบี้ยเลี้ยง");
            chart1.Series.Add("Allowane");

            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
            chart1.Series["Allowane"].Points.Clear();
            chart1.Series["Allowane"].IsValueShownAsLabel = true;

            string sql = "";
            if (txtSearchMonth.Text == "")
            {
                sql = "SELECT [UserID],Sum(Amount) AS SumOfAmount FROM EOD_AllowanceData GROUP BY [UserID]";
            }
            else
            {
                sql = "SELECT [UserID],Sum(Amount) AS SumOfAmount FROM EOD_AllowanceData where (EOD_AllowanceData.PaymentPeriod) = '" + txtSearchMonth.Text + "'  GROUP BY EOD_AllowanceData.UserID ";
                txtSearchMonth.Clear();
            }

            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())

            {
                string varEmployeeName = "";

                string sql1 = "SELECT * from HR_Employee where EmployeeID = '" + dr["UserID"].ToString() + "' ";
                OleDbCommand com1 = new OleDbCommand(sql1, conn);
                OleDbDataReader dr1 = com1.ExecuteReader();
                while (dr1.Read())
                {
                    varEmployeeName = dr1["ThaiName"].ToString();
                }

                string varuserID = dr["UserID"].ToString();
                decimal varSumofAmount = Convert.ToDecimal(dr["SumOfAmount"].ToString());
                chart1.Series["Allowane"].Points.AddXY(varEmployeeName, varSumofAmount);
            }
            dr.Close();
            
            txtSearchMonth.Text = dtpSearchMonth.Value.Year.ToString("0000") + "-" + dtpSearchMonth.Value.Month.ToString("00") + "-" + dtpSearchMonth.Value.Day.ToString("25");

        }      
    }
}