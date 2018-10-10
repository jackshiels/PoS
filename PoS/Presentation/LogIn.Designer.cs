namespace PoS
{
    partial class LogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.btnLogIn = new System.Windows.Forms.Button();
            this.txtLoginPass = new System.Windows.Forms.TextBox();
            this.txtLoginEmpId = new System.Windows.Forms.TextBox();
            this.lblLogInWelcome = new System.Windows.Forms.Label();
            this.lblLogInName = new System.Windows.Forms.Label();
            this.lblLogInPassword = new System.Windows.Forms.Label();
            this.pbOrderLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrderLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Location = new System.Drawing.Point(421, 246);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(98, 27);
            this.btnLogIn.TabIndex = 2;
            this.btnLogIn.Text = "&Log In";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // txtLoginPass
            // 
            this.txtLoginPass.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginPass.Location = new System.Drawing.Point(300, 213);
            this.txtLoginPass.Name = "txtLoginPass";
            this.txtLoginPass.Size = new System.Drawing.Size(219, 27);
            this.txtLoginPass.TabIndex = 1;
            // 
            // txtLoginEmpId
            // 
            this.txtLoginEmpId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginEmpId.Location = new System.Drawing.Point(300, 180);
            this.txtLoginEmpId.Name = "txtLoginEmpId";
            this.txtLoginEmpId.Size = new System.Drawing.Size(219, 27);
            this.txtLoginEmpId.TabIndex = 0;
            // 
            // lblLogInWelcome
            // 
            this.lblLogInWelcome.AutoSize = true;
            this.lblLogInWelcome.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogInWelcome.Location = new System.Drawing.Point(296, 153);
            this.lblLogInWelcome.Name = "lblLogInWelcome";
            this.lblLogInWelcome.Size = new System.Drawing.Size(140, 19);
            this.lblLogInWelcome.TabIndex = 6;
            this.lblLogInWelcome.Text = "Enter Log In Details:";
            // 
            // lblLogInName
            // 
            this.lblLogInName.AutoSize = true;
            this.lblLogInName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogInName.Location = new System.Drawing.Point(199, 183);
            this.lblLogInName.Name = "lblLogInName";
            this.lblLogInName.Size = new System.Drawing.Size(94, 19);
            this.lblLogInName.TabIndex = 7;
            this.lblLogInName.Text = "Employee ID:";
            // 
            // lblLogInPassword
            // 
            this.lblLogInPassword.AutoSize = true;
            this.lblLogInPassword.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogInPassword.Location = new System.Drawing.Point(218, 216);
            this.lblLogInPassword.Name = "lblLogInPassword";
            this.lblLogInPassword.Size = new System.Drawing.Size(75, 19);
            this.lblLogInPassword.TabIndex = 8;
            this.lblLogInPassword.Text = "Password:";
            // 
            // pbOrderLogo
            // 
            this.pbOrderLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbOrderLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbOrderLogo.BackgroundImage")));
            this.pbOrderLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbOrderLogo.Location = new System.Drawing.Point(132, 12);
            this.pbOrderLogo.Name = "pbOrderLogo";
            this.pbOrderLogo.Size = new System.Drawing.Size(387, 95);
            this.pbOrderLogo.TabIndex = 9;
            this.pbOrderLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 95);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // LogIn
            // 
            this.AcceptButton = this.btnLogIn;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(794, 338);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbOrderLogo);
            this.Controls.Add(this.lblLogInPassword);
            this.Controls.Add(this.lblLogInName);
            this.Controls.Add(this.lblLogInWelcome);
            this.Controls.Add(this.txtLoginEmpId);
            this.Controls.Add(this.txtLoginPass);
            this.Controls.Add(this.btnLogIn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LogIn";
            this.Text = "Log In";
            ((System.ComponentModel.ISupportInitialize)(this.pbOrderLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.TextBox txtLoginPass;
        private System.Windows.Forms.TextBox txtLoginEmpId;
        private System.Windows.Forms.Label lblLogInWelcome;
        private System.Windows.Forms.Label lblLogInName;
        private System.Windows.Forms.Label lblLogInPassword;
        private System.Windows.Forms.PictureBox pbOrderLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

