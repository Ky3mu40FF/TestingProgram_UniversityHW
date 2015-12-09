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
    public partial class ServerMainUserAuth : Form
    {
        SqlConnectionStringBuilder connectionString;
        SelectServer_Form selectServer_Form;
        SqlConnection connection;
        DataTable tblDatabases;

        public ServerMainUserAuth()
        {
            InitializeComponent();
        }

        public ServerMainUserAuth(ref SqlConnectionStringBuilder connectionString, 
            SelectServer_Form selectServer_Form,
            ref DataTable tblDatabases)
        {
            InitializeComponent();
            Configuration();
            this.connectionString = connectionString;
            this.selectServer_Form = selectServer_Form;
            this.tblDatabases = tblDatabases;
        }

        /*-------------------- Делегаты --------------------*/

        private void logIn_Button_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString.UserID = name_TextBox.Text;
                connectionString.Password = password_TextBox.Text;
                connection = new SqlConnection(connectionString.ConnectionString);
                connection.Open();
                //tblDatabases = connection.GetSchema("Databases");
                MessageBox.Show("Подключение к серверу установлено!");
                this.Close();
            }
            catch (Exception ex)
            {
                connectionString.UserID = "";
                connectionString.Password = "";
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(connection.State != ConnectionState.Closed)
                    connection.Close();
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
            this.AcceptButton = connectToServer_Button;

            password_TextBox.UseSystemPasswordChar = true;

            connectToServer_Button.Click +=
                new EventHandler(logIn_Button_Click);
            back_Button.Click +=
                new EventHandler(back_Button_Click);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

    }
}
