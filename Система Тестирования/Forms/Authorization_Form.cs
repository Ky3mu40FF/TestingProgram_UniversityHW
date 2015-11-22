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
                // Это пригодится для проверки пароля с помощью MD5 хэша
                /* 
                string source = "Hello World!";
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, source);

                    if (VerifyMd5Hash(md5Hash, source, hash))
                    {
                        Console.WriteLine("The hashes are the same.");
                    }
                    else
                    {
                        Console.WriteLine("The hashes are not same.");
                    }
                }
                */

                connection = new SqlConnection(connectionString.ConnectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * " +
                    "FROM Login " +
                    "WHERE " +
                    "Login.UserName = '" + name_TextBox.Text + "' " +
                    " AND " +
                    "Login.Password = '" + password_TextBox.Text + "'", 
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
                        Admin_Form admin_Form = new Admin_Form();
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
            password_TextBox.UseSystemPasswordChar = true;

            logIn_Button.Click +=
                new EventHandler(logIn_Button_Click);
            back_Button.Click +=
                new EventHandler(back_Button_Click);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
