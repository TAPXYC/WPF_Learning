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
    /// Логика взаимодействия для WriteTask.xaml
    /// </summary>
    public partial class WriteTask : Page
    {
        Level CurrentLvl;
        //Список всех тестовых слов
        List<TextBox> AllTestTbx = new List<TextBox>();

        /// <summary>
        /// Добавление слова в панель предложения
        /// </summary>
        void AddWord(StringBuilder CurWord, WrapPanel Sentence)
        {
            //Если у нас было слово оформляем его в текстблок
            TextBlock TBWord = new TextBlock()
            {
                Text = CurWord.ToString(),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 17,
                Foreground = Brushes.Yellow,
                FontFamily = new FontFamily("Consolas")
            };
            //добавляем его в предложение
            Sentence.Children.Add(TBWord);
            //очищаем текущее слово
            CurWord.Clear();
        }

        /// <summary>
        /// Конструктор модуля
        /// </summary>
        /// <param name="level">Уровень, по которому будет создаваться модуль</param>
        public WriteTask(Level level)
        {
            InitializeComponent();

            CurrentLvl = level;

            this.Title.Text = CurrentLvl.Title;
            this.TaskTable.Text = CurrentLvl.Text;
            string Code = CurrentLvl.Code;

            //Текущее слово
            StringBuilder CurWord = new StringBuilder();
            //Контейнер для слов
            WrapPanel Sentence = new WrapPanel() 
            { 
                HorizontalAlignment = HorizontalAlignment.Left, 
                VerticalAlignment = VerticalAlignment.Top 
            };

            for(int chrNum = 0; chrNum < Code.Length; chrNum++)
            {
                //Если встретили пробел
                if(Code[chrNum] == ' ')
                {
                    //Если у нас уже есть слово
                    if (CurWord.Length != 0)
                    {
                        AddWord(CurWord, Sentence);
                    }

                    //Оформляем пробел в текстблок
                    TextBlock TbSpace = new TextBlock()
                    {
                        Text = " ",
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontSize = 17,
                    };
                    //добавляем его в предложение
                    Sentence.Children.Add(TbSpace);

                    continue;
                }

                //Если тильда - символ начала тестового слова
                if(Code[chrNum] == '~')
                {  
                    //пропускаем тильду
                    chrNum++;

                    //Если у нас уже есть слово
                    if (CurWord.Length != 0)
                    {
                        //Добавляем его
                        AddWord(CurWord, Sentence);   
                    }

                    //начинаем читать тестовое слово
                    //Тестовое слово
                    StringBuilder TestWord = new StringBuilder();
                    //читаем до следующей тильды
                    for(;Code[chrNum] !='~'; chrNum++ )
                    {
                        TestWord.Append(Code[chrNum]);
                    }

                    //оформляем найденное слово в текстовое поле
                    TextBox TbxTestWord = new TextBox()
                    {
                        //запоминаем правильный ответ для этого поля
                        Tag = TestWord.ToString(),
                        Background = Brushes.Transparent,
                        BorderThickness = new Thickness(0, 0, 0, 3),
                        MaxLength = TestWord.Length,
                        Width = 9.7 * TestWord.Length,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontSize = 17,
                        Foreground = Brushes.White,
                        FontFamily = new FontFamily("Consolas")
                    };
                    TbxTestWord.GotFocus += TbxTestWord_GotFocus;
                    //добавляем его в предложение
                    Sentence.Children.Add(TbxTestWord);
                    //Добавляем тестовое поле в список тестовых полей
                    AllTestTbx.Add(TbxTestWord);

                    continue;
                }

                //Если конец предложения
                if (Code[chrNum] == '\n')
                {
                    AddWord(CurWord, Sentence);

                    TestField.Children.Add(Sentence);

                    Sentence = new WrapPanel()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    continue;
                }

                //Дошли до сюда - значит данный символ я-я частью слова
                //Добавляем его в слово
                CurWord.Append(Code[chrNum]);
            }

        }

        private void TbxTestWord_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.White;
        }

        /// <summary>
        /// Обработчик события возврата к лекции
        /// </summary>
        private void btn_BackToLection_click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeModel(CurrentLvl.Prev);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку проверить. Выполняет проверку правильности заполнения текстовых полей
        /// </summary>
        private void btn_Check_click(object sender, RoutedEventArgs e)
        {
            bool AllIsRight = true;
            foreach (TextBox TbTest in AllTestTbx)
            {
                if(TbTest.Text != TbTest.Tag.ToString())
                {
                    AllIsRight = false;
                    TbTest.Foreground = Brushes.Red;
                }
            }
            //Если всё верно - переходим на следующую страницу
            if(AllIsRight)
            {
                Canvas.SetZIndex(Block, 4);

                TaskTable.FontSize = 45;
                TaskTable.Text = "Верно!";

                //Получаем данные, сколько уровней уже пройдено
                int CompleteLvl;
                StreamReader SR = new StreamReader("NumOfCompleteLvl.cfg");
                CompleteLvl = int.Parse(SR.ReadToEnd());
                SR.Close();

                //Если пользователь прошел последний доступный ему уровень - то записываем его номер в файл конфигурации
                if (CurrentLvl.NumOfThisLvl > CompleteLvl)
                {
                    StreamWriter WriterCfg = new StreamWriter("NumOfCompleteLvl.cfg");
                    WriterCfg.Write(CurrentLvl.NumOfThisLvl);
                    WriterCfg.Close();
                }

                (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeModel(CurrentLvl.Next);
            }
            //нет - запускаем анимацию
            else
            {
                Canvas.SetZIndex(Block, 4);

                DoubleAnimationUsingKeyFrames IncFontSize = new DoubleAnimationUsingKeyFrames();
                IncFontSize.Duration = TimeSpan.FromSeconds(1.5);
                IncFontSize.KeyFrames.Add(new DiscreteDoubleKeyFrame(45, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                IncFontSize.KeyFrames.Add(new DiscreteDoubleKeyFrame(18, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
                IncFontSize.Completed += IncFontSize_Completed;

                TaskTable.Text = "Неверно!";
                TaskTable.BeginAnimation(FontSizeProperty, IncFontSize);

                RadialGradientBrush Rad = (RadialGradientBrush)TryFindResource("Rad");

                Rad.GradientStops[0].Color = Colors.Red;
                Rad.GradientStops[1].Color = Color.FromArgb(0xFE, 0xFF, 0x6C, 0x6C);
                Rad.GradientStops[2].Color = Color.FromArgb(0xFF,0xFF,46,00);
                Rad.GradientStops[3].Color = Color.FromArgb(0xFF,0xBB,00,00);
                Rad.GradientStops[4].Color = Color.FromArgb(0xFF,08,00,00);
                Rad.GradientStops[5].Color = Color.FromArgb(0xFF,0xAE,00,00);
            }
        }

        /// <summary>
        /// Обработчик события окончания анимации
        /// </summary>
        private void IncFontSize_Completed(object sender, EventArgs e)
        {
            TaskTable.Text = CurrentLvl.Text;

            RadialGradientBrush Rad = (RadialGradientBrush)TryFindResource("Rad");
           
            Rad.GradientStops[0].Color = Colors.White;
            Rad.GradientStops[1].Color = Color.FromArgb(0xFE,00,0xE7,0xFE);
            Rad.GradientStops[2].Color = Colors.Cyan;
            Rad.GradientStops[3].Color = Color.FromArgb(0xFF,00,55,0xBB);
            Rad.GradientStops[4].Color = Color.FromArgb(0xFF,00,30,38);
            Rad.GradientStops[5].Color = Color.FromArgb(0xFF,00,0xA6,0xAE);

            Canvas.SetZIndex(Block, -1);
        }
    }
}
