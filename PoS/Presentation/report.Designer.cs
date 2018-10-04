namespace PoS.Presentation
{
    partial class report
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
            System.Windows.Forms.Label titleBox;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.listBack = new System.Windows.Forms.Label();
            this.chartBack = new System.Windows.Forms.Label();
            this.dateBox = new System.Windows.Forms.Label();
            this.expiredItems = new System.Windows.Forms.DataVisualization.Charting.Chart();
            titleBox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.expiredItems)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBox
            // 
            titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            titleBox.AutoSize = true;
            titleBox.BackColor = System.Drawing.Color.Blue;
            titleBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            titleBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            titleBox.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            titleBox.ForeColor = System.Drawing.Color.White;
            titleBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            titleBox.Location = new System.Drawing.Point(15, 14);
            titleBox.Margin = new System.Windows.Forms.Padding(5);
            titleBox.Name = "titleBox";
            titleBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            titleBox.Size = new System.Drawing.Size(771, 66);
            titleBox.TabIndex = 0;
            titleBox.Text = "Expired and Expiring Items Report";
            titleBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleBox.UseCompatibleTextRendering = true;
            titleBox.Click += new System.EventHandler(this.titleBox_Click);
            // 
            // listBack
            // 
            this.listBack.BackColor = System.Drawing.Color.Blue;
            this.listBack.Location = new System.Drawing.Point(12, 130);
            this.listBack.Name = "listBack";
            this.listBack.Size = new System.Drawing.Size(378, 311);
            this.listBack.TabIndex = 3;
            // 
            // chartBack
            // 
            this.chartBack.BackColor = System.Drawing.Color.Blue;
            this.chartBack.Location = new System.Drawing.Point(412, 130);
            this.chartBack.Name = "chartBack";
            this.chartBack.Size = new System.Drawing.Size(374, 311);
            this.chartBack.TabIndex = 4;
            // 
            // dateBox
            // 
            this.dateBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateBox.BackColor = System.Drawing.Color.Blue;
            this.dateBox.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateBox.ForeColor = System.Drawing.Color.White;
            this.dateBox.Location = new System.Drawing.Point(615, 93);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(171, 25);
            this.dateBox.TabIndex = 5;
            this.dateBox.Text = "currentDate";
            this.dateBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // expiredItems
            // 
            this.expiredItems.BorderSkin.BorderColor = System.Drawing.Color.Blue;
            chartArea1.BorderColor = System.Drawing.Color.Blue;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.expiredItems.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.White;
            legend1.Name = "Legend1";
            this.expiredItems.Legends.Add(legend1);
            this.expiredItems.Location = new System.Drawing.Point(424, 138);
            this.expiredItems.Name = "expiredItems";
            this.expiredItems.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.expiredItems.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Aqua,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Fuchsia};
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.expiredItems.Series.Add(series1);
            this.expiredItems.Size = new System.Drawing.Size(349, 300);
            this.expiredItems.TabIndex = 6;
            this.expiredItems.Text = "chart1";
            this.expiredItems.Click += new System.EventHandler(this.expiredItems_Click);
            // 
            // report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.expiredItems);
            this.Controls.Add(this.dateBox);
            this.Controls.Add(titleBox);
            this.Controls.Add(this.chartBack);
            this.Controls.Add(this.listBack);
            this.Name = "report";
            this.Text = "report";
            this.Load += new System.EventHandler(this.report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.expiredItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label listBack;
        private System.Windows.Forms.Label chartBack;
        private System.Windows.Forms.Label dateBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart expiredItems;
     
    }
}