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
    public partial class Allowance : Form
    {
        int valErr = 0;

        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\Database.accdb");

        public Allowance()
        {
            InitializeComponent();
        }

        private void Allowance_Load(object sender, EventArgs e)
        {
           
            conn.Open();
            txtUserID2.Text = MyGlobal.GlobalUserID;
            txtUser2.Text = MyGlobal.GlobalUserName;

            if (MyGlobal.GlobalAuthority == "Admin")
            {
                tabControl2.Visible = false;
                tabControl1.Visible = true;
            }
            else
            {
                tabControl1.Visible = false;
                tabControl2.Visible = true;
            }

            
            Showdata();
            ClearData();
        }

        private void ClearData()
        {
            if (MyGlobal.GlobalAuthority == "Admin")
            {
                txtStrDate.Text = "";
                txtEndDate.Text = "";
                txtAmount.Text = "";
                txtDeliveryDate.Text = "";
                txtPaymentPeriod.Text = "";
                txtSEQ.Text = "";
                txtUserID.Text = "";
                txtUser.Text = "";

                bttChart.Enabled = true;

                bttAdd.Enabled = true;
                bttDelete.Enabled = false;
                bttUpdate.Enabled = false;
                bttUser.Enabled = true;
            }
            else
            {             
                txtSEQ.Text = "";
                bttChart.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
        }

        private void Showdata()
        {
            int varindex = 0;

            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 8;
         

            dataGridView1.Columns[varindex].Name = "วันที่นำส่ง";
            dataGridView1.Columns[varindex].Width = 130;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ผู้เบิก";
            dataGridView1.Columns[varindex].Visible = false;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ชื่อผู้เบิก";
            dataGridView1.Columns[varindex].Width = 170;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "จำนวนเงิน";
            dataGridView1.Columns[varindex].Width = 110;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ระหว่างวันที่";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ถึงวันที่";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "รอบเงินเข้า";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "SEQ";
            dataGridView1.Columns[varindex].Width = 45;

            string sql = "";
            if (MyGlobal.GlobalAuthority == "Admin")
            {
                if (txtSearchUserID.Text == "")
                {
                    sql = "Select * from EOD_AllowanceData order by SEQ";
                    txtUser.Text = "";
                    txtUserID.Text = "";
                }
                else if (txtSearchUserID.Text != "") { sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + txtSearchUserID.Text + "' + '%'  order by SEQ"; } 
                             
            }
            
            else
            {
                if (txtSraStartDate.Text == "" && txtSearchEndDate.Text == "")
                {
                    sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + MyGlobal.GlobalUserID + "' + '%' order by SEQ";
                }

              else  if (txtSraStartDate.Text != "" && txtSearchEndDate.Text != "") { sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + MyGlobal.GlobalUserID + "' + '%' and StartDate >= '" + txtSraStartDate.Text + "' and EndDate <= '" + txtSearchEndDate.Text + "' order by SEQ"; }

            }

            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                string varUserName = "";

                string sql1 = "Select * from UserMaster where UserID = '" + dr["UserID"].ToString() + "' ";

                OleDbCommand com1 = new OleDbCommand(sql1, conn);
                OleDbDataReader dr1 = com1.ExecuteReader();
                while (dr1.Read())
                {
                    varUserName = dr1["UserName"].ToString();
                    txtUser2.Text = dr1["UserName"].ToString();
                    
                }
                dr1.Close();              

                dataGridView1.Rows.Add(dr["DeliveryDate"].ToString(), dr["UserID"].ToString(), varUserName, dr["Amount"].ToString(), dr["StartDate"].ToString(), dr["EndDate"].ToString(), dr["PaymentPeriod"].ToString(), dr["SEQ"].ToString());
            }
                foreach (DataGridViewColumn column in dataGridView1.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable;}

            dr.Close();

        }

        private void bttUser_Click(object sender, EventArgs e)
        {
            SearchUserName WinD3 = new SearchUserName();
            WinD3.ShowDialog();
            txtUserID.Text = MyGlobal.GlobalUserID;
            txtUser.Text = MyGlobal.GlobalUserName;
        }

        private void dtpStarDate_CloseUp(object sender, EventArgs e)
        {
            txtStrDate.Text = dtpStarDate.Value.Year.ToString("0000") + "-" + dtpStarDate.Value.Month.ToString("00") + "-" + dtpStarDate.Value.Day.ToString("00");
        }

        private void dtpEndDate_CloseUp(object sender, EventArgs e)
        {
            txtEndDate.Text = dtpEndDate.Value.Year.ToString("0000") + "-" + dtpEndDate.Value.Month.ToString("00") + "-" + dtpEndDate.Value.Day.ToString("00");
        }

        private void dtpDeliveryDate_CloseUp(object sender, EventArgs e)
        {
            txtDeliveryDate.Text = dtpDeliveryDate.Value.Year.ToString("0000") + "-" + dtpDeliveryDate.Value.Month.ToString("00") + "-" + dtpDeliveryDate.Value.Day.ToString("00");
        }

        private void dtpPaymentPeriod_CloseUp(object sender, EventArgs e)
        {
            txtPaymentPeriod.Text = dtpPaymentPeriod.Value.Year.ToString("0000") + "-" + dtpPaymentPeriod.Value.Month.ToString("00") + "-" + dtpPaymentPeriod.Value.Day.ToString("00");
        }

        private void bttClearStrDate_Click(object sender, EventArgs e)
        {
            txtStrDate.Text = "";
        }

        private void bttClearEndDate_Click(object sender, EventArgs e)
        {
            txtEndDate.Text = "";
        }

        private void bttDeliveryDate_Click(object sender, EventArgs e)
        {
            txtDeliveryDate.Text = "";
        }

        private void bttPaymentPeriod_Click(object sender, EventArgs e)
        {
            txtPaymentPeriod.Text = "";
        }

        private void bttClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            CheckError();
            if (valErr == 0)
            {
                String sql = "INSERT INTO EOD_AllowanceData (DeliveryDate,UserID,Amount,StartDate,EndDate,PaymentPeriod)VALUES ('" + txtDeliveryDate.Text + "','" + txtUserID.Text + "','" + txtAmount.Text + "','" + txtStrDate.Text + "','" + txtEndDate.Text + "','" + txtPaymentPeriod.Text + "')";

                OleDbCommand com = new OleDbCommand(sql, conn);
                com.ExecuteNonQuery();
                MessageBox.Show("Add completed");

                Showdata();
                ClearData();
            }
        }

        private void CheckError()
        {
            valErr = 0;
            if (txtUserID.Text.Trim() == "")
            {
                MessageBox.Show("กรุณาเพิ่มชื่อผู้เบิก", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return;
            }

            else if (txtAmount.Text.Trim() == "") { MessageBox.Show("กรุณากรอกข้อมูลในช่อง จำนวนเงิน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return; }
            else if (txtStrDate.Text.Trim() == "") { MessageBox.Show("กรุณากรอกข้อมูลในช่อง ระว่างวันที่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return; }
            else if (txtEndDate.Text.Trim() == "") { MessageBox.Show("กรุณากรอกข้อมูลในช่อง ถึงวันที่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return; }
            else if (txtDeliveryDate.Text.Trim() == "") { MessageBox.Show("กรุณากรอกข้อมูลในช่อง วันที่นำส่ง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return; }
            else if (txtPaymentPeriod.Text.Trim() == "") { MessageBox.Show("กรุณากรอกข้อมูลในช่อง รอบเงินเข้า", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); valErr = +1; return; }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {             
                    txtUserID.Text = dataGridView1.Rows[e.RowIndex].Cells["ผู้เบิก"].Value.ToString();
                    txtUser.Text = dataGridView1.Rows[e.RowIndex].Cells["ชื่อผู้เบิก"].Value.ToString();
                    txtSEQ.Text = dataGridView1.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
                    txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells["จำนวนเงิน"].Value.ToString();
                    txtDeliveryDate.Text = dataGridView1.Rows[e.RowIndex].Cells["วันที่นำส่ง"].Value.ToString();
                    txtStrDate.Text = dataGridView1.Rows[e.RowIndex].Cells["ระหว่างวันที่"].Value.ToString();
                    txtEndDate.Text = dataGridView1.Rows[e.RowIndex].Cells["ถึงวันที่"].Value.ToString();
                    txtPaymentPeriod.Text = dataGridView1.Rows[e.RowIndex].Cells["รอบเงินเข้า"].Value.ToString();

                    bttAdd.Enabled = false;
                    bttDelete.Enabled = true;
                    bttUpdate.Enabled = true;
                    bttUser.Enabled = false;
            }
            catch { }                    
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {            
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Showdata();
        }

        
        private void bttChart_Click(object sender, EventArgs e)
        {
            ChartbyEmployee WinD3 = new ChartbyEmployee();
            WinD3.ShowDialog();
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            txtSraStartDate.Text = dateTimePicker2.Value.Year.ToString("0000") + "-" + dateTimePicker2.Value.Month.ToString("00") + "-" + dateTimePicker2.Value.Day.ToString("00");
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            txtSearchEndDate.Text = dateTimePicker3.Value.Year.ToString("0000") + "-" + dateTimePicker3.Value.Month.ToString("00") + "-" + dateTimePicker3.Value.Day.ToString("00");
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            txtSearchPaymentPeriod.Text = dateTimePicker1.Value.Year.ToString("0000") + "-" + dateTimePicker1.Value.Month.ToString("00") + "-" + dateTimePicker1.Value.Day.ToString("00");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtSraStartDate.Enabled = true;
            txtSearchEndDate.Enabled = true;
            dateTimePicker2.Enabled = true;
            dateTimePicker3.Enabled = true;


            txtSearchPaymentPeriod.Enabled = false;
            dateTimePicker1.Enabled = false;
            txtSearchPaymentPeriod.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchPaymentPeriod.Enabled = true;
            dateTimePicker1.Enabled = true;

            txtSraStartDate.Enabled = false;
            txtSearchEndDate.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            txtSraStartDate.Clear();
            txtSearchEndDate.Clear();
        }

        private void bttSearch_Click(object sender, EventArgs e)
        {
            Showdata();
        }

        private void bttSearchUser_Click(object sender, EventArgs e)
        {
            SearchUser WinD3 = new SearchUser();
            WinD3.ShowDialog();
            txtSearchUserID.Text = MyGlobal.GlobalSearchUserID;
            txtSearchUserName.Text = MyGlobal.GlobalSearchUserName;
            Showdata();
        }

        private void bttSearchUserName_Click(object sender, EventArgs e)
        {
            Showdata();
        }

        private void txtSearchUserID_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchUserID.Text != "")
            {
                Showdata();
            }
            else { txtSearchUserName.Text = ""; Showdata(); }
        }

        private void bttUpdate_Click(object sender, EventArgs e)
        {
            CheckError();
            if (valErr != 0) { return; }
            if (txtSEQ.Text.Trim() == "")
            {
                MessageBox.Show("Please Select Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); return;
            }

            if (MessageBox.Show("Do you want to Update the data ? " + txtSEQ.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                String sql = "Update EOD_AllowanceData SET Amount = '" + txtAmount.Text + "', StartDate = '" + txtStrDate.Text + "', EndDate = '" + txtEndDate.Text + "' , DeliveryDate = '" + txtDeliveryDate.Text + "' , PaymentPeriod = '" + txtPaymentPeriod.Text + "' where SEQ = '" + txtSEQ.Text + "'";
                OleDbCommand com = new OleDbCommand(sql, conn);
                com.ExecuteNonQuery();

                MessageBox.Show("Update completed");
                Showdata();
                ClearData();
            }
        }

        private void txtSearchPaymentPeriod_TextChanged(object sender, EventArgs e)
        {
            int varindex = 0;

            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 8;


            dataGridView1.Columns[varindex].Name = "วันที่นำส่ง";
            dataGridView1.Columns[varindex].Width = 130;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ผู้เบิก";
            dataGridView1.Columns[varindex].Visible = false;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ชื่อผู้เบิก";
            dataGridView1.Columns[varindex].Width = 170;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "จำนวนเงิน";
            dataGridView1.Columns[varindex].Width = 110;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ระหว่างวันที่";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "ถึงวันที่";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "รอบเงินเข้า";
            dataGridView1.Columns[varindex].Width = 120;

            varindex = varindex + 1;
            dataGridView1.Columns[varindex].Name = "SEQ";
            dataGridView1.Columns[varindex].Width = 45;

            string sql = "";
            if (MyGlobal.GlobalAuthority == "Admin")
            {
                if (txtSearchUserID.Text == "")
                {
                    sql = "Select * from EOD_AllowanceData order by SEQ";
                    txtUser.Text = "";
                    txtUserID.Text = "";
                }
                else if (txtSearchUserID.Text != "") { sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + txtSearchUserID.Text + "' + '%'  order by SEQ"; }

            }

            else
            {
               if (txtSearchPaymentPeriod.Text == "")
                {
                    sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + MyGlobal.GlobalUserID + "' + '%' order by SEQ";
                }

               else if (txtSearchPaymentPeriod.Text != "") { sql = "Select * from EOD_AllowanceData where UserID like '%' + '" + MyGlobal.GlobalUserID + "' + '%' and PaymentPeriod like '%' + '" + txtSearchPaymentPeriod.Text + "' + '%' order by SEQ"; }
              
            }

            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                string varUserName = "";

                string sql1 = "Select * from UserMaster where UserID = '" + dr["UserID"].ToString() + "' ";

                OleDbCommand com1 = new OleDbCommand(sql1, conn);
                OleDbDataReader dr1 = com1.ExecuteReader();
                while (dr1.Read())
                {
                    varUserName = dr1["UserName"].ToString();
                    txtUser2.Text = dr1["UserName"].ToString();

                }
                dr1.Close();

                dataGridView1.Rows.Add(dr["DeliveryDate"].ToString(), dr["UserID"].ToString(), varUserName, dr["Amount"].ToString(), dr["StartDate"].ToString(), dr["EndDate"].ToString(), dr["PaymentPeriod"].ToString(), dr["SEQ"].ToString());
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }

            dr.Close();
        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            if (txtSEQ.Text.Trim() == "")
            { MessageBox.Show("Please Select Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); return; }

            if (MessageBox.Show("Do you want to delete the data ? " + txtSEQ.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                String sql = "Delete from EOD_AllowanceData where SEQ ='" + txtSEQ.Text + "' ";
                OleDbCommand com = new OleDbCommand(sql, conn);
                com.ExecuteNonQuery();

                MessageBox.Show("Delete completed");
                Showdata();
                ClearData();
            }
        }
    }
}
