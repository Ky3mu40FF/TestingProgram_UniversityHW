using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;



namespace Система_Тестирования
{
    public partial class Add_Student_Form : Form
    {
        /*-------------------- Переменные --------------------*/

        DataTable studentsTable;
        DataTable loginTable;
        SqlDataAdapter studentsAdapter;
        SqlDataAdapter loginAdapter;

        /*-------------------- Конструкторы --------------------*/

        public Add_Student_Form()
        {
            InitializeComponent();

            Configure();
        }

        public Add_Student_Form(ref DataTable students, 
                                ref DataTable login, 
                                SqlDataAdapter studentsAdapter, 
                                SqlDataAdapter loginAdapter)
        {
            InitializeComponent();

            this.studentsTable = students;
            this.loginTable = login;
            this.studentsAdapter = studentsAdapter;
            this.loginAdapter = loginAdapter;

            Configure();
        }

        /*-------------------- Делегаты --------------------*/

        private void Add_Student_Form_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Вы уверены, что хотите выйти?",
                "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Faculty_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cB = sender as ComboBox;
            speciality_ComboBox.Items.Clear();
            FillSpecialityComboBox(cB.SelectedItem.ToString());
            speciality_ComboBox.SelectedIndex = 0;
        }

        private void AddNewStudent_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 newLoginID = Int32.Parse(loginTable.Rows[loginTable.Rows.Count - 1]["LoginID"].ToString()) + 1;
                Int32 newStudentID = Int32.Parse(studentsTable.Rows[studentsTable.Rows.Count - 1]["StudentID"].ToString()) + 1;

                MD5 md5Hash = MD5.Create();
                String passMD5Hash = md5_Functions.GetMd5Hash(md5Hash, password_TextBox.Text);

                loginTable.Rows.Add(
                        new object[]
                        {
                        newLoginID,
                        nickname_TextBox.Text,
                        passMD5Hash,
                        "Student"
                        }
                    );

                studentsTable.Rows.Add(
                        new object[]
                        {
                        newStudentID,
                        lastName_TextBox.Text,
                        firstName_TextBox.Text,
                        patronymic_TextBox.Text,
                        faculty_ComboBox.SelectedItem,
                        speciality_ComboBox.SelectedItem,
                        course_ComboBox.SelectedItem,
                        0,  // IsTestPassed
                        0,  // Mark
                        0,   // NumOfRetries
                        newLoginID
                        }
                    );

                loginAdapter.Update(loginTable);
                studentsAdapter.Update(studentsTable);

                MessageBox.Show("Новый студент успешно добавлен!",
                    "Успех!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось добавить нового студента!\n\n"+ex.ToString(), 
                    "Ошибка!", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*-------------------- Остальные функции --------------------*/

        private void Configure()
        {
            this.AcceptButton = addNewStudent_Button;
            this.CancelButton = back_Button;

            this.FormClosing += new FormClosingEventHandler(Add_Student_Form_Closing);
            faculty_ComboBox.SelectedIndexChanged += new EventHandler(Faculty_ComboBox_SelectedIndexChanged);
            addNewStudent_Button.Click += new EventHandler(AddNewStudent_Button_Click);
            back_Button.Click += new EventHandler(Back_Button_Click);

            //faculty_ComboBox.SelectedIndex = 0;
        }

        private void FillSpecialityComboBox(String faculty)
        {
            String[] spec;
            switch (faculty)
            {
                case "ФОИСТ":
                    spec = new String[] { "ИБ", "Оптотехника", "ЛТиЛТ" };
                    break;
                case "ФПКиФ":
                    spec = new String[] { "ГиДЗ", "ИСиТ" };
                    break;
                case "ФКиГ":
                    spec = new String[] { "КиГ" };
                    break;
                default:
                    spec = new String[] { "ИБ", "Оптотехника", "ЛТиЛТ", "ГиДЗ", "ИСиТ", "КиГ" };
                    break;
            }
            speciality_ComboBox.Items.AddRange(spec);
        }
    }
}
