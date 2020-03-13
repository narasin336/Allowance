namespace EOD_Allowance
{
    partial class ChartbyEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpSearchMonth = new System.Windows.Forms.DateTimePicker();
            this.txtSearchMonth = new System.Windows.Forms.TextBox();
            this.bttSearchMonth = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            this.chart1.BorderlineColor = System.Drawing.Color.Red;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 38);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.XValueMember = "X";
            series2.YValueMembers = "Y";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(885, 447);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dtpSearchMonth
            // 
            this.dtpSearchMonth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSearchMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpSearchMonth.Location = new System.Drawing.Point(59, 13);
            this.dtpSearchMonth.Name = "dtpSearchMonth";
            this.dtpSearchMonth.Size = new System.Drawing.Size(81, 20);
            this.dtpSearchMonth.TabIndex = 92;
            this.dtpSearchMonth.Value = new System.DateTime(2017, 8, 1, 0, 0, 0, 0);
            // 
            // txtSearchMonth
            // 
            this.txtSearchMonth.BackColor = System.Drawing.SystemColors.Control;
            this.txtSearchMonth.Location = new System.Drawing.Point(12, 12);
            this.txtSearchMonth.Name = "txtSearchMonth";
            this.txtSearchMonth.ReadOnly = true;
            this.txtSearchMonth.Size = new System.Drawing.Size(41, 20);
            this.txtSearchMonth.TabIndex = 91;
            // 
            // bttSearchMonth
            // 
            this.bttSearchMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bttSearchMonth.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bttSearchMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttSearchMonth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bttSearchMonth.Image = global::EOD_Allowance.Properties.Resources._1;
            this.bttSearchMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bttSearchMonth.Location = new System.Drawing.Point(146, 11);
            this.bttSearchMonth.Name = "bttSearchMonth";
            this.bttSearchMonth.Size = new System.Drawing.Size(93, 23);
            this.bttSearchMonth.TabIndex = 93;
            this.bttSearchMonth.Text = "Show";
            this.bttSearchMonth.UseVisualStyleBackColor = true;
            this.bttSearchMonth.Click += new System.EventHandler(this.bttSearchMonth_Click);
            // 
            // ChartbyEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(909, 501);
            this.Controls.Add(this.dtpSearchMonth);
            this.Controls.Add(this.bttSearchMonth);
            this.Controls.Add(this.txtSearchMonth);
            this.Controls.Add(this.chart1);
            this.Name = "ChartbyEmployee";
            this.Text = "ChartbyEmployee";
            this.Load += new System.EventHandler(this.ChartbyEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DateTimePicker dtpSearchMonth;
        private System.Windows.Forms.TextBox txtSearchMonth;
        private System.Windows.Forms.Button bttSearchMonth;
    }
}