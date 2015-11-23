namespace Система_Тестирования
{
    partial class Add_Student_Form
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lastName_TextBox = new System.Windows.Forms.TextBox();
            this.firstName_TextBox = new System.Windows.Forms.TextBox();
            this.patronymic_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.faculty_ComboBox = new System.Windows.Forms.ComboBox();
            this.speciality_ComboBox = new System.Windows.Forms.ComboBox();
            this.course_ComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nickname_TextBox = new System.Windows.Forms.TextBox();
            this.password_TextBox = new System.Windows.Forms.TextBox();
            this.addNewStudent_Button = new System.Windows.Forms.Button();
            this.back_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.course_ComboBox);
            this.groupBox1.Controls.Add(this.speciality_ComboBox);
            this.groupBox1.Controls.Add(this.faculty_ComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.patronymic_TextBox);
            this.groupBox1.Controls.Add(this.firstName_TextBox);
            this.groupBox1.Controls.Add(this.lastName_TextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о студенте";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.password_TextBox);
            this.groupBox2.Controls.Add(this.nickname_TextBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Данные для входа в систему";
            // 
            // lastName_TextBox
            // 
            this.lastName_TextBox.Location = new System.Drawing.Point(114, 16);
            this.lastName_TextBox.Name = "lastName_TextBox";
            this.lastName_TextBox.Size = new System.Drawing.Size(128, 20);
            this.lastName_TextBox.TabIndex = 0;
            // 
            // firstName_TextBox
            // 
            this.firstName_TextBox.Location = new System.Drawing.Point(114, 42);
            this.firstName_TextBox.Name = "firstName_TextBox";
            this.firstName_TextBox.Size = new System.Drawing.Size(128, 20);
            this.firstName_TextBox.TabIndex = 1;
            // 
            // patronymic_TextBox
            // 
            this.patronymic_TextBox.Location = new System.Drawing.Point(114, 68);
            this.patronymic_TextBox.Name = "patronymic_TextBox";
            this.patronymic_TextBox.Size = new System.Drawing.Size(128, 20);
            this.patronymic_TextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Имя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Факультет";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Специальность";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Номер курса";
            // 
            // faculty_ComboBox
            // 
            this.faculty_ComboBox.FormattingEnabled = true;
            this.faculty_ComboBox.Items.AddRange(new object[] {
            "ФОИСТ",
            "ФПКиФ",
            "ФКиГ"});
            this.faculty_ComboBox.Location = new System.Drawing.Point(114, 94);
            this.faculty_ComboBox.Name = "faculty_ComboBox";
            this.faculty_ComboBox.Size = new System.Drawing.Size(128, 21);
            this.faculty_ComboBox.TabIndex = 9;
            // 
            // speciality_ComboBox
            // 
            this.speciality_ComboBox.FormattingEnabled = true;
            this.speciality_ComboBox.Items.AddRange(new object[] {
            "ИБ",
            "Оптотехника",
            "ЛТиЛТ",
            "ГиДЗ",
            "ИСиТ",
            "КиГ"});
            this.speciality_ComboBox.Location = new System.Drawing.Point(114, 121);
            this.speciality_ComboBox.Name = "speciality_ComboBox";
            this.speciality_ComboBox.Size = new System.Drawing.Size(128, 21);
            this.speciality_ComboBox.TabIndex = 10;
            // 
            // course_ComboBox
            // 
            this.course_ComboBox.FormattingEnabled = true;
            this.course_ComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.course_ComboBox.Location = new System.Drawing.Point(114, 148);
            this.course_ComboBox.Name = "course_ComboBox";
            this.course_ComboBox.Size = new System.Drawing.Size(128, 21);
            this.course_ComboBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Псевдоним";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Пароль";
            // 
            // nickname_TextBox
            // 
            this.nickname_TextBox.Location = new System.Drawing.Point(114, 24);
            this.nickname_TextBox.Name = "nickname_TextBox";
            this.nickname_TextBox.Size = new System.Drawing.Size(128, 20);
            this.nickname_TextBox.TabIndex = 2;
            // 
            // password_TextBox
            // 
            this.password_TextBox.Location = new System.Drawing.Point(114, 50);
            this.password_TextBox.Name = "password_TextBox";
            this.password_TextBox.Size = new System.Drawing.Size(128, 20);
            this.password_TextBox.TabIndex = 3;
            // 
            // addNewStudent_Button
            // 
            this.addNewStudent_Button.Location = new System.Drawing.Point(167, 285);
            this.addNewStudent_Button.Name = "addNewStudent_Button";
            this.addNewStudent_Button.Size = new System.Drawing.Size(75, 23);
            this.addNewStudent_Button.TabIndex = 2;
            this.addNewStudent_Button.Text = "Добавить";
            this.addNewStudent_Button.UseVisualStyleBackColor = true;
            // 
            // back_Button
            // 
            this.back_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.back_Button.Location = new System.Drawing.Point(12, 285);
            this.back_Button.Name = "back_Button";
            this.back_Button.Size = new System.Drawing.Size(75, 23);
            this.back_Button.TabIndex = 3;
            this.back_Button.Text = "Назад";
            this.back_Button.UseVisualStyleBackColor = true;
            // 
            // Add_Student_Form
            // 
            this.AcceptButton = this.addNewStudent_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.back_Button;
            this.ClientSize = new System.Drawing.Size(254, 311);
            this.Controls.Add(this.back_Button);
            this.Controls.Add(this.addNewStudent_Button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Add_Student_Form";
            this.Text = "Добавить студента";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox patronymic_TextBox;
        private System.Windows.Forms.TextBox firstName_TextBox;
        private System.Windows.Forms.TextBox lastName_TextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox course_ComboBox;
        private System.Windows.Forms.ComboBox speciality_ComboBox;
        private System.Windows.Forms.ComboBox faculty_ComboBox;
        private System.Windows.Forms.TextBox password_TextBox;
        private System.Windows.Forms.TextBox nickname_TextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button addNewStudent_Button;
        private System.Windows.Forms.Button back_Button;
    }
}