using System;
using System.Data;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Система_Тестирования
{
    public partial class Edit_Students_Form : Form
    {
        Admin_Form adminForm;

        DataTable studentsTable;
        DataTable loginTable;
        SqlDataAdapter studentsAdapter;
        SqlDataAdapter loginAdapter;

        SqlCommand lastIdFromTable;
        SqlDataReader reader;
        SqlConnection connection;

        Int32 rowIndex;
        Int32 lastIdInStudentsTable;
        Int32 lastIdInLoginTable;

        Int32[] foundedRowsIndexes;
        Int32 currentFoundedIndex;

        DataRow currentStudentsRow;
        DataRow currentLoginRow;

        TextBox[] dataTextBoxes;
        ComboBox[] dataComboBoxes;

        // Перечисление, указывающее режим работы второй формы 
        // (Редактирование записей, Поиск записей, Просмотр записей)
        enum WorkMode
        {
            Edit,
            Search,
            Preview
        }
        // Перечисление, указывающее режим редактирования
        // (Добавление строк, Обновление строк, Удаление строк)
        enum EditMode
        {
            Insert,
            Update,
            Delete
        }

        public Edit_Students_Form()
        {
            InitializeComponent();
        }
        // Новый конструктор
        public Edit_Students_Form(ref DataTable students,
                                ref DataTable login,
                                SqlDataAdapter studentsAdapter,
                                SqlDataAdapter loginAdapter,
                                Admin_Form adminForm,
                                SqlConnection connection)
        {
            InitializeComponent();

            this.studentsTable = students;
            this.loginTable = login;
            this.studentsAdapter = studentsAdapter;
            this.loginAdapter = loginAdapter;
            this.adminForm = adminForm;
            this.connection = connection;

            // Инициализация остальных компонентов
            MyComponentsInitialization();
            // Выбираем первую строку в таблице
            rowIndex = 0;
        }

        /*-------------------------ДЕЛЕГАТЫ ДЛЯ СОБЫТИЙ ЭЛЕМЕНТОВ ФОРМЫ-------------------------*/

        #region RadioButtons
        // ДОРАБОТАТЬ (Решить проблему с тем, чтобы GroupBox'ы для Редактирования и Поиска
        // при запуске формы сразу были неактивны, а не приходилось вешать код для этого на
        // пункт Просмотра)
        // RadioButton'ы, отвечающие за режим работы (Редактирование, Поиск, Просмотр)
        private void radioButton_WorkMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rB = sender as RadioButton;
            Properties.Settings.Default.SecondFormMode = Convert.ToInt32(rB.Tag);
            switch (Properties.Settings.Default.SecondFormMode)
            {
                case 0:
                    if (rB.Checked)
                    {
                        // Включаем GroupBox, отвечающий за режим редактирования
                        groupBox7.Enabled = true;
                        // Отмечаем RadioButton, отвечающий за добавление строки 
                        radioButton4.Checked = true;

                        Properties.Settings.Default.SecondFormMode = (Int32)WorkMode.Edit;
                        //ReadOnly_ReadWrite_TextBoxes(false);
                    }
                    else
                    {
                        // Отключаем GroupBox, отвечающий за режим редактирования
                        groupBox7.Enabled = false;
                        radioButton4.Checked = false; radioButton5.Checked = false; radioButton6.Checked = false;
                    }
                    break;
                case 1:
                    if (rB.Checked)
                    {
                        groupBox4.Enabled = true;
                        Properties.Settings.Default.SecondFormMode = (Int32)WorkMode.Search;
                        //ReadOnly_ReadWrite_TextBoxes(false);
                    }
                    else
                    {
                        groupBox4.Enabled = false;
                    }
                    break;
                case 2:
                    if (rB.Checked)
                    {
                        groupBox3.Enabled = false; groupBox5.Enabled = false; groupBox6.Enabled = false;
                        groupBox7.Enabled = false;
                        radioButton4.Checked = false; radioButton5.Checked = false; radioButton6.Checked = false;
                        groupBox4.Enabled = false;
                        Properties.Settings.Default.SecondFormMode = (Int32)WorkMode.Preview;
                        OutputDataToForm(rowIndex);
                        ReadOnly_ReadWrite_TextBoxes(true);
                    }
                    else
                    {
                        ReadOnly_ReadWrite_TextBoxes(false);
                    }
                    break;
                default:
                    groupBox3.Enabled = false; groupBox5.Enabled = false; groupBox6.Enabled = false;
                    groupBox7.Enabled = false;
                    radioButton4.Checked = false; radioButton5.Checked = false; radioButton6.Checked = false;
                    groupBox4.Enabled = false;

                    Properties.Settings.Default.SecondFormMode = (Int32)WorkMode.Preview;
                    ReadOnly_ReadWrite_TextBoxes(true);
                    break;
            }
        }

        // ДОДЕЛАТЬ!!!
        // RadioButton'ы, отвечающие за режим редактирования (Добавление, Обновление, Удаление)
        private void radioButton_EditMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rB = sender as RadioButton;
            Properties.Settings.Default.EditMode = Convert.ToInt32(rB.Tag);
            switch (Properties.Settings.Default.EditMode)
            {
                case 0:
                    if (rB.Checked)
                    {
                        groupBox5.Enabled = true;
                        button1.Enabled = false;
                        button6.Enabled = false;
                        Properties.Settings.Default.EditMode = (Int32)EditMode.Insert;
                    }
                    else
                    {
                        groupBox5.Enabled = false;
                    }
                    break;
                case 1:
                    if (rB.Checked)
                    {
                        groupBox6.Enabled = true;
                        Properties.Settings.Default.EditMode = (Int32)EditMode.Update;
                    }
                    else
                    {
                        groupBox6.Enabled = false;
                    }
                    break;
                case 2:
                    if (rB.Checked)
                    {
                        groupBox3.Enabled = true;
                        Properties.Settings.Default.EditMode = (Int32)EditMode.Delete;
                    }
                    else
                    {
                        groupBox3.Enabled = false;
                    }
                    break;
                default:
                    groupBox5.Enabled = false; groupBox6.Enabled = false; groupBox3.Enabled = false;
                    Properties.Settings.Default.EditMode = (Int32)EditMode.Insert;
                    break;
            }
        }

        #endregion

        #region Buttons
        // Начинает процесс добавления строки
        private void button_StartInsertingRow_Click(object sender, EventArgs e)
        {
            try
            {
                lastIdInStudentsTable = studentsTable.AsEnumerable()
                    .Select(x => x.Field<Int32>("StudentsID"))
                    .DefaultIfEmpty(1)
                    .Max(x => x);
                lastIdInLoginTable = loginTable.AsEnumerable()
                    .Select(x => x.Field<Int32>("LoginID"))
                    .DefaultIfEmpty(1)
                    .Max(x => x);

                // Очищаем все поля
                ClearDataInForm();
                // Получаем кнопку, вызвавшую событие
                Button thisBtn = sender as Button;
                // Деактивируем эту кнопку
                thisBtn.Enabled = false;
                // Активируем кнопку завершения процесса добавления строки
                button1.Enabled = true;
                // Активируем кнопку отмены процесса добавления строки
                button6.Enabled = true;
                // В настройки записываем то, что мы начали добавлять строку
                Properties.Settings.Default.IsInsertingStarted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // Отмена процесса добавления строки
        private void button_CancelInsertingRow_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Процес добавления нового студента отменён!", "Внимание!", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Получаем кнопку, вызвавшую событие
            Button thisBtn = sender as Button;
            // Деактивируем эту кнопку
            thisBtn.Enabled = false;
            // Деактивируем кнопку Завершения процесса добавления строки
            button1.Enabled = false;
            // Активируем кнопку завершения процесса добавления строки
            button5.Enabled = true;
            // В настройки записываем то, что мы закончили добавлять строку
            Properties.Settings.Default.IsInsertingStarted = false;
            // Снова выводим на форму предыдущую запись
            OutputDataToForm(rowIndex);
        }
        // Завершает процесс добавления строки
        private void button_EndInsertingRow_Click(object sender, EventArgs e)
        {
            try
            {
                loginTable.Rows.Add(
                        new object[]
                        {
                        lastIdInLoginTable + 1,
                        nickname_TextBox.Text,
                        password_TextBox.Text,
                        "Student"
                        }
                    );

                studentsTable.Rows.Add(
                        new object[]
                        {
                        lastIdInStudentsTable + 1,
                        lastName_TextBox.Text,
                        firstName_TextBox.Text,
                        patronymic_TextBox.Text,
                        faculty_ComboBox.SelectedItem,
                        speciality_ComboBox.SelectedItem,
                        course_ComboBox.SelectedItem,
                        0,  // IsTestPassed
                        0,  // Mark
                        0,   // NumOfRetries
                        lastIdInLoginTable + 1
                        }
                    );

                loginAdapter.Update(loginTable);
                studentsAdapter.Update(studentsTable);

                MessageBox.Show("Новый студент успешно добавлен!",
                    "Успех!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);

                // Обновляем Label в ToolStip, показывающий количество строк в таблице
                toolStripLabel1.Text = "/" + studentsTable.Rows.Count.ToString();
                // Получаем кнопку, вызвавшую событие
                Button thisBtn = sender as Button;
                // Деактивируем эту кнопку
                thisBtn.Enabled = false;
                // Деактивируем кнопку Отмены процесса добавления строки
                button6.Enabled = false;
                // Активируем кнопку завершения процесса добавления строки
                button5.Enabled = true;
                // В настройки записываем то, что мы закончили добавлять строку
                Properties.Settings.Default.IsInsertingStarted = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить строку.\n" + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button_UpdateRow_Click(object sender, EventArgs e)
        {
            try
            {
                loginTable.Rows[rowIndex].ItemArray = new object[]
                        {
                        loginTable.Rows[rowIndex]["LoginID"],   // Остаётся, как было
                        nickname_TextBox.Text,
                        password_TextBox.Text,
                        "Student"   // Остаётся, как было
                        };

                studentsTable.Rows[rowIndex].ItemArray = new object[]
                        {
                        studentsTable.Rows[rowIndex]["StudentsID"],         // Остаётся, как было
                        lastName_TextBox.Text,
                        firstName_TextBox.Text,
                        patronymic_TextBox.Text,
                        faculty_ComboBox.SelectedItem,
                        speciality_ComboBox.SelectedItem,
                        course_ComboBox.SelectedItem,
                        studentsTable.Rows[rowIndex]["IsTestPassed"],       // Остаётся, как было
                        studentsTable.Rows[rowIndex]["Mark"],               // Остаётся, как было
                        studentsTable.Rows[rowIndex]["NumOfRetries"],       // Остаётся, как было
                        studentsTable.Rows[rowIndex]["Students_LoginFK"]    // Остаётся, как было
                        };

                loginAdapter.Update(loginTable);
                studentsAdapter.Update(studentsTable);

                MessageBox.Show("Информация о студенте обновлена!",
                    "Успех!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить строку.\n" + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                loginTable.Rows[rowIndex].Delete();
                studentsTable.Rows[rowIndex].Delete();
                loginAdapter.Update(loginTable);
                studentsAdapter.Update(studentsTable);

                MessageBox.Show("Студент удалён из базы данных!", 
                    "Успех!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                toolStripLabel1.Text = "/" + studentsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить студента из базы данных.\n" + ex, 
                    "Ошибка!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void button_StartSearch_Click(object sender, EventArgs e)
        {
            String conditions = "";

            foreach (TextBox tB in dataTextBoxes)
            {
                String columnName = tB.Tag.ToString().Substring(tB.Tag.ToString().LastIndexOf('.') + 1);
                if (tB.Text == null || tB.Text == "")
                    continue;
                if (studentsTable.Columns[columnName].DataType.ToString() == "System.String" 
                    | 
                    studentsTable.Columns[columnName].DataType.ToString() == "System.Char")
                    conditions += tB.Tag.ToString() + " LIKE '" + tB.Text + "'" + " AND ";
                // В других случаях оставляем как есть и сравниваем через "="
                else
                    conditions += tB.Tag.ToString() + " = " + tB.Text.Replace(',', '.') + " AND ";
            }
            foreach (ComboBox cB in dataComboBoxes)
            {
                String columnName = cB.Tag.ToString().Substring(cB.Tag.ToString().LastIndexOf('.') + 1);
                if (cB.Text == null || cB.Text == "")
                    continue;
                if (studentsTable.Columns[columnName].DataType.ToString() == "System.String"
                    |
                    studentsTable.Columns[columnName].DataType.ToString() == "System.Char")
                    conditions += cB.Tag.ToString() + " LIKE '" + cB.Text + "'" + " AND ";
                // В других случаях оставляем как есть и сравниваем через "="
                else
                    conditions += cB.Tag.ToString() + " = " + cB.Text.Replace(',', '.') + " AND ";
            }

            // Символы, которые нужно удалить с конца строки-условия ("AND ")
            Char[] charsToTrim = { 'A', 'N', 'D', ' ' };
            // Удаляем из конца ("AND ")
            conditions = conditions.TrimEnd(charsToTrim);

            DataTable searchResultTable = new DataTable();
            SqlCommand searchCommand = new SqlCommand(
                "SELECT "+ "Students.StudentsID, Students.LastName, Students.FirstName, Students.Patronymic, Students.Faculty, " +
                        "Students.Speciality, Students.Course, Login.Username, Login.Password" +
                " FROM Students INNER JOIN Login ON Students.Students_LoginFK = Login.LoginID " +
                " WHERE " + conditions, connection);
            SqlDataAdapter searchResultAdapter = new SqlDataAdapter(searchCommand.CommandText, connection);

            searchResultAdapter.Fill(searchResultTable);
            // Следующие 3 строки:
            // В таблице указывается поле, которое является первичным ключом 
            DataColumn[] keyColumn = new DataColumn[1];
            keyColumn[0] = studentsTable.Columns[studentsTable.TableName + "ID"];
            studentsTable.PrimaryKey = keyColumn;

            foundedRowsIndexes = new Int32[searchResultTable.Rows.Count];
            for (Int32 i = 0; i < searchResultTable.Rows.Count; i++)
                foundedRowsIndexes[i] = studentsTable.Rows.IndexOf(studentsTable.Rows.Find(searchResultTable.Rows[i][0]));
            // Записываем в настройки, что мы в режиме поиска 
            // (нужно, чтобы с помощью ToolStrip можно было перемещаться по результатам поиска)
            Properties.Settings.Default.IsSearching = true;
            currentFoundedIndex = 0;

            Button thisButton = sender as Button;
            thisButton.Enabled = false;
            button7.Enabled = true;

            MessageBox.Show("Найдено записей: " + foundedRowsIndexes.Length.ToString());

            rowIndex = foundedRowsIndexes[currentFoundedIndex];
            OutputDataToForm(rowIndex);
        }

        private void button_StopSearch_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSearching = false;
            Button thisButton = sender as Button;
            thisButton.Enabled = false;
            button4.Enabled = true;
        }

        #endregion

        #region ToolStrip elements
        // ToolStrip-элементы
        // Нажатие на кнопку перехода К первой записи
        private void toolStrip_ToStart_Button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.IsSearching)
            {
                rowIndex = 0;
                OutputDataToForm(rowIndex);
            }
            else
            {
                //rowIndex = foundedRowsIndexes[0];
                currentFoundedIndex = 0;
                rowIndex = foundedRowsIndexes[currentFoundedIndex];
                OutputDataToForm(rowIndex);
            }
        }
        // Нажатие на кнопку перехода К предыдущей записи 
        private void toolStrip_Previous_Button_Click(object sender, EventArgs e)
        {
            if(!Properties.Settings.Default.IsSearching)
                OutputDataToForm(--rowIndex);
            else
            {
                if (currentFoundedIndex != 0)
                {
                    rowIndex = foundedRowsIndexes[--currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                }
                else
                {
                    rowIndex = foundedRowsIndexes[foundedRowsIndexes.Length - 1];
                    OutputDataToForm(rowIndex);
                }
            }
        }
        // Нажатие на кнопку перехода К следующей записи 
        private void toolStrip_Next_Button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.IsSearching)
                OutputDataToForm(++rowIndex);
            else
            {
                if (currentFoundedIndex != foundedRowsIndexes.Length - 1)
                {
                    rowIndex = foundedRowsIndexes[++currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                }
                else
                {
                    rowIndex = foundedRowsIndexes[0];
                    OutputDataToForm(rowIndex);
                }
            }
        }
        // Нажатие на кнопку перехода К последней записи 
        private void toolStrip_ToEnd_Button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.IsSearching)
            {
                rowIndex = studentsTable.Rows.Count - 1;
                OutputDataToForm(rowIndex);
            }
            else
            {
                //rowIndex = foundedRowsIndexes.Length - 1;
                currentFoundedIndex = foundedRowsIndexes.Length - 1;
                rowIndex = foundedRowsIndexes[currentFoundedIndex];
                OutputDataToForm(rowIndex);
            }
        }
        // При изменении свойства Text элемента toolStripTextBox1 (отображается номер текущей записи)
        private void toolStrip_TextBox_TextChanged(object sender, EventArgs e)
        {
            // Проверяем, не вышли ли мы за границу. Если вышли, то исправляем
            ToolStripTextBox tSTB = sender as ToolStripTextBox;
            if (tSTB.Text == null || tSTB.Text == "")
                tSTB.Text = "1";
            if (Int32.Parse(tSTB.Text) > studentsTable.Rows.Count)
                tSTB.Text = studentsTable.Rows.Count.ToString();

            // Проверяем граничные значения, чтобы включать/отключать кнопки
            // перехода к предыдущей/следующей записи (чтобы не выйти за пределы массива записей)
            if (Int32.Parse(tSTB.Text) == 1)
            {
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = true;
            }
            else if (Int32.Parse(tSTB.Text) == studentsTable.Rows.Count)
            {
                toolStripButton3.Enabled = false;
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = true;
            }
        }
        // Проверка нажатой клавиши. Пропускать только цифры и Backspace
        private void toolStrip_TextBox_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                // Цифры можно
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace можно
            }
            // Если нажали Enter, то перейти на записанную строку
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                rowIndex = Int32.Parse(toolStripTextBox1.Text) - 1;
                OutputDataToForm(rowIndex);
            }
            else
            {
                // Swallow this invalid key
                // Все остальные клавиши не пропускать
                e.Handled = true;
            }
        }

        #endregion

        #region Form

        // При закрытии формы вывести сообщение о необходимости вручную сохранить изменения
        private void Form_OnClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.IsInsertingStarted)
            {
                MessageBox.Show("Внимание! Вы начали процесс добавления строки, но не завершили его.\n"+
                    "Пожалуйста, завершите или отмените процесс добавления строки!", 
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                adminForm.Show();
            }
        }

        #endregion Form

        /*-------------------------ОТДЕЛЬНЫЕ ФУНКЦИИ-------------------------*/

        // Выводит значения полей указанной строки на форму
        private void OutputDataToForm(Int32 rowIndex)
        {
            currentStudentsRow = studentsTable.Rows[rowIndex];
            Int32 loginFK = Int32.Parse(studentsTable.Rows[rowIndex]["Students_LoginFK"].ToString());
            //currentLoginRow = loginTable.Rows[loginFK];
            currentLoginRow = loginTable.AsEnumerable()
                .Single<DataRow>(row => row.Field<Int32>("LoginID") == loginFK);

            lastName_TextBox.Text = currentStudentsRow["LastName"].ToString();
            firstName_TextBox.Text = currentStudentsRow["FirstName"].ToString();
            patronymic_TextBox.Text = currentStudentsRow["Patronymic"].ToString();
            faculty_ComboBox.SelectedItem = currentStudentsRow["Faculty"].ToString();
            speciality_ComboBox.SelectedItem = currentStudentsRow["Speciality"].ToString();
            course_ComboBox.SelectedItem = currentStudentsRow["Course"].ToString();
            nickname_TextBox.Text = currentLoginRow["Username"].ToString();
            password_TextBox.Text = currentLoginRow["Password"].ToString();

            toolStripTextBox1.Text = (rowIndex + 1).ToString();
        }

        private void ClearDataInForm()
        {
            lastName_TextBox.Text = "";
            firstName_TextBox.Text = "";
            patronymic_TextBox.Text = "";

            faculty_ComboBox.ResetText();
            speciality_ComboBox.ResetText();
            course_ComboBox.ResetText();
            faculty_ComboBox.SelectedIndex = -1;
            speciality_ComboBox.SelectedIndex = -1;
            course_ComboBox.SelectedIndex = -1;

            nickname_TextBox.Text = "";
            password_TextBox.Text = "";
        }

        // Меняет режим чтения/записи для TextBox'ов, в которых обрабатываются значения текущей строки
        // true - ReadOnly,
        // false - Read and Write
        private void ReadOnly_ReadWrite_TextBoxes(bool state)
        {
            lastName_TextBox.ReadOnly = state;
            firstName_TextBox.ReadOnly = state;
            patronymic_TextBox.ReadOnly = state;
            faculty_ComboBox.Enabled = !state;
            speciality_ComboBox.Enabled = !state;
            course_ComboBox.Enabled = !state;
            nickname_TextBox.ReadOnly = state;
            password_TextBox.ReadOnly = state;
        }

        
        // Настройка остальных элементов формы (включая саму форму)
        // Название функции, наверное, лучше будет изменить
        private void MyComponentsInitialization()
        {
            lastName_TextBox.Tag = "Students.LastName";
            firstName_TextBox.Tag = "Students.FirstName";
            patronymic_TextBox.Tag = "Students.Patronymic";
            faculty_ComboBox.Tag = "Students.Faculty";
            speciality_ComboBox.Tag = "Students.Speciality";
            course_ComboBox.Tag = "Students.Course";
            nickname_TextBox.Tag = "Login.Username";
            password_TextBox.Tag = "Login.Password";
            dataTextBoxes = new TextBox[] { lastName_TextBox, firstName_TextBox, patronymic_TextBox};
            dataComboBoxes = new ComboBox[] { faculty_ComboBox, speciality_ComboBox, course_ComboBox};

            // Устанавливаем границы изменения размеров формы (ширина постоянная, а высота меняется)
            this.MinimumSize = new Size(this.Width, 500);
            this.MaximumSize = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height);

            // Записываем в Tag всех radioButton параметры, определяющие режим работы
            radioButton1.Tag = WorkMode.Edit;
            radioButton2.Tag = WorkMode.Search;
            radioButton3.Tag = WorkMode.Preview;
            // Записываем в Tag всех radioButton параметры, определяющие режим редактирования
            radioButton4.Tag = EditMode.Insert;
            radioButton5.Tag = EditMode.Update;
            radioButton6.Tag = EditMode.Delete;

            // Добавляем делегаты для события, когда состояние radioButton меняется (помечаем один из них)
            // RadioButton'ы, отвечающие за режим работы формы (Редактирование, Поиск, Просмотр)
            radioButton1.CheckedChanged += new EventHandler(radioButton_WorkMode_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButton_WorkMode_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(radioButton_WorkMode_CheckedChanged);
            // RadioButton'ы, отвечающие за режим редактирования (Добавление, Обновление, Удаление)
            radioButton4.CheckedChanged += new EventHandler(radioButton_EditMode_CheckedChanged);
            radioButton5.CheckedChanged += new EventHandler(radioButton_EditMode_CheckedChanged);
            radioButton6.CheckedChanged += new EventHandler(radioButton_EditMode_CheckedChanged);

            // Активируем radioButton "Просмотр"
            radioButton3.Checked = true;

            // Добавляем делегаты для события нажатия на кнопку
            button5.Click += new EventHandler(button_StartInsertingRow_Click);
            button6.Click += new EventHandler(button_CancelInsertingRow_Click);
            button1.Click += new EventHandler(button_EndInsertingRow_Click);
            button2.Click += new EventHandler(button_UpdateRow_Click);
            button3.Click += new EventHandler(button_DeleteRow_Click);
            button4.Click += new EventHandler(button_StartSearch_Click);
            button7.Click += new EventHandler(button_StopSearch_Click);
            toolStripButton1.Click += new EventHandler(toolStrip_ToStart_Button_Click);
            toolStripButton2.Click += new EventHandler(toolStrip_Previous_Button_Click);
            toolStripButton3.Click += new EventHandler(toolStrip_Next_Button_Click);
            toolStripButton4.Click += new EventHandler(toolStrip_ToEnd_Button_Click);

            // Выводим общее количество строк в таблице в элемент toolStripLabel1
            toolStripLabel1.Text = "/" + studentsTable.Rows.Count;
            // Добавляем делегаты для события нажатия на клавишу клавиатуры и изменения свйоства Text
            toolStripTextBox1.KeyPress += new KeyPressEventHandler(toolStrip_TextBox_OnKeyPress);
            toolStripTextBox1.TextChanged += new EventHandler(toolStrip_TextBox_TextChanged);
            // Деактивируем кнопку "К предыдущей записи", т.к. изначально выбирается первая строка 
            // и нельзя допустить выхода за границы массива
            toolStripButton2.Enabled = false;
            // Добавляем подсказки к кнопкам в элементе ToolStrip1
            toolStripButton1.ToolTipText = "К первой записи";
            toolStripButton2.ToolTipText = "К предыдущей записи";
            toolStripButton3.ToolTipText = "К следующей записи";
            toolStripButton4.ToolTipText = "К последней записи";

            // Деактивируем кнопку "Закончить поиск"
            button7.Enabled = false;

            // Добавляем делегат для события закрытия формы (закрытие ещё не закончилось)
            this.FormClosing += new FormClosingEventHandler(Form_OnClosing);
        }
    }
}