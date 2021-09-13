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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        string[] Position = new string[] { "Новобранец", "Дерзкий новичок","Освоивший основы", "Смотрящий в конструктор", 
            "Маленький верстальщик", "Начинающий демиург", "Говорящий с компилятором", "Великий Компоновщик", "Создающий ... нечто", "Способный творить",
            "Познавший красоту", "Знающий", "Мистер Компактность", "Могучий Могуч"};
        public MainMenu()
        {
            InitializeComponent();

            //Кол-во уже пройденных уровней
            int CountCompletedLevels = 0;

            //Считываем из файла конфигурации номер последнего пройденного уровня
            StreamReader ReaderCfg;

            try
            {
                ReaderCfg = new StreamReader("NumOfCompleteLvl.cfg");
                string CmpltdLvl = ReaderCfg.ReadToEnd();
                try
                {
                    CountCompletedLevels = int.Parse(CmpltdLvl);
                }
                catch
                {
                    StreamWriter WriterCfg = new StreamWriter("NumOfCompleteLvl.cfg");
                    WriterCfg.Write("0");
                    WriterCfg.Close();
                }
                ReaderCfg.Close();
            }
            catch
            {
                StreamWriter WriterCfg = new StreamWriter("NumOfCompleteLvl.cfg");
                WriterCfg.Write("0");
                WriterCfg.Close();
            }

            NumOfCompleteLvl.Text = CountCompletedLevels.ToString();
            CurrentPosition.Text = Position[CountCompletedLevels / 6];
        }

        /// <summary>
        /// Событие нажатия на кнопку "Начать". Загружает страницу "Разделы"
        /// </summary>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeFoundPage("Chapters.xaml");
        }


        /// <summary>
        /// Событие нажатия на кнопку "Рекомендуемая литература". Загружает страницу "Рекомендуемая литература"
        /// </summary>
        private void BtnRecommendedReading_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeFoundPage("RecommendedReading.xaml");
        }


        /// <summary>
        /// Событие нажатия на кнопку "Выход"
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ClosingConfirmWindow confirmWindow = new ClosingConfirmWindow();
            bool DoClose = confirmWindow.ShowDialog(1);
            
            if (DoClose)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
