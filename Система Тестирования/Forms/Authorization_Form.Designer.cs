namespace Система_Тестирования
{
    partial class Authorization_Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.name_TextBox = new System.Windows.Forms.TextBox();
            this.password_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.back_Button = new System.Windows.Forms.Button();
            this.logIn_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.password_TextBox);
            this.groupBox1.Controls.Add(this.name_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ввод данных";
            // 
            // name_TextBox
            // 
            this.name_TextBox.Location = new System.Drawing.Point(126, 19);
            this.name_TextBox.Name = "name_TextBox";
            this.name_TextBox.Size = new System.Drawing.Size(120, 20);
            this.name_TextBox.TabIndex = 0;
            // 
            // password_TextBox
            // 
            this.password_TextBox.Location = new System.Drawing.Point(126, 45);
            this.password_TextBox.Name = "password_TextBox";
            this.password_TextBox.Size = new System.Drawing.Size(120, 20);
            this.password_TextBox.TabIndex = 1;
            this.password_TextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя пользователя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // back_Button
            // 
            this.back_Button.Location = new System.Drawing.Point(12, 98);
            this.back_Button.Name = "back_Button";
            this.back_Button.Size = new System.Drawing.Size(75, 23);
            this.back_Button.TabIndex = 1;
            this.back_Button.Text = "Назад";
            this.back_Button.UseVisualStyleBackColor = true;
            // 
            // logIn_Button
            // 
            this.logIn_Button.Location = new System.Drawing.Point(162, 98);
            this.logIn_Button.Name = "logIn_Button";
            this.logIn_Button.Size = new System.Drawing.Size(102, 23);
            this.logIn_Button.TabIndex = 2;
            this.logIn_Button.Text = "Войти в систему";
            this.logIn_Button.UseVisualStyleBackColor = true;
            // 
            // Authorization_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 133);
            this.Controls.Add(this.logIn_Button);
            this.Controls.Add(this.back_Button);
            this.Controls.Add(this.groupBox1);
            this.Name = "Authorization_Form";
            this.Text = "Авторизация";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password_TextBox;
        private System.Windows.Forms.TextBox name_TextBox;
        private System.Windows.Forms.Button back_Button;
        private System.Windows.Forms.Button logIn_Button;
    }
}