namespace App.Desktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            pnl_Login = new Panel();
            btn_login = new Button();
            txt_password = new TextBox();
            txt_username = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pnl_Login.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(96, 28);
            label1.Name = "label1";
            label1.Size = new Size(97, 38);
            label1.TabIndex = 0;
            label1.Text = "Email:";
            // 
            // pnl_Login
            // 
            pnl_Login.BackColor = SystemColors.ButtonFace;
            pnl_Login.Controls.Add(btn_login);
            pnl_Login.Controls.Add(txt_password);
            pnl_Login.Controls.Add(txt_username);
            pnl_Login.Controls.Add(label2);
            pnl_Login.Controls.Add(label1);
            pnl_Login.Location = new Point(150, 132);
            pnl_Login.Name = "pnl_Login";
            pnl_Login.Size = new Size(432, 189);
            pnl_Login.TabIndex = 1;
            // 
            // btn_login
            // 
            btn_login.BackColor = SystemColors.ActiveCaption;
            btn_login.FlatStyle = FlatStyle.Popup;
            btn_login.ForeColor = SystemColors.ActiveCaptionText;
            btn_login.Location = new Point(278, 125);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(112, 34);
            btn_login.TabIndex = 4;
            btn_login.Text = "Giriş Yap";
            btn_login.UseVisualStyleBackColor = false;
            btn_login.Click += btn_login_Click;
            // 
            // txt_password
            // 
            txt_password.Location = new Point(207, 71);
            txt_password.Name = "txt_password";
            txt_password.PlaceholderText = "Şifre Giriniz";
            txt_password.Size = new Size(183, 31);
            txt_password.TabIndex = 3;
            // 
            // txt_username
            // 
            txt_username.Location = new Point(207, 28);
            txt_username.Name = "txt_username";
            txt_username.PlaceholderText = "E-mail giriniz";
            txt_username.Size = new Size(183, 31);
            txt_username.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(107, 65);
            label2.Name = "label2";
            label2.Size = new Size(86, 38);
            label2.TabIndex = 1;
            label2.Text = "Şifre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(76, 24);
            label3.Name = "label3";
            label3.Size = new Size(720, 54);
            label3.TabIndex = 2;
            label3.Text = "API ile Web projesinin Desktop Kısmı";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 78);
            label4.Name = "label4";
            label4.Size = new Size(784, 25);
            label4.TabIndex = 3;
            label4.Text = "Sadece Login ve Token ile kullanıcıların çekilmesi ve DataGridView de listelenmesi işlemi mevcuttur.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pnl_Login);
            Name = "Form1";
            Text = "Form1";
            pnl_Login.ResumeLayout(false);
            pnl_Login.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel pnl_Login;
        private Button btn_login;
        private TextBox txt_password;
        private TextBox txt_username;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}