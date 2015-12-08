namespace Система_Тестирования
{
    partial class Student_Form
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
            this.questionPanel = new System.Windows.Forms.Panel();
            this.questionContent_Label = new System.Windows.Forms.Label();
            this.questionNumber_Label = new System.Windows.Forms.Label();
            this.answersPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.next_Button = new System.Windows.Forms.Button();
            this.answer_Button = new System.Windows.Forms.Button();
            this.firstPanel = new System.Windows.Forms.Panel();
            this.exit_Button = new System.Windows.Forms.Button();
            this.start_Button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numOfRetries_Label = new System.Windows.Forms.Label();
            this.numOfCorrectAnswers_Label = new System.Windows.Forms.Label();
            this.lastMark_Label = new System.Windows.Forms.Label();
            this.testPassed_Label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.courseNum_Label = new System.Windows.Forms.Label();
            this.speciality_Label = new System.Windows.Forms.Label();
            this.faculty_Label = new System.Windows.Forms.Label();
            this.studentName_Label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.questionPanel.SuspendLayout();
            this.answersPanel.SuspendLayout();
            this.firstPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // questionPanel
            // 
            this.questionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.questionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questionPanel.Controls.Add(this.questionContent_Label);
            this.questionPanel.Controls.Add(this.questionNumber_Label);
            this.questionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.questionPanel.Location = new System.Drawing.Point(0, 0);
            this.questionPanel.Name = "questionPanel";
            this.questionPanel.Size = new System.Drawing.Size(691, 108);
            this.questionPanel.TabIndex = 0;
            // 
            // questionContent_Label
            // 
            this.questionContent_Label.AutoSize = true;
            this.questionContent_Label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionContent_Label.Location = new System.Drawing.Point(22, 27);
            this.questionContent_Label.Name = "questionContent_Label";
            this.questionContent_Label.Size = new System.Drawing.Size(39, 15);
            this.questionContent_Label.TabIndex = 1;
            this.questionContent_Label.Text = "label2";
            this.questionContent_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // questionNumber_Label
            // 
            this.questionNumber_Label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.questionNumber_Label.AutoSize = true;
            this.questionNumber_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionNumber_Label.Location = new System.Drawing.Point(313, 8);
            this.questionNumber_Label.Name = "questionNumber_Label";
            this.questionNumber_Label.Size = new System.Drawing.Size(81, 16);
            this.questionNumber_Label.TabIndex = 0;
            this.questionNumber_Label.Text = "Вопрос №";
            // 
            // answersPanel
            // 
            this.answersPanel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.answersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.answersPanel.Controls.Add(this.flowLayoutPanel1);
            this.answersPanel.Controls.Add(this.label1);
            this.answersPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.answersPanel.Location = new System.Drawing.Point(0, 108);
            this.answersPanel.Name = "answersPanel";
            this.answersPanel.Size = new System.Drawing.Size(691, 182);
            this.answersPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(684, 146);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(288, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Варианты ответа";
            // 
            // next_Button
            // 
            this.next_Button.Location = new System.Drawing.Point(604, 296);
            this.next_Button.Name = "next_Button";
            this.next_Button.Size = new System.Drawing.Size(75, 23);
            this.next_Button.TabIndex = 2;
            this.next_Button.Text = "Дальше";
            this.next_Button.UseVisualStyleBackColor = true;
            // 
            // answer_Button
            // 
            this.answer_Button.Location = new System.Drawing.Point(307, 296);
            this.answer_Button.Name = "answer_Button";
            this.answer_Button.Size = new System.Drawing.Size(75, 23);
            this.answer_Button.TabIndex = 4;
            this.answer_Button.Text = "Ответить";
            this.answer_Button.UseVisualStyleBackColor = true;
            // 
            // firstPanel
            // 
            this.firstPanel.Controls.Add(this.exit_Button);
            this.firstPanel.Controls.Add(this.start_Button);
            this.firstPanel.Controls.Add(this.groupBox2);
            this.firstPanel.Controls.Add(this.groupBox1);
            this.firstPanel.Location = new System.Drawing.Point(0, 0);
            this.firstPanel.Name = "firstPanel";
            this.firstPanel.Size = new System.Drawing.Size(691, 339);
            this.firstPanel.TabIndex = 5;
            // 
            // exit_Button
            // 
            this.exit_Button.Location = new System.Drawing.Point(15, 296);
            this.exit_Button.Name = "exit_Button";
            this.exit_Button.Size = new System.Drawing.Size(150, 23);
            this.exit_Button.TabIndex = 3;
            this.exit_Button.Text = "Выйти";
            this.exit_Button.UseVisualStyleBackColor = true;
            // 
            // start_Button
            // 
            this.start_Button.Location = new System.Drawing.Point(529, 296);
            this.start_Button.Name = "start_Button";
            this.start_Button.Size = new System.Drawing.Size(150, 23);
            this.start_Button.TabIndex = 2;
            this.start_Button.Text = "Начать тестирование";
            this.start_Button.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numOfRetries_Label);
            this.groupBox2.Controls.Add(this.numOfCorrectAnswers_Label);
            this.groupBox2.Controls.Add(this.lastMark_Label);
            this.groupBox2.Controls.Add(this.testPassed_Label);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(350, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 122);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Статистика";
            // 
            // numOfRetries_Label
            // 
            this.numOfRetries_Label.AutoSize = true;
            this.numOfRetries_Label.Location = new System.Drawing.Point(220, 94);
            this.numOfRetries_Label.Name = "numOfRetries_Label";
            this.numOfRetries_Label.Size = new System.Drawing.Size(40, 13);
            this.numOfRetries_Label.TabIndex = 12;
            this.numOfRetries_Label.Text = "Retries";
            // 
            // numOfCorrectAnswers_Label
            // 
            this.numOfCorrectAnswers_Label.AutoSize = true;
            this.numOfCorrectAnswers_Label.Location = new System.Drawing.Point(220, 68);
            this.numOfCorrectAnswers_Label.Name = "numOfCorrectAnswers_Label";
            this.numOfCorrectAnswers_Label.Size = new System.Drawing.Size(74, 13);
            this.numOfCorrectAnswers_Label.TabIndex = 11;
            this.numOfCorrectAnswers_Label.Text = "NumOfCorrect";
            // 
            // lastMark_Label
            // 
            this.lastMark_Label.AutoSize = true;
            this.lastMark_Label.Location = new System.Drawing.Point(219, 42);
            this.lastMark_Label.Name = "lastMark_Label";
            this.lastMark_Label.Size = new System.Drawing.Size(51, 13);
            this.lastMark_Label.TabIndex = 10;
            this.lastMark_Label.Text = "LastMark";
            // 
            // testPassed_Label
            // 
            this.testPassed_Label.AutoSize = true;
            this.testPassed_Label.Location = new System.Drawing.Point(219, 16);
            this.testPassed_Label.Name = "testPassed_Label";
            this.testPassed_Label.Size = new System.Drawing.Size(63, 13);
            this.testPassed_Label.TabIndex = 9;
            this.testPassed_Label.Text = "TestPassed";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(10, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Количество попыток";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(10, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Количество правильных ответов";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(10, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Последняя оценка";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(10, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Тест пройден: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.courseNum_Label);
            this.groupBox1.Controls.Add(this.speciality_Label);
            this.groupBox1.Controls.Add(this.faculty_Label);
            this.groupBox1.Controls.Add(this.studentName_Label);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о студенте";
            // 
            // courseNum_Label
            // 
            this.courseNum_Label.AutoSize = true;
            this.courseNum_Label.Location = new System.Drawing.Point(102, 94);
            this.courseNum_Label.Name = "courseNum_Label";
            this.courseNum_Label.Size = new System.Drawing.Size(62, 13);
            this.courseNum_Label.TabIndex = 8;
            this.courseNum_Label.Text = "CourseNum";
            // 
            // speciality_Label
            // 
            this.speciality_Label.AutoSize = true;
            this.speciality_Label.Location = new System.Drawing.Point(102, 68);
            this.speciality_Label.Name = "speciality_Label";
            this.speciality_Label.Size = new System.Drawing.Size(52, 13);
            this.speciality_Label.TabIndex = 7;
            this.speciality_Label.Text = "Speciality";
            // 
            // faculty_Label
            // 
            this.faculty_Label.AutoSize = true;
            this.faculty_Label.Location = new System.Drawing.Point(101, 42);
            this.faculty_Label.Name = "faculty_Label";
            this.faculty_Label.Size = new System.Drawing.Size(41, 13);
            this.faculty_Label.TabIndex = 6;
            this.faculty_Label.Text = "Faculty";
            // 
            // studentName_Label
            // 
            this.studentName_Label.AutoSize = true;
            this.studentName_Label.Location = new System.Drawing.Point(101, 16);
            this.studentName_Label.Name = "studentName_Label";
            this.studentName_Label.Size = new System.Drawing.Size(34, 13);
            this.studentName_Label.TabIndex = 5;
            this.studentName_Label.Text = "ФИО";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(6, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Номер курса:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Направление:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Факультет:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ФИО:";
            // 
            // Student_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 339);
            this.Controls.Add(this.firstPanel);
            this.Controls.Add(this.answer_Button);
            this.Controls.Add(this.next_Button);
            this.Controls.Add(this.answersPanel);
            this.Controls.Add(this.questionPanel);
            this.Name = "Student_Form";
            this.Text = "Student_Form";
            this.questionPanel.ResumeLayout(false);
            this.questionPanel.PerformLayout();
            this.answersPanel.ResumeLayout(false);
            this.answersPanel.PerformLayout();
            this.firstPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel questionPanel;
        private System.Windows.Forms.Label questionContent_Label;
        private System.Windows.Forms.Label questionNumber_Label;
        private System.Windows.Forms.Panel answersPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button next_Button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button answer_Button;
        private System.Windows.Forms.Panel firstPanel;
        private System.Windows.Forms.Button exit_Button;
        private System.Windows.Forms.Button start_Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label numOfRetries_Label;
        private System.Windows.Forms.Label numOfCorrectAnswers_Label;
        private System.Windows.Forms.Label lastMark_Label;
        private System.Windows.Forms.Label testPassed_Label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label courseNum_Label;
        private System.Windows.Forms.Label speciality_Label;
        private System.Windows.Forms.Label faculty_Label;
        private System.Windows.Forms.Label studentName_Label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}