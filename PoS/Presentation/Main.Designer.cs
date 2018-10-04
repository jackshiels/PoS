namespace PoS.Presentation
{
    partial class Main
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
            this.lblHome = new System.Windows.Forms.Label();
            this.pbUserIcon = new System.Windows.Forms.PictureBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lstFunctions = new System.Windows.Forms.ListBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.grpFunction = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BackColor = System.Drawing.Color.Transparent;
            this.lblHome.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHome.Location = new System.Drawing.Point(155, 9);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(96, 39);
            this.lblHome.TabIndex = 0;
            this.lblHome.Text = "Home";
            // 
            // pbUserIcon
            // 
            this.pbUserIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbUserIcon.BackgroundImage = global::PoS.Properties.Resources.UserIcon2;
            this.pbUserIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUserIcon.Location = new System.Drawing.Point(987, 9);
            this.pbUserIcon.Name = "pbUserIcon";
            this.pbUserIcon.Size = new System.Drawing.Size(94, 95);
            this.pbUserIcon.TabIndex = 2;
            this.pbUserIcon.TabStop = false;
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Location = new System.Drawing.Point(883, 77);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(98, 27);
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.Text = "&Log Out";
            this.btnLogIn.UseVisualStyleBackColor = true;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserName.Location = new System.Drawing.Point(898, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(83, 19);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "Bill Watson";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.BackColor = System.Drawing.Color.Transparent;
            this.lblUserRole.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserRole.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserRole.Location = new System.Drawing.Point(870, 32);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUserRole.Size = new System.Drawing.Size(111, 19);
            this.lblUserRole.TabIndex = 5;
            this.lblUserRole.Text = "Marketing Clerk";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lstFunctions
            // 
            this.lstFunctions.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFunctions.FormattingEnabled = true;
            this.lstFunctions.ItemHeight = 19;
            this.lstFunctions.Location = new System.Drawing.Point(12, 107);
            this.lstFunctions.Name = "lstFunctions";
            this.lstFunctions.Size = new System.Drawing.Size(239, 403);
            this.lstFunctions.TabIndex = 8;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImage = global::PoS.Properties.Resources.PoppelLogo1;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(12, 9);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(137, 95);
            this.pbLogo.TabIndex = 9;
            this.pbLogo.TabStop = false;
            // 
            // grpFunction
            // 
            this.grpFunction.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFunction.Location = new System.Drawing.Point(257, 107);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Size = new System.Drawing.Size(824, 403);
            this.grpFunction.TabIndex = 10;
            this.grpFunction.TabStop = false;
            this.grpFunction.Text = "Function";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PoS.Properties.Resources.Motif;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1093, 522);
            this.Controls.Add(this.grpFunction);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lstFunctions);
            this.Controls.Add(this.lblUserRole);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.pbUserIcon);
            this.Controls.Add(this.lblHome);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.pbUserIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.PictureBox pbUserIcon;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.ListBox lstFunctions;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.GroupBox grpFunction;
    }
}