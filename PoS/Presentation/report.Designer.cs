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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label titleBox;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 78D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 526D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 20D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.listBack = new System.Windows.Forms.Label();
            this.chartBack = new System.Windows.Forms.Label();
            this.dateBox = new System.Windows.Forms.Label();
            this.expiredItems = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reportTable = new System.Windows.Forms.DataGridView();
            this.dBMainDataSet = new PoS.DBMainDataSet();
            this.dBMainDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.prodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shelfLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            titleBox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.expiredItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBMainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBMainDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBox
            // 
            titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            titleBox.Size = new System.Drawing.Size(909, 66);
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
            this.listBack.Size = new System.Drawing.Size(502, 311);
            this.listBack.TabIndex = 3;
            // 
            // chartBack
            // 
            this.chartBack.BackColor = System.Drawing.Color.Blue;
            this.chartBack.Location = new System.Drawing.Point(543, 130);
            this.chartBack.Name = "chartBack";
            this.chartBack.Size = new System.Drawing.Size(381, 311);
            this.chartBack.TabIndex = 4;
            // 
            // dateBox
            // 
            this.dateBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateBox.BackColor = System.Drawing.Color.Blue;
            this.dateBox.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateBox.ForeColor = System.Drawing.Color.White;
            this.dateBox.Location = new System.Drawing.Point(715, 85);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(209, 25);
            this.dateBox.TabIndex = 5;
            this.dateBox.Text = "currentDate";
            this.dateBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // expiredItems
            // 
            this.expiredItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.expiredItems.BackColor = System.Drawing.Color.GhostWhite;
            this.expiredItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.expiredItems.BorderSkin.BorderColor = System.Drawing.Color.Blue;
            chartArea2.Name = "ChartArea1";
            chartArea2.ShadowColor = System.Drawing.Color.Transparent;
            this.expiredItems.ChartAreas.Add(chartArea2);
            this.expiredItems.Location = new System.Drawing.Point(558, 141);
            this.expiredItems.Name = "expiredItems";
            this.expiredItems.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.expiredItems.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Aqua,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Fuchsia};
            series2.BorderColor = System.Drawing.Color.DarkGray;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Blue;
            series2.IsValueShownAsLabel = true;
            series2.Name = "Series1";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            this.expiredItems.Series.Add(series2);
            this.expiredItems.Size = new System.Drawing.Size(355, 293);
            this.expiredItems.TabIndex = 6;
            title2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Bar Graph Representing the Number of Expired Objects";
            title2.Text = "Bar Graph Representing the Number of Expired Objects";
            this.expiredItems.Titles.Add(title2);
            this.expiredItems.Click += new System.EventHandler(this.expiredItems_Click);
            // 
            // reportTable
            // 
            this.reportTable.AllowUserToAddRows = false;
            this.reportTable.AllowUserToDeleteRows = false;
            this.reportTable.AllowUserToResizeColumns = false;
            this.reportTable.AllowUserToResizeRows = false;
            this.reportTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prodName,
            this.expiryDate,
            this.quantity,
            this.shelfLocation,
            this.writeOff});
            this.reportTable.Location = new System.Drawing.Point(16, 137);
            this.reportTable.Name = "reportTable";
            this.reportTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.reportTable.Size = new System.Drawing.Size(494, 293);
            this.reportTable.TabIndex = 7;
            this.reportTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dBMainDataSet
            // 
            this.dBMainDataSet.DataSetName = "DBMainDataSet";
            this.dBMainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dBMainDataSetBindingSource
            // 
            this.dBMainDataSetBindingSource.DataSource = this.dBMainDataSet;
            this.dBMainDataSetBindingSource.Position = 0;
            // 
            // prodName
            // 
            this.prodName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.prodName.HeaderText = "Product Name";
            this.prodName.Name = "prodName";
            this.prodName.Width = 90;
            // 
            // expiryDate
            // 
            this.expiryDate.HeaderText = "Expiry Date";
            this.expiryDate.Name = "expiryDate";
            this.expiryDate.Width = 90;
            // 
            // quantity
            // 
            this.quantity.HeaderText = "Quantity";
            this.quantity.Name = "quantity";
            this.quantity.Width = 90;
            // 
            // shelfLocation
            // 
            this.shelfLocation.HeaderText = "Location on Shelf";
            this.shelfLocation.Name = "shelfLocation";
            this.shelfLocation.Width = 90;
            // 
            // writeOff
            // 
            this.writeOff.HeaderText = "Total Write-Off (R)";
            this.writeOff.Name = "writeOff";
            this.writeOff.Width = 90;
            // 
            // report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 464);
            this.Controls.Add(this.reportTable);
            this.Controls.Add(this.expiredItems);
            this.Controls.Add(this.dateBox);
            this.Controls.Add(titleBox);
            this.Controls.Add(this.chartBack);
            this.Controls.Add(this.listBack);
            this.Name = "report";
            this.Text = "report";
            this.Load += new System.EventHandler(this.report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.expiredItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBMainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBMainDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label listBack;
        private System.Windows.Forms.Label chartBack;
        private System.Windows.Forms.Label dateBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart expiredItems;
        private System.Windows.Forms.DataGridView reportTable;
        private DBMainDataSet dBMainDataSet;
        private System.Windows.Forms.BindingSource dBMainDataSetBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn shelfLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn writeOff;
    }
}