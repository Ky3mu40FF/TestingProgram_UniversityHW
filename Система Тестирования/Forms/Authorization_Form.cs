using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace Система_Тестирования
{
    public partial class Authorization_Form : Form
    {
        /*-------------------- Переменные --------------------*/

        SqlConnectionStringBuilder connectionString;
        SqlConnection connection;
        String connectionStr;

        SelectServer_Form selectServer_Form;

        User currentUser;


        /*-------------------- Конструктор --------------------*/

        public Authorization_Form()
        {
            InitializeComponent();
        }

        public Authorization_Form(SqlConnectionStringBuilder con, SelectServer_Form selectServer_Form)
        {
            InitializeComponent();
            this.connectionString = con;
            this.selectServer_Form = selectServer_Form;
            Configuration();
        }

        public Authorization_Form(String con, SelectServer_Form selectServer_Form)
        {
            InitializeComponent();
            this.connectionStr = con;
            this.selectServer_Form = selectServer_Form;
            Configuration();
        }

        /*-------------------- Делегаты --------------------*/

        private void Authorization_Form_Closing(object sender, FormClosingEventArgs e)
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

        private void logIn_Button_Click(object sender, EventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connectionString.ConnectionString);
                connection = new SqlConnection(Properties.Settings.Default.ServerConnectionString);
                connection.Open();

                /*
                string sourcePassword = password_TextBox.Text;
                MD5 md5Hash = MD5.Create();
                string hash = md5_Functions.GetMd5Hash(md5Hash, sourcePassword);
                */

                SqlCommand cmd = new SqlCommand(
                    "SELECT * " +
                    "FROM Login " +
                    "WHERE " +
                    "Login.UserName = '" + name_TextBox.Text + "' " +
                    " AND " +
                    "Login.Password = '" + password_TextBox.Text + "'", 
                    //"Login.Password = '" + hash + "'",
                    connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Имя пользователя или Пароль были введены неверно!");
                    return;
                }
     // !!!!! Не уверен по поводу того, как правильно закрывать Reader и выходить из While
                while (reader.Read())
                {
                    if(reader["Role"].ToString() == "Teacher")
                    {
                        MessageBox.Show("Вы вошли как администратор!");
                        Admin_Form admin_Form = new Admin_Form(connection);
                        reader.Close();
                        admin_Form.ShowDialog();
                        this.Hide();
                        return;
                    }
                    else if (reader["Role"].ToString() == "Student")
                    {
                        MessageBox.Show("Вы вошли как студент!");
                        Student_Form student_Form = new Student_Form(connection, (Int32)reader["LoginID"]);
                        reader.Close();
                        student_Form.ShowDialog();
                        this.Hide();
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void back_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            selectServer_Form.Show();
        }


        /*-------------------- Остальные функции --------------------*/

        private void Configuration()
        {
            // Устанавливаем кнопку приёма введённых данных с формы
            // нажатие клавиши Enter эквивалентно нажатию этой кнопки
            // т.е. теперь при вводе данных можно сразу нажать Enter, не фокусируясь на кнопке
            // с помощью Tab или не выбирая её с помощью мыши
            this.AcceptButton = logIn_Button;

            password_TextBox.UseSystemPasswordChar = true;

            this.FormClosing +=
                new FormClosingEventHandler(Authorization_Form_Closing);
            logIn_Button.Click +=
                new EventHandler(logIn_Button_Click);
            back_Button.Click +=
                new EventHandler(back_Button_Click);

            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
