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
    public partial class Student_Form : Form
    {
        /*-------------------- Переменные --------------------*/

        // Пока не все вопросы готовы, использую эту переменную, чтобы регулировать, сколько выдавать вопросов
        Int32 numOfTakenQuestions = 30;

        Int32 answersOffsetInQuery = 7;


        SqlConnection connectionString;
        Int32 studentLoginID;
        List<String> studentInfo;

        Int32 numOfQuestions = 116; // Надо будет сделать так, чтобы эта переменная получала значение из БД
        Int32 numOfAnswers;
        List<Int32> shuffledNums;
        List<List<String>> questions;
        Int32 questionCounter;
        String correctAnswers;

        Int32 numOfCorrectlyAnsweredQuestions;
        Int32 mark;

        enum QuestionType
        {
            OneAnswer,
            MultiAnswers
        }
        QuestionType qType;

        enum PanelColorsState
        {
            Correct,
            Wrong,
            Neutral
        }

        enum ButtonState
        {
            BeginTest,
            NextQuestion,
            PreviousQuestion,
            FinishTest
        };
        /*-------------------- Конструкторы --------------------*/

        public Student_Form()
        {
            InitializeComponent();
        }

        public Student_Form(SqlConnection connectionString, Int32 studentLoginID)
        {
            InitializeComponent();

            Configure();

            this.studentLoginID = studentLoginID;
            this.connectionString = connectionString;
        }


        /*-------------------- Делегаты --------------------*/

        private void Student_Form_Load(object sender, EventArgs e)
        {
            GetInformationAboutCurrentStudent(studentLoginID);
        }

        private void Student_Form_Closing(object sender, FormClosingEventArgs e)
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

        // 3 Состояния кнопки:
        // Начать тест (подготавливается переменная-счётчик вопросов, запрос и т.д)
        // Перейти к следующему вопросу (инкрементируется счётчик, выполняется запрос на получение следующего вопроса)
        // Завершить тестирование
        private void start_Button_Click(object sender, EventArgs e)
        {
            shuffledNums = Enumerable.Range(1, numOfQuestions).Shuffle(new Random()).Take(numOfTakenQuestions).ToList();
            GetQuestionsWithAnswers();
            questionCounter = 0;
            numOfAnswers = 0;
            numOfCorrectlyAnsweredQuestions = 0;
            PreparePanelForQuestion();

            next_Button.Text = "Дальше";
            next_Button.Enabled = false;
            start_Button.Enabled = true;
            answer_Button.Enabled = true;

            if (firstPanel.Visible)
            {
                firstPanel.SendToBack();
                firstPanel.Visible = false;
            }
        }

        private void exit_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void next_Button_Click(object sender, EventArgs e)
        {
            if (questionCounter != numOfTakenQuestions - 1)
            {
                questionCounter++;
                numOfAnswers = 0;
                flowLayoutPanel1.Controls.Clear();
                PreparePanelForQuestion();
                ChangeColorDependingOnAnswer(PanelColorsState.Neutral);

                Button thisBtn = sender as Button;
                thisBtn.Enabled = false;
                answer_Button.Enabled = true;

                if (questionCounter == numOfTakenQuestions - 2)
                {
                    thisBtn.Text = "Закончить";
                }
            }
            else
            {
                if (!firstPanel.Visible)
                {
                    firstPanel.BringToFront();
                    firstPanel.Visible = true;
                }
                flowLayoutPanel1.Controls.Clear();
                GetMark();
                SendInformationAboutPassedTest();
                GetInformationAboutCurrentStudent(studentLoginID);
            }
        }

        private void previous_Button_Click(object sender, EventArgs e)
        {

        }

        private void answer_Button_Click(object sender, EventArgs e)
        {
            String studentAnswer = "";

            if (qType == QuestionType.OneAnswer)
            {
                IEnumerable<RadioButton> orderedRBs = from curRB in flowLayoutPanel1.Controls.OfType<RadioButton>()
                                                   orderby curRB.Tag
                                                   select curRB;
                foreach (RadioButton rB in orderedRBs)
                {
                    if (rB.Checked)
                    {
                        studentAnswer = rB.Tag.ToString();
                    }
                }
            }
            else
            {
                IEnumerable<CheckBox> orderedCBs = from curCB in flowLayoutPanel1.Controls.OfType<CheckBox>()
                                                   orderby curCB.Tag
                                                   select curCB;
                foreach (CheckBox cB in orderedCBs)
                {
                    if (cB.Checked)
                    {
                        studentAnswer += cB.Tag.ToString() + ",";
                    }
                }
            }
            studentAnswer = studentAnswer.TrimEnd(',', ' ');
            


            if (studentAnswer == correctAnswers)
            {
                ChangeColorDependingOnAnswer(PanelColorsState.Correct);
                numOfCorrectlyAnsweredQuestions++;
            }
            else
            {
                ChangeColorDependingOnAnswer(PanelColorsState.Wrong);
            }

            next_Button.Enabled = true;
            Button thisBtn = sender as Button;
            thisBtn.Enabled = false;
        }

        /*-------------------- Остальные функции --------------------*/

        private void Configure()
        {
            this.Load += new EventHandler(Student_Form_Load);
            this.FormClosing += new FormClosingEventHandler(Student_Form_Closing);
            exit_Button.Click += new EventHandler(exit_Button_Click);
            start_Button.Click += new EventHandler(start_Button_Click);
            answer_Button.Click += new EventHandler(answer_Button_Click);
            next_Button.Click += new EventHandler(next_Button_Click);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void GetInformationAboutCurrentStudent(Int32 id)
        {
            SqlCommand getInfoCmd = new SqlCommand(
                "SELECT * " +
                "FROM Students " +
                "WHERE " +
                "Students.Students_LoginFK = " + id.ToString(),
                connectionString);
            SqlDataReader infoReader;
            infoReader = getInfoCmd.ExecuteReader();

            if(!infoReader.HasRows)
            {
                MessageBox.Show("О данном студенте нет информации!", 
                    "Странно...", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
                return;
            }

            while(infoReader.Read())
            {
                studentInfo = new List<string>();
                for (Int32 i = 0; i < infoReader.FieldCount; i++)
                    studentInfo.Add(infoReader[i].ToString());

                studentName_Label.Text = infoReader["LastName"].ToString() + 
                    " " + infoReader["FirstName"].ToString() +
                    " " + infoReader["Patronymic"].ToString();
                faculty_Label.Text = infoReader["Faculty"].ToString();
                speciality_Label.Text = infoReader["Speciality"].ToString();
                courseNum_Label.Text = infoReader["Course"].ToString();
                testPassed_Label.Text = infoReader["IsTestPassed"].ToString();
                lastMark_Label.Text = infoReader["Mark"].ToString();
                numOfRetries_Label.Text = infoReader["NumOfRetries"].ToString();
            }
            infoReader.Close();
        }

        private void SendInformationAboutPassedTest()
        {
            Int32 studentID = Int32.Parse(studentInfo[0]) - 1;

            DataTable table = new DataTable("Students");

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students",
                connectionString);
            adapter.Fill(table);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();

            table.Rows[studentID]["IsTestPassed"] = 1;
            table.Rows[studentID]["Mark"] = mark;
            table.Rows[studentID]["NumOfRetries"] = (Int32.Parse(studentInfo[9]) + 1);

            adapter.Update(table);
        }

        private void GetQuestionsWithAnswers()
        {
            // Переменная, в которой будет храниться запрос на получение вопросов и ответов
            SqlCommand getQuestionAndAnswers;
            // Переменная, в которой будут храниться номера необходимых вопросов для использования
            // с предикатом IN()
            String questionNumbers = "";

            // Формируем строку для предиката IN()
            foreach (Int32 num in shuffledNums)
                questionNumbers += num.ToString() + ", ";
            // Убираем лишние символы пробела и запятой в конце
            questionNumbers = questionNumbers.TrimEnd(' ', ',');

            // Формируем запрос
            getQuestionAndAnswers = new SqlCommand(
                "SELECT * " +
                "FROM Questions INNER JOIN Answers ON " +
                "Questions.Questions_AnswersFK = Answers.AnswersID " +
                "WHERE Questions.QuestionNumber IN (" + questionNumbers + ")",
                connectionString
                );

            // Объявляем Reader для получения результатов выполнения запроса
            SqlDataReader questionReader;
            // Запускаем Reader и передаём еёго переменной questionReader
            questionReader = getQuestionAndAnswers.ExecuteReader();

            // Если в результате выполнения запроса не пришло ни одной строки, то выходим из приложения
            if (!questionReader.HasRows)
            {
                MessageBox.Show("Извините, не найдены вопросы в базе данных.\nПрограмма сейчас закроется.",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                //return;
            }

            // Создаём новый экземпляр списка с вопросами
            questions = new List<List<string>>();
            // Читаем все вопросы и заносим их в список
            while(questionReader.Read())
            {
                List<String> question = new List<string>();
                for(Int32 i = 0; i < questionReader.FieldCount; i++)
                    question.Add(questionReader[i].ToString());
                questions.Add(question);
            }
            // Закрываем Reader
            questionReader.Close();
        }

        private void PreparePanelForQuestion()
        {
            questionNumber_Label.Text = "Вопрос №" + (questionCounter + 1).ToString();
            questionContent_Label.Text = questions[questionCounter][2];

            if (questions[questionCounter][3] == "One")
            {
                //GenerateAnswers(QuestionType.OneAnswer);
                qType = QuestionType.OneAnswer;
                GenerateAnswers();
            }
            else if (questions[questionCounter][3] == "Multiple")
            {
                //GenerateAnswers(QuestionType.MultiAnswers);
                qType = QuestionType.MultiAnswers;
                GenerateAnswers();
            }
            else
            {
                MessageBox.Show("Неизвестен или неправильно указан тип вопроса!", 
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            // Получаем правильные ответы
            correctAnswers = questions[questionCounter][5];

        }

        //private void GenerateAnswers(QuestionType qType)
        private void GenerateAnswers()
        {
            numOfAnswers = Int32.Parse(questions[questionCounter][4]);
            Int32[] answersNums = new Int32[numOfAnswers];
            List<String> answers = new List<String>(numOfAnswers);

            answersNums = Enumerable.Range(1, numOfAnswers).Shuffle(new Random()).ToArray();

            //for (Int32 i = 8; i < questions[questionCounter].Capacity; i++)
            for(Int32 i = 0; i < numOfAnswers; i++)
            {
                answers.Add(questions[questionCounter][answersOffsetInQuery + answersNums[i]]);
            }

            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

            switch(qType)
            {
                case QuestionType.OneAnswer:
                    for(Int32 i = 0; i < numOfAnswers; i++)
                    {
                        RadioButton answerOption = new RadioButton();
                        answerOption.Text = answers[i];
                        answerOption.Tag = answersNums[i];
                        answerOption.AutoSize = true;
                        flowLayoutPanel1.Controls.Add(answerOption);
                    }
                    break;
                case QuestionType.MultiAnswers:
                    for (Int32 i = 0; i < numOfAnswers; i++)
                    {
                        CheckBox answerOption = new CheckBox();
                        answerOption.Text = answers[i];
                        answerOption.Tag = answersNums[i];
                        answerOption.AutoSize = true;
                        // Эксперимент. Нужно, чтобы при получении значений CheckBox'ы 
                        // не пришлось заново сортировать их для сравнения с правильными ответами
                    // ПЕРЕФОРМУЛИРОВАТЬ ЭТОТ КОММЕНТАРИЙ!
                        answerOption.TabIndex = answersNums[i];
                        flowLayoutPanel1.Controls.Add(answerOption);
                    }
                    break;
            }
        }

        private void GetMark()
        {
            float percentOfCorrectAnswers = 0;
            percentOfCorrectAnswers = numOfCorrectlyAnsweredQuestions * 100 / numOfTakenQuestions;
            if (percentOfCorrectAnswers >= 90.0f)
                mark = 5;
            else if (percentOfCorrectAnswers >= 80.0f && percentOfCorrectAnswers < 90.0f)
                mark = 4;
            else if (percentOfCorrectAnswers >= 70.0f && percentOfCorrectAnswers < 80.0f)
                mark = 3;
            else
                mark = 2;
        }

        private void ChangeColorDependingOnAnswer(PanelColorsState colState)
        {
            switch(colState)
            {
                case PanelColorsState.Correct:
                    flowLayoutPanel1.BackColor = Color.LimeGreen;
                    answersPanel.BackColor = Color.LimeGreen;
                    break;
                case PanelColorsState.Wrong:
                    flowLayoutPanel1.BackColor = Color.Red;
                    answersPanel.BackColor = Color.Red;
                    break;
                case PanelColorsState.Neutral:
                    flowLayoutPanel1.BackColor = Color.PaleTurquoise;
                    answersPanel.BackColor = Color.PaleTurquoise;
                    break;
            }
        }

    }


}

public static class IEnumerableExtension
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
        T[] elements = source.ToArray();
        for (int i = elements.Length - 1; i >= 0; i--)
        {
            // Swap element "i" with a random earlier element it (or itself)
            // ... except we don't really need to swap it fully, as we can
            // return it immediately, and afterwards it's irrelevant.
            int swapIndex = rng.Next(i + 1);
            yield return elements[swapIndex];
            elements[swapIndex] = elements[i];
        }
    }
}


