namespace Система_Тестирования
{
    partial class Admin_Form
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.editStudentsData_Button = new System.Windows.Forms.Button();
            this.refreshStudentsDataGrid_Button = new System.Windows.Forms.Button();
            this.Students_DataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.editQuestions_Button = new System.Windows.Forms.Button();
            this.refreshQuestionsDataGrid_Button = new System.Windows.Forms.Button();
            this.Questions_DataGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Students_DataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Questions_DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(908, 360);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.editStudentsData_Button);
            this.tabPage1.Controls.Add(this.refreshStudentsDataGrid_Button);
            this.tabPage1.Controls.Add(this.Students_DataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(900, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Студенты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // editStudentsData_Button
            // 
            this.editStudentsData_Button.Location = new System.Drawing.Point(3, 305);
            this.editStudentsData_Button.Name = "editStudentsData_Button";
            this.editStudentsData_Button.Size = new System.Drawing.Size(234, 23);
            this.editStudentsData_Button.TabIndex = 4;
            this.editStudentsData_Button.Text = "Редактировать данные о студенте";
            this.editStudentsData_Button.UseVisualStyleBackColor = true;
            // 
            // refreshStudentsDataGrid_Button
            // 
            this.refreshStudentsDataGrid_Button.Location = new System.Drawing.Point(739, 305);
            this.refreshStudentsDataGrid_Button.Name = "refreshStudentsDataGrid_Button";
            this.refreshStudentsDataGrid_Button.Size = new System.Drawing.Size(158, 23);
            this.refreshStudentsDataGrid_Button.TabIndex = 3;
            this.refreshStudentsDataGrid_Button.Text = "Обновить таблицу";
            this.refreshStudentsDataGrid_Button.UseVisualStyleBackColor = true;
            // 
            // Students_DataGridView
            // 
            this.Students_DataGridView.AllowUserToAddRows = false;
            this.Students_DataGridView.AllowUserToDeleteRows = false;
            this.Students_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Students_DataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.Students_DataGridView.Location = new System.Drawing.Point(3, 3);
            this.Students_DataGridView.Name = "Students_DataGridView";
            this.Students_DataGridView.ReadOnly = true;
            this.Students_DataGridView.Size = new System.Drawing.Size(894, 296);
            this.Students_DataGridView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.editQuestions_Button);
            this.tabPage2.Controls.Add(this.refreshQuestionsDataGrid_Button);
            this.tabPage2.Controls.Add(this.Questions_DataGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(900, 334);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Вопросы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // editQuestions_Button
            // 
            this.editQuestions_Button.Location = new System.Drawing.Point(3, 305);
            this.editQuestions_Button.Name = "editQuestions_Button";
            this.editQuestions_Button.Size = new System.Drawing.Size(234, 23);
            this.editQuestions_Button.TabIndex = 6;
            this.editQuestions_Button.Text = "Редактировать вопросы";
            this.editQuestions_Button.UseVisualStyleBackColor = true;
            // 
            // refreshQuestionsDataGrid_Button
            // 
            this.refreshQuestionsDataGrid_Button.Location = new System.Drawing.Point(739, 305);
            this.refreshQuestionsDataGrid_Button.Name = "refreshQuestionsDataGrid_Button";
            this.refreshQuestionsDataGrid_Button.Size = new System.Drawing.Size(158, 23);
            this.refreshQuestionsDataGrid_Button.TabIndex = 5;
            this.refreshQuestionsDataGrid_Button.Text = "Обновить таблицу";
            this.refreshQuestionsDataGrid_Button.UseVisualStyleBackColor = true;
            // 
            // Questions_DataGridView
            // 
            this.Questions_DataGridView.AllowUserToAddRows = false;
            this.Questions_DataGridView.AllowUserToDeleteRows = false;
            this.Questions_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Questions_DataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.Questions_DataGridView.Location = new System.Drawing.Point(3, 3);
            this.Questions_DataGridView.Name = "Questions_DataGridView";
            this.Questions_DataGridView.ReadOnly = true;
            this.Questions_DataGridView.Size = new System.Drawing.Size(894, 296);
            this.Questions_DataGridView.TabIndex = 4;
            // 
            // Admin_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 360);
            this.Controls.Add(this.tabControl1);
            this.Name = "Admin_Form";
            this.Text = "Администратор";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Students_DataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Questions_DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView Students_DataGridView;
        private System.Windows.Forms.Button refreshStudentsDataGrid_Button;
        private System.Windows.Forms.Button editStudentsData_Button;
        private System.Windows.Forms.Button editQuestions_Button;
        private System.Windows.Forms.Button refreshQuestionsDataGrid_Button;
        private System.Windows.Forms.DataGridView Questions_DataGridView;
    }
}