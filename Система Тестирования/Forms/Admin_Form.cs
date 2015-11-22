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

            dataGridView1.DataSource = commonStudentsTable;
        }

        private void Admin_Form_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Вы уверены, что хотите выйти?",
                "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /*-------------------- Остальные функции --------------------*/

        private void Configure()
        {
            this.Load += new EventHandler(Admin_Form_Load);
            this.FormClosing += new FormClosingEventHandler(Admin_Form_Closing);
        }

        private void GetAllNecessaryTables()
        {
            try
            {
                commonStudentsTable = new DataTable("Students");
                commonStudentsAdapter = new SqlDataAdapter(
                    "SELECT * "+
                    "FROM Students INNER JOIN Login ON Students.Students_LoginFK = Login.LoginID",
                    connectionString);
                commonStudentsAdapter.Fill(commonStudentsTable);
                commandBuilderCommonStudents = new SqlCommandBuilder(commonStudentsAdapter);
                commonStudentsAdapter.UpdateCommand = commandBuilderCommonStudents.GetUpdateCommand();

                /*
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
                */
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
