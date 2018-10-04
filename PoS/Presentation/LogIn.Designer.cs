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
            this.btnLogIn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblLogInWelcome = new System.Windows.Forms.Label();
            this.lblLogInName = new System.Windows.Forms.Label();
            this.lblLogInPassword = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Location = new System.Drawing.Point(421, 246);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(98, 27);
            this.btnLogIn.TabIndex = 0;
            this.btnLogIn.Text = "&Log In";
            this.btnLogIn.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(300, 213);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 27);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(300, 180);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(219, 27);
            this.textBox2.TabIndex = 2;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImage = global::PoS.Properties.Resources.PoppelLogo1;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(188, 136);
            this.pbLogo.TabIndex = 3;
            this.pbLogo.TabStop = false;
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
            this.lblLogInName.Location = new System.Drawing.Point(214, 183);
            this.lblLogInName.Name = "lblLogInName";
            this.lblLogInName.Size = new System.Drawing.Size(79, 19);
            this.lblLogInName.TabIndex = 7;
            this.lblLogInName.Text = "Username:";
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
            // pbTitle
            // 
            this.pbTitle.BackColor = System.Drawing.Color.Transparent;
            this.pbTitle.BackgroundImage = global::PoS.Properties.Resources.Logo22;
            this.pbTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbTitle.Location = new System.Drawing.Point(206, 42);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(438, 106);
            this.pbTitle.TabIndex = 9;
            this.pbTitle.TabStop = false;
            // 
            // LogIn
            // 
            this.AcceptButton = this.btnLogIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::PoS.Properties.Resources.Motif;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(814, 329);
            this.Controls.Add(this.pbTitle);
            this.Controls.Add(this.lblLogInPassword);
            this.Controls.Add(this.lblLogInName);
            this.Controls.Add(this.lblLogInWelcome);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnLogIn);
            this.DoubleBuffered = true;
            this.Name = "LogIn";
            this.Text = "Log In";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblLogInWelcome;
        private System.Windows.Forms.Label lblLogInName;
        private System.Windows.Forms.Label lblLogInPassword;
        private System.Windows.Forms.PictureBox pbTitle;
    }
}

