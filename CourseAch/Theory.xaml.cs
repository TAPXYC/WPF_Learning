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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseAch
{
    /// <summary>
    /// Логика взаимодействия для Theory.xaml
    /// </summary>
    public partial class Theory : Page
    {
        //Текущий уровень
        Level CurrentLvl;

        public Theory(Level level)
        {
            InitializeComponent();

            //Присваиваем значение текущего уровня
            CurrentLvl = level;

            this.Title.Text = CurrentLvl.Title;
            this.Example.Children.Add(CurrentLvl.Example);
            Lection.Text = CurrentLvl.Text;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Далее". Переходит на следующую страницу
        /// </summary>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            //Получаем данные, сколько уровней уже пройдено
            int CompleteLvl;
            StreamReader SR = new StreamReader("NumOfCompleteLvl.cfg");
            CompleteLvl = int.Parse(SR.ReadToEnd());
            SR.Close();

            //Если пользователь прошел последний из пока доступных ему уровней - то записываем его номер в файл конфигурации
            if (CurrentLvl.NumOfThisLvl > CompleteLvl)
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

        /// <summary>
        /// Событие нажатия на кнопку "Назад к главам"
        /// </summary>
        private void BtnBackToChapter_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeFoundPage("Chapters.xaml");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(Example.Children.Count == 0)
            {
                if((Application.Current.MainWindow as MainWindow).SaveStackPanel != null)
                {
                    Example.Children.Add((Application.Current.MainWindow as MainWindow).SaveStackPanel);
                }
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).SaveStackPanel = CurrentLvl.Example;
            Example.Children.Clear();
        }
    }
}
