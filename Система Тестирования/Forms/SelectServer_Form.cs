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

namespace Система_Тестирования
{
    public partial class SelectServer_Form : Form
    {
        /*-------------------- Переменные --------------------*/

        SqlConnectionStringBuilder connectionString;
        DataTable serversTable;
        enum Status
        {
            Error,
            SearchingServers,
            SearchingDB,
            OK
        };
        String serverName;
        Server server;
        List<String> db_Names;

        DataTable tblDatabases;


        /*-------------------- Конструктор --------------------*/

        public SelectServer_Form()
        {
            InitializeComponent();

            Configuration();

            pictureBox1.Image = Properties.Resources.Loading;
            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;

        }


        /*-------------------- Делегаты --------------------*/

        private void SelectServer_Form_Loading(object sender, EventArgs e)
        {
            ChangeStatus(Status.SearchingServers);
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
                serversTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ChangeStatus(Status.Error);
                MessageBox.Show(e.Error.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ChangeStatus(Status.OK);
                ReadFoundedServers();
            }
            //MessageBox.Show("Поиск завершён!");
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bW = sender as BackgroundWorker;
                /*
                db_Names = new List<string>();
                foreach (Database database in server.Databases)
                {
                    //availableDB_ListBox.Items.Add(database.Name);
                    db_Names.Add(database.Name);
                }
                */
                db_Names = new List<string>();
                //foreach (Database database in tblDatabases.Rows)
                foreach (DataRow row in tblDatabases.Rows)
                {
                    //availableDB_ListBox.Items.Add(database.Name);
                    //db_Names.Add(database.Name);

                    //availableDB_ListBox.Items.Add(row[0].ToString());
                    db_Names.Add(row[0].ToString());

                    /*
                    String items = "";
                    for(Int32 i = 0; i < row.ItemArray.Length; i++)
                    {
                        items += " || " + i + " = " + row.ItemArray[i] + " || ";
                    }
                    MessageBox.Show(items);
                    */
                }

                if (bW.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bW = sender as BackgroundWorker;
            if (e.Error != null)
            {
                ChangeStatus(Status.Error);
                MessageBox.Show(e.Error.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e != null && e.Cancelled)
            {
                bW.RunWorkerAsync();
                return;
            }
            else
            {
                AddDataBasesNamesToList(db_Names);
                ChangeStatus(Status.OK);
            }
            //MessageBox.Show("Поиск завершён!");
        }

        private void availableServers_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                availableDB_ListBox.DataSource = null;

                // Очищаем ListBox, содержащий доступные БД на выбранном сервере
                availableDB_ListBox.Items.Clear();

                ChangeStatus(Status.SearchingDB);

                if (availableServers_ListBox.SelectedIndex != -1)
                {
                    // Т.к. при смене сервера придётся снова выбирать БД, то отключаем кнопку "Далее",
                    // пока пользователь снова не выберет нужную БД
                    next_Button.Enabled = false;

                    // Получаем имя сервера
                    serverName = availableServers_ListBox.SelectedItem.ToString();

                    // Создаём экземпляр Server и передаём ему имя выбранного сервера
                    server = new Server(serverName);

                    connectionString = new SqlConnectionStringBuilder();
                    connectionString.DataSource = serverName;
                    connectionString.UserID = "Testing_System_Login";
                    connectionString.Password = "1234";
                    connectionString.IntegratedSecurity = false;
                    String strConn = connectionString.ToString();
                    SqlConnection sqlConn = new SqlConnection(strConn);
                    sqlConn.Open();
                    tblDatabases = sqlConn.GetSchema("Databases");
                    sqlConn.Close();

                    ChangeStatus(Status.SearchingDB);
                    if (!backgroundWorker2.IsBusy)
                    {
                        backgroundWorker2.RunWorkerAsync();
                    }
                    else
                    {
                        backgroundWorker2.CancelAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void availableDB_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableDB_ListBox.SelectedIndex != -1)
            {
                next_Button.Enabled = true;
            }
        }

        private void next_Button_Click(object sender, EventArgs e)
        {
            //connectionString = new SqlConnectionStringBuilder();
            //connectionString.DataSource = availableServers_ListBox.SelectedItem.ToString();
            connectionString.InitialCatalog = availableDB_ListBox.SelectedItem.ToString();
            //connectionString.IntegratedSecurity = false;

            Authorization_Form authForm = new Authorization_Form(connectionString, this);
            this.Hide();
            authForm.Show();
        }

        private void refresh_Button_Click(object sender, EventArgs e)
        {
            ChangeStatus(Status.SearchingServers);
            availableServers_ListBox.Items.Clear();
            availableDB_ListBox.DataSource = null;
            availableDB_ListBox.Items.Clear();
            next_Button.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void exit_Button_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        /*-------------------- Остальные функции --------------------*/

        private void ReadFoundedServers()
        {
            availableServers_ListBox.ValueMember = "Name";
            foreach (DataRow row in serversTable.Rows)
            {
                availableServers_ListBox.Items.Add(string.Concat(row["ServerName"], "\\", row["InstanceName"]));
            }
        }

        private void AddDataBasesNamesToList(List<String> names)
        {
            //for (Int32 i = 0; i < names.Count; i++)
            //    availableDB_ListBox.Items.Add(names[i]);
            availableDB_ListBox.DataSource = names;
        }

        private void Configuration()
        {
            next_Button.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            this.Load += 
                new EventHandler(SelectServer_Form_Loading);
            backgroundWorker1.DoWork += 
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += 
                new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker2.DoWork +=
                new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            availableServers_ListBox.SelectedIndexChanged +=
                new EventHandler(availableServers_ListBox_SelectedIndexChanged);
            availableDB_ListBox.SelectedIndexChanged +=
                new EventHandler(availableDB_ListBox_SelectedIndexChanged);
            exit_Button.Click += 
                new EventHandler(exit_Button_Click);
            refresh_Button.Click +=
                new EventHandler(refresh_Button_Click);
            next_Button.Click +=
                new EventHandler(next_Button_Click);

            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ChangeStatus(Status status)
        {
            switch (status)
            {
                case Status.Error:
                    label1.Text = "Произошла ошибка!";
                    label1.ForeColor = Color.Red;
                    refresh_Button.Enabled = true;
                    pictureBox1.Visible = false;
                    pictureBox1.Enabled = false;
                    break;
                case Status.SearchingServers:
                    label1.Text = "Пожалуйста, подождите.\nИдёт поиск доступных серверов.";
                    label1.ForeColor = Color.Blue;
                    refresh_Button.Enabled = false;
                    pictureBox1.Visible = true;
                    pictureBox1.Enabled = true;
                    break;
                case Status.SearchingDB:
                    label1.Text = "Пожалуйста, подождите.\nИдёт поиск доступных баз данных.";
                    label1.ForeColor = Color.Blue;
                    refresh_Button.Enabled = false;
                    pictureBox1.Visible = true;
                    pictureBox1.Enabled = true;
                    break;
                case Status.OK:
                    label1.Text = "Поиск завершён!";
                    label1.ForeColor = Color.Green;
                    refresh_Button.Enabled = true;
                    pictureBox1.Visible = false;
                    pictureBox1.Enabled = false;
                    break;
            }
        }

    }
}
