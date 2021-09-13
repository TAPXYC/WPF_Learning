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
    /// Логика взаимодействия для Chapters.xaml
    /// </summary>
    public partial class Chapters : Page
    {
        public Chapters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Все уровни
        /// </summary>
        Dictionary<string, List<Level>> AllChapters = new Dictionary<string, List<Level>>()
        {      //6
            {
                "Введение", (Application.Current.MainWindow as MainWindow).Vvedenie
            },
            //8
            {
                "XAML", (Application.Current.MainWindow as MainWindow).XAML
            }, 
            //14
            {
                "Компоновка", (Application.Current.MainWindow as MainWindow).Komponovka
            }, 
            //28
            {
                "Элементы управления", (Application.Current.MainWindow as MainWindow).ElmYpravlenia
            }, 
            //6
            {
                "Модель событий", (Application.Current.MainWindow as MainWindow).ModelSobitii
            },
            //12
            {
                "Кисти", (Application.Current.MainWindow as MainWindow).Kisti
            }, 
            
            //8
            {
                "Ресурсы", (Application.Current.MainWindow as MainWindow).Resyrsi
            }/*,
            //6
            {
                "Привязка", (Application.Current.MainWindow as MainWindow).Privyazka
            }, 
            //8
            {
                "Стили, триггеры, темы", (Application.Current.MainWindow as MainWindow).StiliTriggeri
                    //"Назначение стилей",
                    //"Тест по теме\n\"Назначение стилей\"",

                    //"Определение стилей",
                    //"Задание по теме\n\"Определение стилей\"",

                    //"Триггеры, созданиие триггеров",
                    //"Задание по теме\n\"Триггеры, созданиие триггеров\"",

                    //"Темы, определение тем",
                    //"Задание по теме\n\"Темы, определение тем\""
                
            }, 
            //4
            {
                "Класс Application", (Application.Current.MainWindow as MainWindow).KlassApplication
                    //"Основные события жизни приложения",
                    //"Тест по теме\n\"Основные события жизни приложения\"",

                    //"Обращение к текущему приложению",
                    //"Тест по теме\n\"Обращение к текущему приложению\""
                
            }, 
            //24
            {
                "Работа с графикой", (Application.Current.MainWindow as MainWindow).RabotaSGrafikoi
                    //"Фигуры. Базовые свойства",
                    //"Задание по теме\n\"Фигуры. Базовые свойства\"",

                    //"Линия (Line)",
                    //"Тест по теме\n\"Линия (Line)\"",

                    //"Ломаная линия (Polyline)",
                    //"Задание по теме\n\"Ломаная линия (Polyline)\"",

                    //"Овал (Ellipse)",
                    //"Задание по теме\n\"Овал (Ellipse)\"",

                    //"Прямоугольник (Rectangle)",
                    //"Тест по теме\n\"Прямоугольник (Rectangle)\"",

                    //"Многоугольник (Polygon)",
                    //"Задание по теме\n\"Многоугольник (Polygon)\"",

                    //"Объединение фигур в группы (GeometryGroup)",
                    //"Тест по теме\n\"Объединение фигур в группы (GeometryGroup)\"",

                    //"Комбинация фигур (CombinedGeometry)",
                    //"Задание по теме\n\"Комбинация фигур (CombinedGeometry)\"",

                    //"Сложные фигуры. Геометрия пути (PathGeometry)",
                    //"Задание по теме\n\"Сложные фигуры. Геометрия пути (PathGeometry)\"",

                    //"Дуга (ArcSegment)",
                    //"Тест по теме\n\"Дуга (ArcSegment)\"",

                    //"Кривые Безье (BezierSegment)",
                    //"Задание по теме\n\"Кривые Безье (BezierSegment)\"",

                    //"Трансформации (Transform)",
                    //"Тест по теме\n\"Трансформации (Transform)\""

            },
            //10
            {
                "Анимация", (Application.Current.MainWindow as MainWindow).Animacia
                    //"Основы анимаций",
                    //"Тест по теме\n\"Основы анимаций\"",

                    //"Программная анимация",
                    //"Задание по теме\n\"Программная анимация\"",

                    //"Анимация в XAML",
                    //"Задание по теме\n\"Анимация в XAML\"",

                    //"Анимация свойств вложенных объектов",
                    //"Задание по теме\n\"Анимация свойств вложенных объектов\"",

                    //"Анимация пути",
                    //"Задание по теме\n\"Анимация пути\"",

                    //"Плавность анимации",
                    //"Задание по теме\n\"Плавность анимации\""
                
            }, 
            //8
            {
                "Окна. Вызов, взаимодействие", (Application.Current.MainWindow as MainWindow).OknaVizov
                    //"Класс Window",
                    //"Тест по теме\n\"Класс Window\"",

                    //"Взаимодействие между окнами",
                    //"Задание по теме\n\"Взаимодействие между окнами\"",

                    //"Методы вызова окна",
                    //"Задание по теме\n\"Методы вызова окна\"",

                    //"Обертка методов вызова окна",
                    //"Задание по теме\n\"Обертка методов вызова окна\""
                
            }*/
        };


        /// <summary>
        /// Событие загрузки страницы
        /// </summary>
        private void PageChapters_Loaded(object sender, RoutedEventArgs e)
        {
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

            //Для каждого раздела создаем свой контейнер
            foreach(KeyValuePair<string, List<Level>> Chapter in AllChapters)
            {
                //контейнер для главы. разделен на заголовок, содержимое и отступ
                Grid GrdChptr = new Grid();
                GrdChptr.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60)} );
                GrdChptr.RowDefinitions.Add(new RowDefinition());
                GrdChptr.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });

                //
                TextBlock NameChapter = new TextBlock() { Style = (Style)this.TryFindResource("ChaptersTblck"), Text = Chapter.Key };
                GrdChptr.Children.Add(NameChapter);

                WrapPanel ChapterLevels = new WrapPanel() { MaxWidth = 390, HorizontalAlignment = HorizontalAlignment.Center };
                Grid.SetRow(ChapterLevels, 1);

                //В каждом разделе создаем кнопки уровней
                for(int i = 0; i < Chapter.Value.Count; i++)
                {
                    Button LevelBtn = new Button() { Content = i+1};
                    //В таге храним указатель на экземпляр уровня
                    LevelBtn.Tag = Chapter.Value[i];

                    //Если уровень доступен - делаем кнопку активной
                    if(CountCompletedLevels >= 0)
                    {
                        LevelBtn.Style = (Style)TryFindResource("UnlockedBtn");

                        LevelBtn.Click += LevelBtn_Click;

                        CountCompletedLevels--;
                    }
                    //Иначе создаем пустую кнопку
                    else
                    {
                        LevelBtn.Style = (Style)TryFindResource("LockedBtn");
                    }
                    
                    //Привязываем события наведения мыши на кнопку
                    LevelBtn.MouseEnter += LevelBtn_MouseEnter;
                    LevelBtn.MouseLeave += LevelBtn_MouseLeave;

                    //Добавляем кнопку в контейнер уровней
                    ChapterLevels.Children.Add(LevelBtn);
                }

                //Добавляем контейнер уровней в контейнер главы
                GrdChptr.Children.Add(ChapterLevels);

                //Добавляем контейнер главы в контейнер всех глав
                StckPnl_Chapters.Children.Add(GrdChptr);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на разблокированную кнопку
        /// </summary>
        private void LevelBtn_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ClosingDoorsBeforeModel((sender as Button).Tag as Level);
        }



        /// <summary>
        /// События наведения на кнопку уровня
        /// </summary>
        private void LevelBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            TblckInfo.Text = "Наведите на уровень чтобы узнать его тему";
        }
        private void LevelBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            TblckInfo.Text = ((sender as Button).Tag as Level).Title;
        }
    }
}
