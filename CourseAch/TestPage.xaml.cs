using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseAch
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        string CurrentQuess;
        string RightAnswer, CurrentAnswer;
        //Текущий уровень
        Level CurrentLvl;

        /// <summary>
        /// Конструктор класса страницы
        /// </summary>
        public TestPage(Level level)
        {
            InitializeComponent();

            CurrentLvl = level;

            //Правильный ответ должен быть на 0 месте в списке
            const int right = 0;

            this.Title.Text = CurrentLvl.Title;
            this.QuestionTable.Text = CurrentQuess = CurrentLvl.Text;
            List<string> Answers = new List<string>();
            foreach(string answer in CurrentLvl.Answers)
            {
                Answers.Add(answer);
            }

            RightAnswer = Answers[right];

            Random Rnd = new Random();

            int CommonCountOfAnswer = Answers.Count;
            //Для всех вариантов ответа создаем радиокнопки
            for (int CountOfAnswer = CommonCountOfAnswer; CountOfAnswer!=0; CountOfAnswer--)
            {
                //рандомно выбираем вариант ответа из списка оставшихся
                int index = Rnd.Next(CountOfAnswer);
                //Создаем на его основе радиокнопку
                RadioButton radioButton = new RadioButton() { Style = (Style)TryFindResource("NeonRadioBtn") };
                radioButton.Content = Answers[index];
                radioButton.Click += radioBtnAnswer;

                //Запоминаем ответ на 0й кнопке
                if(CountOfAnswer == CommonCountOfAnswer)
                {
                    CurrentAnswer = Answers[index];
                }

                //Помещаем кнопку в контейнер
                this.Answers.Children.Add(radioButton);
                //Удаляем этот вариант ответа из списка
                Answers.RemoveAt(index);
            }
            //По умолчанию выделена первая кнопка
            (this.Answers.Children[0] as RadioButton).IsChecked = true;
        }

        private void radioBtnAnswer(object sender, RoutedEventArgs e)
        {
            CurrentAnswer = (sender as RadioButton).Content.ToString();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Проверить"
        /// </summary>
        private void btnCheck(object sender, RoutedEventArgs e)
        {
            //Если текущий ответ совпадает с правильным
            if(CurrentAnswer == RightAnswer)
            {
                QuestionTable.FontSize = 57;
                QuestionTable.Text = "Верно!";

                //Убираем "темноту"
                DoubleAnimation DarkToLight = new DoubleAnimation();
                DarkToLight.From = Dark.Opacity;
                DarkToLight.To = 0;
                DarkToLight.Duration = TimeSpan.FromSeconds(0.6);
                Dark.BeginAnimation(Rectangle.OpacityProperty, DarkToLight);

                //Получаем данные, сколько уровней уже пройдено
                int CompleteLvl;
                StreamReader SR = new StreamReader("NumOfCompleteLvl.cfg");
                CompleteLvl = int.Parse(SR.ReadToEnd());
                SR.Close();

                //Если пользователь прошел последний доступный ему уровень - то записываем его номер в файл конфигурации
                if(CurrentLvl.NumOfThisLvl > CompleteLvl)
                {
                    StreamWriter WriterCfg = new StreamWriter("NumOfCompleteLvl.cfg");
                    WriterCfg.Write(CurrentLvl.NumOfThisLvl);
                    WriterCfg.Close();
                }

                //активируем следующий уровень, если он есть, иначе выходим в раздел главы
                if (CurrentLvl.Next != null)
                {
                    (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeModel(CurrentLvl.Next);
                }
                else
                {
                    (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeFoundPage("Chapters.xaml");
                }
            }
            else
            {
                QuestionTable.Text = "Неверный ответ!";

                //Активируем анимацию неверного ответа
                Canvas.SetZIndex(WrongAnswerAlarm, 4);              //Выводит прямоугольник WrongAnswerAlarm на первый план, запрещая взаимодействовать с кнопками

                DoubleAnimation DarkToLight = new DoubleAnimation();
                DarkToLight.From = Dark.Opacity;
                DarkToLight.To = 0;
                DarkToLight.AutoReverse = true;
                DarkToLight.RepeatBehavior = new RepeatBehavior(2);
                DarkToLight.Duration = TimeSpan.FromSeconds(0.3);

                DoubleAnimation AlarmActivated = new DoubleAnimation();
                AlarmActivated.From = WrongAnswerAlarm.Opacity;
                AlarmActivated.To = 1;
                AlarmActivated.AutoReverse = true;
                AlarmActivated.RepeatBehavior = new RepeatBehavior(2);
                AlarmActivated.Duration = TimeSpan.FromSeconds(0.3);
                AlarmActivated.Completed += AlarmActivated_Completed;

                WrongAnswerAlarm.BeginAnimation(Rectangle.OpacityProperty, AlarmActivated);
                Dark.BeginAnimation(Rectangle.OpacityProperty, DarkToLight);
            }
        }

        /// <summary>
        /// Завершение анимации неправильного ответа
        /// </summary>
        private void AlarmActivated_Completed(object sender, EventArgs e)
        {
            Canvas.SetZIndex(WrongAnswerAlarm, 0);
            QuestionTable.Text = CurrentQuess;
        }


        /// <summary>
        /// Обработчик нажатия на кнопку "Назад к лекции"
        /// </summary>
        private void btnBackToLection(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeModel(CurrentLvl.Prev);
        }
    }
}
