namespace passwordVault2Host
{
    partial class LoginWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWin));
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.signPassword_txt = new System.Windows.Forms.TextBox();
            this.signVerifyPassword_txt = new System.Windows.Forms.TextBox();
            this.buttonSignup = new System.Windows.Forms.Button();
            this.labelVerifyPass = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.name_txt = new System.Windows.Forms.TextBox();
            this.labelEnterPass = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Password.Location = new System.Drawing.Point(49, 132);
            this.txt_Password.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(212, 30);
            this.txt_Password.TabIndex = 3;
            this.txt_Password.UseSystemPasswordChar = true;
            this.txt_Password.Visible = false;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(181)))), ((int)(((byte)(41)))));
            this.buttonLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(98, 182);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(112, 40);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Visible = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // signPassword_txt
            // 
            this.signPassword_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signPassword_txt.Location = new System.Drawing.Point(49, 98);
            this.signPassword_txt.Margin = new System.Windows.Forms.Padding(4);
            this.signPassword_txt.Name = "signPassword_txt";
            this.signPassword_txt.Size = new System.Drawing.Size(212, 26);
            this.signPassword_txt.TabIndex = 37;
            this.signPassword_txt.UseSystemPasswordChar = true;
            // 
            // signVerifyPassword_txt
            // 
            this.signVerifyPassword_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signVerifyPassword_txt.Location = new System.Drawing.Point(49, 160);
            this.signVerifyPassword_txt.Margin = new System.Windows.Forms.Padding(4);
            this.signVerifyPassword_txt.Name = "signVerifyPassword_txt";
            this.signVerifyPassword_txt.Size = new System.Drawing.Size(212, 26);
            this.signVerifyPassword_txt.TabIndex = 36;
            this.signVerifyPassword_txt.UseSystemPasswordChar = true;
            // 
            // buttonSignup
            // 
            this.buttonSignup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(181)))), ((int)(((byte)(41)))));
            this.buttonSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSignup.FlatAppearance.BorderSize = 0;
            this.buttonSignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSignup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSignup.ForeColor = System.Drawing.Color.White;
            this.buttonSignup.Location = new System.Drawing.Point(98, 200);
            this.buttonSignup.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSignup.Name = "buttonSignup";
            this.buttonSignup.Size = new System.Drawing.Size(112, 40);
            this.buttonSignup.TabIndex = 35;
            this.buttonSignup.Text = "Sign up";
            this.buttonSignup.UseCompatibleTextRendering = true;
            this.buttonSignup.UseVisualStyleBackColor = false;
            this.buttonSignup.Click += new System.EventHandler(this.buttonSignup_Click);
            // 
            // labelVerifyPass
            // 
            this.labelVerifyPass.AutoSize = true;
            this.labelVerifyPass.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVerifyPass.Location = new System.Drawing.Point(44, 128);
            this.labelVerifyPass.Name = "labelVerifyPass";
            this.labelVerifyPass.Size = new System.Drawing.Size(174, 28);
            this.labelVerifyPass.TabIndex = 34;
            this.labelVerifyPass.Text = "Verify Password";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.Location = new System.Drawing.Point(44, 66);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(108, 28);
            this.labelPass.TabIndex = 33;
            this.labelPass.Text = "Password";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(45, 4);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(123, 28);
            this.labelName.TabIndex = 32;
            this.labelName.Text = "Your Name";
            // 
            // name_txt
            // 
            this.name_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.name_txt.Location = new System.Drawing.Point(50, 37);
            this.name_txt.Name = "name_txt";
            this.name_txt.Size = new System.Drawing.Size(212, 26);
            this.name_txt.TabIndex = 29;
            // 
            // labelEnterPass
            // 
            this.labelEnterPass.AutoSize = true;
            this.labelEnterPass.BackColor = System.Drawing.Color.Teal;
            this.labelEnterPass.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEnterPass.Location = new System.Drawing.Point(45, 94);
            this.labelEnterPass.Name = "labelEnterPass";
            this.labelEnterPass.Size = new System.Drawing.Size(166, 28);
            this.labelEnterPass.TabIndex = 39;
            this.labelEnterPass.Text = "Enter Password";
            this.labelEnterPass.Visible = false;
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(12, 22);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(153, 41);
            this.userName.TabIndex = 40;
            this.userName.Text = "Hello ... !";
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.userName.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Teal;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.Teal;
            this.dataGridView1.Location = new System.Drawing.Point(19, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(270, 177);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.Visible = false;
            // 
            // LoginWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(311, 258);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.labelEnterPass);
            this.Controls.Add(this.signPassword_txt);
            this.Controls.Add(this.signVerifyPassword_txt);
            this.Controls.Add(this.buttonSignup);
            this.Controls.Add(this.labelVerifyPass);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.name_txt);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LoginWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox signPassword_txt;
        private System.Windows.Forms.TextBox signVerifyPassword_txt;
        private System.Windows.Forms.Button buttonSignup;
        private System.Windows.Forms.Label labelVerifyPass;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox name_txt;
        private System.Windows.Forms.Label labelEnterPass;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}