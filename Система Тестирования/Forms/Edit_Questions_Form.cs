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
    public partial class Edit_Questions_Form : Form
    {
        Admin_Form adminForm;

        DataTable questionsTable;
        DataTable answersTable;
        SqlDataAdapter questionsAdapter;
        SqlDataAdapter answersAdapter;

        SqlCommand lastIdFromTable;
        SqlDataReader reader;
        SqlConnection connection;

        TextBox[] answersTextBoxes;

        Int32 rowIndex;
        Int32 lastIdInQuestionsTable;
        Int32 lastIdInAnswersTable;

        Int32[] foundedRowsIndexes;
        Int32 currentFoundedIndex;

        DataRow currentQuestionRow;
        DataRow currentAnswersRow;

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

        public Edit_Questions_Form()
        {
            InitializeComponent();
        }
        // Новый конструктор
        public Edit_Questions_Form(ref DataTable questions,
                                ref DataTable answers,
                                SqlDataAdapter questionsAdapter,
                                SqlDataAdapter answersAdapter,
                                Admin_Form adminForm,
                                SqlConnection connection)
        {
            InitializeComponent();

            this.questionsTable = questions;
            this.answersTable = answers;
            this.questionsAdapter = questionsAdapter;
            this.answersAdapter = answersAdapter;
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
                lastIdInQuestionsTable = questionsTable.AsEnumerable()
                    .Select(x => x.Field<Int32>("QuestionsID"))
                    .DefaultIfEmpty(1)
                    .Max(x => x);
                lastIdInAnswersTable = answersTable.AsEnumerable()
                    .Select(x => x.Field<Int32>("AnswersID"))
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
                answersTable.Rows.Add(
                        new object[]
                        {
                            lastIdInAnswersTable + 1,
                            answer_1_TextBox.Text,
                            answer_2_TextBox.Text,
                            answer_3_TextBox.Text,
                            answer_4_TextBox.Text,
                            answer_5_TextBox.Text,
                            answer_6_TextBox.Text,
                            answer_7_TextBox.Text,
                            answer_8_TextBox.Text
            }
                    );

                questionsTable.Rows.Add(
                        new object[]
                        {
                            lastIdInQuestionsTable + 1,
                            lastIdInQuestionsTable + 1,
                            questionContent_TextBox.Text,
                            questionType_ComboBox.SelectedItem.ToString(),
                            answersCount_ComboBox.SelectedItem.ToString(),
                            correctAnswer_TextBox.Text,
                            lastIdInAnswersTable + 1
                        }
                    );

                answersAdapter.Update(answersTable);
                questionsAdapter.Update(questionsTable);

                MessageBox.Show("Новый вопрос успешно добавлен!",
                    "Успех!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);

                // Обновляем Label в ToolStip, показывающий количество строк в таблице
                toolStripLabel1.Text = "/" + questionsTable.Rows.Count.ToString();
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
                MessageBox.Show("Не удалось добавить вопрос.\n" + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button_UpdateRow_Click(object sender, EventArgs e)
        {
            try
            {
                answersTable.Rows[rowIndex].ItemArray =
                        new object[]
                        {
                            answersTable.Rows[rowIndex]["AnswersID"],   // Оставляем как есть
                            answer_1_TextBox.Text,
                            answer_2_TextBox.Text,
                            answer_3_TextBox.Text,
                            answer_4_TextBox.Text,
                            answer_5_TextBox.Text,
                            answer_6_TextBox.Text,
                            answer_7_TextBox.Text,
                            answer_8_TextBox.Text
                        };

                questionsTable.Rows[rowIndex].ItemArray =
                        new object[]
                        {
                            questionsTable.Rows[rowIndex]["QuestionsID"],   // Оставляем как есть
                            questionsTable.Rows[rowIndex]["QuestionsID"],   // Оставляем как есть
                            questionContent_TextBox.Text,
                            questionType_ComboBox.SelectedItem.ToString(),
                            answersCount_ComboBox.SelectedItem.ToString(),
                            correctAnswer_TextBox.Text,
                            answersTable.Rows[rowIndex]["AnswersID"]    // Оставляем как есть
                        };

                answersAdapter.Update(answersTable);
                questionsAdapter.Update(questionsTable);

                MessageBox.Show("Вопрос обновлён!",
                    "Успех!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить вопрос.\n" + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                answersTable.Rows[rowIndex].Delete();
                questionsTable.Rows[rowIndex].Delete();
                questionsAdapter.Update(questionsTable);
                answersAdapter.Update(answersTable);

                MessageBox.Show("Вопрос удалён из базы данных!", 
                    "Успех!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                toolStripLabel1.Text = "/" + questionsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить вопрос из базы данных.\n" + ex, 
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
                String tableName = tB.Tag.ToString().Substring(0, tB.Tag.ToString().IndexOf('.'));
                String columnName = tB.Tag.ToString().Substring(tB.Tag.ToString().LastIndexOf('.') + 1);
                if (tB.Text == null || tB.Text == "")
                    continue;
                if (tableName == "Questions")
                {
                    if (questionsTable.Columns[columnName].DataType.ToString() == "System.String"
                        |
                        questionsTable.Columns[columnName].DataType.ToString() == "System.Char")
                        conditions += tB.Tag.ToString() + " LIKE '" + tB.Text + "'" + " AND ";
                    // В других случаях оставляем как есть и сравниваем через "="
                    else
                        conditions += tB.Tag.ToString() + " = " + tB.Text.Replace(',', '.') + " AND ";
                }
                else if (tableName == "Answers")
                {
                    {
                        if (answersTable.Columns[columnName].DataType.ToString() == "System.String"
                            |
                            answersTable.Columns[columnName].DataType.ToString() == "System.Char")
                            conditions += tB.Tag.ToString() + " LIKE '" + tB.Text + "'" + " AND ";
                        // В других случаях оставляем как есть и сравниваем через "="
                        else
                            conditions += tB.Tag.ToString() + " = " + tB.Text.Replace(',', '.') + " AND ";
                    }
                }
            }
            foreach (ComboBox cB in dataComboBoxes)
            {
                String tableName = cB.Tag.ToString().Substring(0, cB.Tag.ToString().IndexOf('.'));
                String columnName = cB.Tag.ToString().Substring(cB.Tag.ToString().LastIndexOf('.') + 1);
                if (cB.Text == null || cB.Text == "")
                    continue;
                if (tableName == "Questions")
                {
                    if (questionsTable.Columns[columnName].DataType.ToString() == "System.String"
                        |
                        questionsTable.Columns[columnName].DataType.ToString() == "System.Char")
                        conditions += cB.Tag.ToString() + " LIKE '" + cB.Text + "'" + " AND ";
                    // В других случаях оставляем как есть и сравниваем через "="
                    else
                        conditions += cB.Tag.ToString() + " = " + cB.Text.Replace(',', '.') + " AND ";
                }
                else if (tableName == "Answers")
                {
                    {
                        if (answersTable.Columns[columnName].DataType.ToString() == "System.String"
                            |
                            answersTable.Columns[columnName].DataType.ToString() == "System.Char")
                            conditions += cB.Tag.ToString() + " LIKE '" + cB.Text + "'" + " AND ";
                        // В других случаях оставляем как есть и сравниваем через "="
                        else
                            conditions += cB.Tag.ToString() + " = " + cB.Text.Replace(',', '.') + " AND ";
                    }
                }
            }

            // Символы, которые нужно удалить с конца строки-условия ("AND ")
            Char[] charsToTrim = { 'A', 'N', 'D', ' ' };
            // Удаляем из конца ("AND ")
            conditions = conditions.TrimEnd(charsToTrim);

            DataTable searchResultTable = new DataTable();
            SqlCommand searchCommand = new SqlCommand(
                "SELECT Questions.QuestionsID, Questions.QuestionContent, Questions.QuestionType, " +
                        "Questions.CorrectAnswer, " +
                        "Answers.Answer_1, Answers.Answer_2, Answers.Answer_3, Answers.Answer_4, " +
                        "Answers.Answer_5, Answers.Answer_6, Answers.Answer_7, Answers.Answer_8 " +
                " FROM Questions INNER JOIN Answers ON Questions.Questions_AnswersFK = Answers.AnswersID" +
                " WHERE " + conditions, connection);
            SqlDataAdapter searchResultAdapter = new SqlDataAdapter(searchCommand.CommandText, connection);

            searchResultAdapter.Fill(searchResultTable);
            // Следующие 3 строки:
            // В таблице указывается поле, которое является первичным ключом 
            DataColumn[] keyColumn = new DataColumn[1];
            keyColumn[0] = questionsTable.Columns[questionsTable.TableName + "ID"];
            questionsTable.PrimaryKey = keyColumn;

            foundedRowsIndexes = new Int32[searchResultTable.Rows.Count];
            for (Int32 i = 0; i < searchResultTable.Rows.Count; i++)
                foundedRowsIndexes[i] = questionsTable.Rows.IndexOf(questionsTable.Rows.Find(searchResultTable.Rows[i][0]));
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
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = false;
            }
            else
            {
                //rowIndex = foundedRowsIndexes[0];
                currentFoundedIndex = 0;
                rowIndex = foundedRowsIndexes[currentFoundedIndex];
                OutputDataToForm(rowIndex);
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = false;
            }
        }
        // Нажатие на кнопку перехода К предыдущей записи 
        private void toolStrip_Previous_Button_Click(object sender, EventArgs e)
        {
            ToolStripButton thisTSB = sender as ToolStripButton;
            ToolStripButton tSB = sender as ToolStripButton;
            if (!Properties.Settings.Default.IsSearching)
            { 
                if (rowIndex == 1)
                {
                    OutputDataToForm(--rowIndex);
                    toolStripButton3.Enabled = true;
                    thisTSB.Enabled = false;
                }
                else if(rowIndex != 0)
                {
                    OutputDataToForm(--rowIndex);
                    toolStripButton3.Enabled = true;
                }
                
                else
                {
                    thisTSB.Enabled = false;
                }
            }
            else
            {
                if (currentFoundedIndex == 1)
                {
                    rowIndex = foundedRowsIndexes[--currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                    thisTSB.Enabled = false;
                    toolStripButton3.Enabled = true;
                }
                else if (currentFoundedIndex != 0)
                {
                    rowIndex = foundedRowsIndexes[--currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                    toolStripButton3.Enabled = true;
                }
                else
                {
                    //rowIndex = foundedRowsIndexes[foundedRowsIndexes.Length - 1];
                    OutputDataToForm(rowIndex);
                    thisTSB.Enabled = false;
                }
            }
        }
        // Нажатие на кнопку перехода К следующей записи 
        private void toolStrip_Next_Button_Click(object sender, EventArgs e)
        {
            ToolStripButton thisTSB = sender as ToolStripButton;
            if (!Properties.Settings.Default.IsSearching)
            {
                if (rowIndex == questionsTable.Rows.Count - 2)
                {
                    OutputDataToForm(++rowIndex);
                    toolStripButton2.Enabled = true;
                    thisTSB.Enabled = false;
                }
                else if (rowIndex != questionsTable.Rows.Count - 1)
                {
                    OutputDataToForm(++rowIndex);
                    toolStripButton2.Enabled = true;
                }
                
                else
                {
                    thisTSB.Enabled = false;
                }
            }
            else
            {
                if (currentFoundedIndex == foundedRowsIndexes.Length - 2)
                {
                    rowIndex = foundedRowsIndexes[++currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                    thisTSB.Enabled = false;
                    toolStripButton2.Enabled = true;
                }
                else if (currentFoundedIndex != foundedRowsIndexes.Length - 1)
                {
                    rowIndex = foundedRowsIndexes[++currentFoundedIndex];
                    OutputDataToForm(rowIndex);
                    toolStripButton2.Enabled = true;
                }
                else
                {
                    //rowIndex = foundedRowsIndexes[0];
                    OutputDataToForm(rowIndex);
                    thisTSB.Enabled = false;
                }
            }
        }
        // Нажатие на кнопку перехода К последней записи 
        private void toolStrip_ToEnd_Button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.IsSearching)
            {
                rowIndex = questionsTable.Rows.Count - 1;
                OutputDataToForm(rowIndex);
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = false;
            }
            else
            {
                //rowIndex = foundedRowsIndexes.Length - 1;
                currentFoundedIndex = foundedRowsIndexes.Length - 1;
                rowIndex = foundedRowsIndexes[currentFoundedIndex];
                OutputDataToForm(rowIndex);
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = false;
            }
        }
        // При изменении свойства Text элемента toolStripTextBox1 (отображается номер текущей записи)
        private void toolStrip_TextBox_TextChanged(object sender, EventArgs e)
        {
            // Проверяем, не вышли ли мы за границу. Если вышли, то исправляем
            ToolStripTextBox tSTB = sender as ToolStripTextBox;
            if (tSTB.Text == null || tSTB.Text == "")
                tSTB.Text = "1";
            if (Int32.Parse(tSTB.Text) > questionsTable.Rows.Count)
                tSTB.Text = questionsTable.Rows.Count.ToString();

            // Проверяем граничные значения, чтобы включать/отключать кнопки
            // перехода к предыдущей/следующей записи (чтобы не выйти за пределы массива записей)
            if (Int32.Parse(tSTB.Text) == 1)
            {
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = true;
            }
            else if (Int32.Parse(tSTB.Text) == questionsTable.Rows.Count)
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

        #region TextBoxes

        // Запрет на ввод каких-либо символов, отличных от цифр и запятой 
        // (допускаются командные клавиши, т.е. Backspace и т.д.)
        private void correctAnswer_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Если нажатая клавиша не является клавишей управления (backspace и т.д.),
            // цифрой или запятой, то пропускаем эту клавишу
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                // Мы обработали это событие
                e.Handled = true;
            }
        }

        #endregion

        #region ComboBoxes

        private void AnswersCount_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cB = sender as ComboBox;
            // Сколько ответов предлагает вопрос
            Int32 numOfAnswers = Int32.Parse(cB.SelectedItem.ToString());
            // Сколько всего ответов может быть
            Int32 answersCount = 8;

            if(numOfAnswers < 1)
            {
                return;
            }
            
            foreach(TextBox tB in answersTextBoxes)
            {
                tB.Enabled = true;
                //tB.Text = "";
            }

            for(Int32 i = numOfAnswers; i < answersCount; i++)
            {
                answersTextBoxes[i].Enabled = false;
                answersTextBoxes[i].Text = "Unavailable";
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
            currentQuestionRow = questionsTable.Rows[rowIndex];
            Int32 answersFK = Int32.Parse(questionsTable.Rows[rowIndex]["Questions_AnswersFK"].ToString());
            //currentLoginRow = loginTable.Rows[answersFK];
            currentAnswersRow = answersTable.AsEnumerable()
                .Single<DataRow>(row => row.Field<Int32>("AnswersID") == answersFK);

            questionContent_TextBox.Text = currentQuestionRow["QuestionContent"].ToString();
            questionType_ComboBox.SelectedItem = currentQuestionRow["QuestionType"].ToString();
            answersCount_ComboBox.SelectedItem = currentQuestionRow["AnswersCount"].ToString();
            correctAnswer_TextBox.Text = currentQuestionRow["CorrectAnswer"].ToString();
            answer_1_TextBox.Text = currentAnswersRow["Answer_1"].ToString();
            answer_2_TextBox.Text = currentAnswersRow["Answer_2"].ToString();
            answer_3_TextBox.Text = currentAnswersRow["Answer_3"].ToString();
            answer_4_TextBox.Text = currentAnswersRow["Answer_4"].ToString();
            answer_5_TextBox.Text = currentAnswersRow["Answer_5"].ToString();
            answer_6_TextBox.Text = currentAnswersRow["Answer_6"].ToString();
            answer_7_TextBox.Text = currentAnswersRow["Answer_7"].ToString();
            answer_8_TextBox.Text = currentAnswersRow["Answer_8"].ToString();

            toolStripTextBox1.Text = (rowIndex + 1).ToString();
        }

        private void ClearDataInForm()
        {
            questionType_ComboBox.ResetText();
            questionType_ComboBox.SelectedIndex = -1;
            answersCount_ComboBox.ResetText();
            answersCount_ComboBox.SelectedIndex = -1;
            questionContent_TextBox.Text = "";
            correctAnswer_TextBox.Text = "";
            answer_1_TextBox.Text = "";
            answer_2_TextBox.Text = "";
            answer_3_TextBox.Text = "";
            answer_4_TextBox.Text = "";
            answer_5_TextBox.Text = "";
            answer_6_TextBox.Text = "";
            answer_7_TextBox.Text = "";
            answer_8_TextBox.Text = "";
        }

        // Меняет режим чтения/записи для TextBox'ов, в которых обрабатываются значения текущей строки
        // true - ReadOnly,
        // false - Read and Write
        private void ReadOnly_ReadWrite_TextBoxes(bool state)
        {
            questionType_ComboBox.Enabled = !state;
            questionContent_TextBox.ReadOnly = state;
            answersCount_ComboBox.Enabled = !state;
            correctAnswer_TextBox.ReadOnly = state;
            answer_1_TextBox.ReadOnly = state;
            answer_2_TextBox.ReadOnly = state;
            answer_3_TextBox.ReadOnly = state;
            answer_4_TextBox.ReadOnly = state;
            answer_5_TextBox.ReadOnly = state;
            answer_6_TextBox.ReadOnly = state;
            answer_7_TextBox.ReadOnly = state;
            answer_8_TextBox.ReadOnly = state;
        }

        
        // Настройка остальных элементов формы (включая саму форму)
        // Название функции, наверное, лучше будет изменить
        private void MyComponentsInitialization()
        {
            questionType_ComboBox.Tag = "Questions.QuestionType";
            questionContent_TextBox.Tag = "Questions.QuestionContent";
            answersCount_ComboBox.Tag = "Questions.AnswersCount";
            correctAnswer_TextBox.Tag = "Questions.CorrectAnswer";
            answer_1_TextBox.Tag = "Answers.Answer_1";
            answer_2_TextBox.Tag = "Answers.Answer_2";
            answer_3_TextBox.Tag = "Answers.Answer_3";
            answer_4_TextBox.Tag = "Answers.Answer_4";
            answer_5_TextBox.Tag = "Answers.Answer_5";
            answer_6_TextBox.Tag = "Answers.Answer_6";
            answer_7_TextBox.Tag = "Answers.Answer_7";
            answer_8_TextBox.Tag = "Answers.Answer_8";

            dataTextBoxes = new TextBox[] 
                {questionContent_TextBox, correctAnswer_TextBox,
                    answer_1_TextBox, answer_2_TextBox, answer_3_TextBox, answer_4_TextBox,
                    answer_5_TextBox, answer_6_TextBox, answer_7_TextBox, answer_8_TextBox};
            dataComboBoxes = new ComboBox[] {questionType_ComboBox, answersCount_ComboBox};

            questionType_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            answersCount_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            answersTextBoxes = new TextBox[] { answer_1_TextBox, answer_2_TextBox, answer_3_TextBox, answer_4_TextBox,
                answer_5_TextBox, answer_6_TextBox, answer_7_TextBox, answer_8_TextBox};

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
            toolStripLabel1.Text = "/" + questionsTable.Rows.Count;
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

            correctAnswer_TextBox.KeyPress += new KeyPressEventHandler(correctAnswer_TextBox_KeyPress);

            answersCount_ComboBox.SelectedIndexChanged += new EventHandler(AnswersCount_ComboBox_SelectedIndexChanged);

            // Деактивируем кнопку "Закончить поиск"
            button7.Enabled = false;

            // Добавляем делегат для события закрытия формы (закрытие ещё не закончилось)
            this.FormClosing += new FormClosingEventHandler(Form_OnClosing);
        }
    }
}