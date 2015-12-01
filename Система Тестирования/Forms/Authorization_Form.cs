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


        /*-------------------- Делегаты --------------------*/
        /*
        private void logIn_Button_Click(object sender, EventArgs e)
        {
            connectionString.UserID = name_TextBox.Text;
            connectionString.Password = password_TextBox.Text;
            Server server = new Server(connectionString.DataSource);
            Database dB = new Database(server, connectionString.InitialCatalog.ToString());
            try
            {                
                connection = new SqlConnection(connectionString.ConnectionString);
                connection.Open();
                currentUser = new User(dB, connectionString.UserID);
                
                //MessageBox.Show(currentUser.EnumRoles()[0].ToString());
                //MessageBox.Show("Вы вошли в систему!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if(currentUser.EnumRoles()[0] == "db_owner")
                {
                    MessageBox.Show("Вы вошли как администратор!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Admin_Form admin_Form = new Admin_Form();
                    this.Hide();
                    admin_Form.Show();
                    //selectServer_Form.Close();
                    //this.Close();
                    
                }
                else if (currentUser.EnumRoles()[0] == "db_datawriter")
                {
                    MessageBox.Show("Вы вошли как студент!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        private void logIn_Button_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString.ConnectionString);
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
                        admin_Form.Show();
                        this.Hide();
                        return;
                    }
                    else if (reader["Role"].ToString() == "Student")
                    {
                        MessageBox.Show("Вы вошли как студент!");
                        Student_Form student_Form = new Student_Form(connection, (Int32)reader["LoginID"]);
                        reader.Close();
                        student_Form.Show();
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

            logIn_Button.Click +=
                new EventHandler(logIn_Button_Click);
            back_Button.Click +=
                new EventHandler(back_Button_Click);

            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
