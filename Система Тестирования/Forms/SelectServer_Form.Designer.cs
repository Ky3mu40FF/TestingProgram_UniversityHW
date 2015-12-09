namespace Система_Тестирования
{
    partial class SelectServer_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.availableServers_ListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.availableDB_ListBox = new System.Windows.Forms.ListBox();
            this.next_Button = new System.Windows.Forms.Button();
            this.refresh_Button = new System.Windows.Forms.Button();
            this.exit_Button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.showSelectServer_CheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.availableServers_ListBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Доступные сервера";
            // 
            // availableServers_ListBox
            // 
            this.availableServers_ListBox.FormattingEnabled = true;
            this.availableServers_ListBox.Location = new System.Drawing.Point(6, 19);
            this.availableServers_ListBox.Name = "availableServers_ListBox";
            this.availableServers_ListBox.Size = new System.Drawing.Size(265, 82);
            this.availableServers_ListBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.availableDB_ListBox);
            this.groupBox2.Location = new System.Drawing.Point(295, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 116);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Доступные Базы Данных";
            // 
            // availableDB_ListBox
            // 
            this.availableDB_ListBox.FormattingEnabled = true;
            this.availableDB_ListBox.Location = new System.Drawing.Point(6, 19);
            this.availableDB_ListBox.Name = "availableDB_ListBox";
            this.availableDB_ListBox.Size = new System.Drawing.Size(281, 82);
            this.availableDB_ListBox.TabIndex = 0;
            // 
            // next_Button
            // 
            this.next_Button.Location = new System.Drawing.Point(513, 199);
            this.next_Button.Name = "next_Button";
            this.next_Button.Size = new System.Drawing.Size(75, 23);
            this.next_Button.TabIndex = 2;
            this.next_Button.Text = "Далее";
            this.next_Button.UseVisualStyleBackColor = true;
            // 
            // refresh_Button
            // 
            this.refresh_Button.Location = new System.Drawing.Point(464, 134);
            this.refresh_Button.Name = "refresh_Button";
            this.refresh_Button.Size = new System.Drawing.Size(124, 23);
            this.refresh_Button.TabIndex = 3;
            this.refresh_Button.Text = "Обновить списки";
            this.refresh_Button.UseVisualStyleBackColor = true;
            // 
            // exit_Button
            // 
            this.exit_Button.Location = new System.Drawing.Point(12, 199);
            this.exit_Button.Name = "exit_Button";
            this.exit_Button.Size = new System.Drawing.Size(75, 23);
            this.exit_Button.TabIndex = 4;
            this.exit_Button.Text = "Выход";
            this.exit_Button.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 59);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Состояние";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // showSelectServer_CheckBox
            // 
            this.showSelectServer_CheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.showSelectServer_CheckBox.Location = new System.Drawing.Point(295, 196);
            this.showSelectServer_CheckBox.Name = "showSelectServer_CheckBox";
            this.showSelectServer_CheckBox.Size = new System.Drawing.Size(164, 30);
            this.showSelectServer_CheckBox.TabIndex = 6;
            this.showSelectServer_CheckBox.Text = "Выводить в следующий раз окно выбора сервера?";
            this.showSelectServer_CheckBox.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(239, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // SelectServer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 234);
            this.Controls.Add(this.showSelectServer_CheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.exit_Button);
            this.Controls.Add(this.refresh_Button);
            this.Controls.Add(this.next_Button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectServer_Form";
            this.Text = "Выбор сервера";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox availableServers_ListBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox availableDB_ListBox;
        private System.Windows.Forms.Button next_Button;
        private System.Windows.Forms.Button refresh_Button;
        private System.Windows.Forms.Button exit_Button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.CheckBox showSelectServer_CheckBox;
    }
}

