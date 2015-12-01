using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Система_Тестирования
{
    public partial class Admin_Form : Form
    {
        /*-------------------- Переменные --------------------*/

        SqlConnection connectionString;

        DataTable commonStudentsTable;
        SqlDataAdapter commonStudentsAdapter;
        SqlCommandBuilder commandBuilderCommonStudents;

        DataTable commonQuestionsTable;
        SqlDataAdapter commonQuestionsAdapter;
        SqlCommandBuilder commandBuilderCommonQuestions;

        DataTable studentsTable;
        DataTable loginTable;
        DataTable questionsTable;
        DataTable answersTable;
        SqlDataAdapter studentsAdapter;
        SqlDataAdapter loginAdapter;
        SqlDataAdapter questionsAdapter;
        SqlDataAdapter answersAdapter;
        SqlCommandBuilder commandBuilderStudents;
        SqlCommandBuilder commandBuilderLogin;
        SqlCommandBuilder commandBuilderQuestions;
        SqlCommandBuilder commandBuilderAnswers;


        /*-------------------- Конструкторы --------------------*/

        public Admin_Form()
        {
            InitializeComponent(); 
        }

        public Admin_Form(SqlConnection connection)
        {
            InitializeComponent();

            this.connectionString = connection;
            Configure();
        }


        /*-------------------- Делегаты --------------------*/

        private void Admin_Form_Load(object sender, EventArgs e)
        {
            GetAllNecessaryTables();

            Students_DataGridView.DataSource = commonStudentsTable;
            Questions_DataGridView.DataSource = commonQuestionsTable;
        }

        private void Admin_Form_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Вы уверены, что хотите выйти?",
                "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void AddNewStudent_Button_Click(object sender, EventArgs e)
        {
            Add_Student_Form addStudentForm = new Add_Student_Form(ref studentsTable, 
                                                                    ref loginTable, 
                                                                    studentsAdapter, 
                                                                    loginAdapter,
                                                                    this);
            addStudentForm.Show();
            this.Hide();
        }

        private void EditStudentsData_Button_Click(object sender, EventArgs e)
        {
            Edit_Students_Form editStudentsForm = new Edit_Students_Form(ref studentsTable,
                                                                    ref loginTable,
                                                                    studentsAdapter,
                                                                    loginAdapter,
                                                                    this,
                                                                    connectionString);
            editStudentsForm.Show();
            this.Hide();
        }

        private void EditQuestions_Button_Click(object sender, EventArgs e)
        {
            Edit_Questions_Form editQuestionsForm = new Edit_Questions_Form(ref questionsTable,
                                                                    ref answersTable,
                                                                    questionsAdapter,
                                                                    answersAdapter,
                                                                    this,
                                                                    connectionString);
            editQuestionsForm.Show();
            this.Hide();
        }

        private void RefreshStudentsDataGrid_Button_Click(object sender, EventArgs e)
        {
            try
            {
                commonStudentsTable.Clear();
                commonStudentsAdapter.Fill(commonStudentsTable);
                Students_DataGridView.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось обновить таблицу!\n\n"+ex.ToString(),
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshQuestionsDataGrid_Button_Click(object sender, EventArgs e)
        {
            try
            {
                commonQuestionsTable.Clear();
                commonQuestionsAdapter.Fill(commonQuestionsTable);
                Questions_DataGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить таблицу!\n\n" + ex.ToString(),
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_Button_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                studentsAdapter.Update(studentsTable);
                loginAdapter.Update(loginTable);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            */
            Students_DataGridView.Update();
        }

        /*-------------------- Остальные функции --------------------*/

        private void Configure()
        {
            this.Load += new EventHandler(Admin_Form_Load);
            this.FormClosing += new FormClosingEventHandler(Admin_Form_Closing);
            //addNewStudent_Button.Click += new EventHandler(AddNewStudent_Button_Click);
            editStudentsData_Button.Click += new EventHandler(EditStudentsData_Button_Click);
            editQuestions_Button.Click += new EventHandler(EditQuestions_Button_Click);
            refreshStudentsDataGrid_Button.Click += new EventHandler(RefreshStudentsDataGrid_Button_Click);
            refreshQuestionsDataGrid_Button.Click += new EventHandler(RefreshQuestionsDataGrid_Button_Click);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void GetAllNecessaryTables()
        {
            try
            {
                commonStudentsTable = new DataTable("Students");
                commonStudentsAdapter = new SqlDataAdapter(
                    "SELECT Students.LastName, Students.FirstName, Students.Patronymic, Students.Faculty, "+
                        "Students.Speciality, Students.Course, Students.IsTestPassed, Students.Mark, Students.NumOfRetries, " +
                        "Login.Username, Login.Password " +
                    "FROM Students INNER JOIN Login ON Students.Students_LoginFK = Login.LoginID",
                    connectionString);
                commonStudentsAdapter.Fill(commonStudentsTable);

                commonQuestionsTable = new DataTable("Questions");
                commonQuestionsAdapter = new SqlDataAdapter(
                    "SELECT Questions.QuestionNumber, Questions.QuestionContent, Questions.QuestionType, " +
                        "Questions.CorrectAnswer, " +
                        "Answers.Answer_1, Answers.Answer_2, Answers.Answer_3, Answers.Answer_4, " +
                        "Answers.Answer_5, Answers.Answer_6, Answers.Answer_7, Answers.Answer_8 " +
                    "FROM Questions INNER JOIN Answers ON Questions.Questions_AnswersFK = Answers.AnswersID",
                    connectionString);
                commonQuestionsAdapter.Fill(commonQuestionsTable);

                studentsTable = new DataTable("Students");
                loginTable = new DataTable("Login");
                questionsTable = new DataTable("Questions");
                answersTable = new DataTable("Answers");

                studentsAdapter = new SqlDataAdapter("SELECT * FROM Students",
                    connectionString);
                loginAdapter = new SqlDataAdapter("SELECT * FROM Login WHERE Login.Role NOT LIKE 'Teacher'",
                    connectionString);
                questionsAdapter = new SqlDataAdapter("SELECT * FROM Questions",
                    connectionString);
                answersAdapter = new SqlDataAdapter("SELECT * FROM Answers",
                    connectionString);

                studentsAdapter.Fill(studentsTable);
                loginAdapter.Fill(loginTable);
                questionsAdapter.Fill(questionsTable);
                answersAdapter.Fill(answersTable);

                commandBuilderStudents = new SqlCommandBuilder(studentsAdapter);
                commandBuilderLogin = new SqlCommandBuilder(loginAdapter);
                commandBuilderQuestions = new SqlCommandBuilder(questionsAdapter);
                commandBuilderAnswers = new SqlCommandBuilder(answersAdapter);

                studentsAdapter.UpdateCommand = commandBuilderStudents.GetUpdateCommand();
                loginAdapter.UpdateCommand = commandBuilderLogin.GetUpdateCommand();
                questionsAdapter.UpdateCommand = commandBuilderQuestions.GetUpdateCommand();
                answersAdapter.UpdateCommand = commandBuilderAnswers.GetUpdateCommand();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
