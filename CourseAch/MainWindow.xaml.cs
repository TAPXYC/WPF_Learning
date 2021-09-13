using System;
using System.Collections.Generic;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseAch
{ 
    public class Level
    {  
        public enum TypeOfLvl
        {
           Lection, 
           Test,
           WriteTask
        }

        public int NumOfThisLvl
        {
            get;
            private set;
        }
       
        public TypeOfLvl Type
        {
            get;
            private set;
        }
        public string Title
        {
            get;
            private set;
        }
        public string Text
        {
            get;
            private set;
        }
        public StackPanel Example
        {
            get;
            private set;
        }
        public List<string> Answers
        {
            get;
            private set;
        }
        public Level Prev
        {
            get;
            private set;
        }
        public Level Next
        {
            get;
            set;
        }
        public string Code
        {
            get;
            private set;
        }

        //конструктор лекционного модуля
        public Level(int NumOfThisLvl, string Title, string Theory, StackPanel Example, Level Prev)
        {
            this.NumOfThisLvl = NumOfThisLvl;
            Type = TypeOfLvl.Lection;
            this.Title = Title;
            this.Text = Theory;
            this.Example = Example;
            this.Prev = Prev;
            //указываем, что для предыдущего модуля текущий модуль будет последующим
            if(this.Prev != null)
            {
                this.Prev.Next = this;
            }
        }
        
        //конструктор практического модуля (тестов)
        public Level(int NumOfThisLvl, string Title, string Question, List<string> Answers, Level Prev)
        {
            this.NumOfThisLvl = NumOfThisLvl;
            Type = TypeOfLvl.Test;
            this.Title = Title;
            Text = Question;
            this.Answers = Answers;
            this.Prev = Prev;
            //указываем, что для предыдущего модуля текущий модуль будет последующим
            this.Prev.Next = this;
        }
  
        //конструктор практического модуля (задание)
        public Level(int NumOfThisLvl, string Title, string Task, string Code, Level Prev)
        {
            this.NumOfThisLvl = NumOfThisLvl;
            Type = TypeOfLvl.WriteTask;
            this.Title = Title;
            Text = Task;
            this.Code = Code;
            this.Prev = Prev;
            //указываем, что для предыдущего модуля текущий модуль будет последующим
            this.Prev.Next = this;
        }

        //Загрузка уровня
        public void Load()
        {
            if(Type == TypeOfLvl.Lection)
            {
                Theory theory = new Theory(this);
                (Application.Current.MainWindow as MainWindow).MainFrame.Navigate(theory);
            }
            if (Type == TypeOfLvl.Test)
            {
                TestPage testPage = new TestPage(this);
                (Application.Current.MainWindow as MainWindow).MainFrame.Navigate(testPage);
            }
            if (Type == TypeOfLvl.WriteTask)
            {
                WriteTask writeTaskPage = new WriteTask(this);
                (Application.Current.MainWindow as MainWindow).MainFrame.Navigate(writeTaskPage);
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Level> Vvedenie = new List<Level>();
        public List<Level> XAML = new List<Level>();
        public List<Level> Komponovka = new List<Level>();
        public List<Level> ElmYpravlenia = new List<Level>();
        public List<Level> ModelSobitii = new List<Level>();
        public List<Level> Kisti = new List<Level>();
        public List<Level> Resyrsi = new List<Level>();
        //public List<Level> Privyazka = new List<Level>();
        //public List<Level> StiliTriggeri = new List<Level>();
        //public List<Level> KlassApplication = new List<Level>();
        //public List<Level> RabotaSGrafikoi = new List<Level>();
        //public List<Level> Animacia = new List<Level>();
        //public List<Level> OknaVizov = new List<Level>();


        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие загрузки экрана
        /// </summary>
        ComboBox cb = new ComboBox();    //нужен для примера
        TextBlock TBTB = new TextBlock() { Background = Brushes.White, FontSize = 20, Width = 260, Height = 40, Text="Мышка НЕ над кнопкой" };

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region ВВЕДЕНИЕ
            //"Платформа WPF",
            Image WPF_Capabilities = new Image() { Margin = new Thickness(10)};
                WPF_Capabilities.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/WPF.png"));
                StackPanel L1SP = new StackPanel();
                L1SP.Children.Add(WPF_Capabilities);

                Level L1 = new Level(1, "Платформа WPF", "\tТехнология WPF (Windows Presentation Foundation) является частью экосистемы платформы .NET " +
                    "и представляет собой подсистему для построения графических интерфейсов. \n\tЕсли при создании традиционных приложений на основе WinForms за отрисовку" +
                    " элементов управления и графики отвечали такие части ОС Windows, как User32 и GDI +, то приложения WPF основаны на DirectX. \n\tВ этом состоит ключевая " +
                    "особенность рендеринга графики в WPF: используя WPF, значительная часть работы по отрисовке графики, как простейших кнопочек, так и сложных 3D - моделей," +
                    " ложиться на графический процессор на видеокарте, что также позволяет воспользоваться аппаратным ускорением графики. \n\tОдной из важных особенностей является " +
                    "использование языка декларативной разметки интерфейса XAML, основанного на XML: вы можете создавать насыщенный графический интерфейс, используя или " +
                    "декларативное объявление интерфейса, или код на управляемых языках C# и VB.NET, либо совмещать и то, и другое.\n\tВажно помнить, что все элементы платформы " +
                    "WPF являются классами, со своими конструкторами, полями и свойствами", L1SP, null);

                Vvedenie.Add(L1);

                //"Тест по теме\n\"Платформа WPF\"",
                Level T1 = new Level(2, "Тест по теме\n\"Платформа WPF\"", "Из каких основных составляющих состоит платформа WPF?",
                    new List<string> { "Код C# и XAML-разметка", "C# и C++", "ОС Windows и User32", "WinForms и XML" }, L1);

                Vvedenie.Add(T1);

                //"Преимущества и недостатки WPF",
                Level L2 = new Level(3, "Преимущества и недостатки WPF", "\tЧто вам, как разработчику, предлагает WPF? \n\t* Использование традиционных языков .NET - " +
                    "платформы - C# и VB.NET для создания логики приложения. \n\t* Возможность разделить работу дизайнера и программиста, и сделать их независимыми друг от друга." +
                    " Все это возможно благодаря разделению платформой приложения на функциональную и графическую составляющую. " +
                    " \n\t* Независимость от разрешения экрана: поскольку в WPF все элементы измеряются в независимых от устройства единицах," +
                    " приложения на WPF легко масштабируются под разные экраны с разным разрешением. \n\t* Создание трехмерных моделей, привязка данных, использование таких элементов, " +
                    "как стили, шаблоны, темы и др. \n\t* Хорошее взаимодействие с WinForms, благодаря чему, например, в приложениях WPF можно использовать традиционные " +
                    "элементы управления из WinForms. \n\t* Богатые возможности по созданию различных приложений.\n\t* Аппаратное ускорение графики - вне зависимости от того, работаете ли " +
                    "вы с 2D или 3D, графикой или текстом.\n\t* Создание приложений под множество ОС семейства Windows - от Windows XP до Windows 10. \n\n\tВ тоже время WPF имеет" +
                    " определенные ограничения. \nНесмотря на поддержку трехмерной визуализации, для создания приложений с большим количеством трехмерных изображений, прежде" +
                    " всего игр, лучше использовать другие средства - DirectX или специальные фреймворки, такие как Monogame или Unity. Также стоит учитывать, что по сравнению " +
                    "с приложениями на Windows Forms объем программ на WPF и потребление ими памяти в процессе работы в среднем несколько выше. Но это с лихвой компенсируется" +
                    " более широкими графическими возможностями и провышенной производительностью при отрисовке графики.", L1SP, T1);

                Vvedenie.Add(L2);

                //"Тест по теме\n\"Преимущества и недостатки WPF\"",
                Level T2 = new Level(4, "Тест по теме\n\"Преимущества и недостатки WPF\"", "Какое из утвердений является ложным?",
                    new List<string>() { "В случае необходимости создания красочного интерфейса предпочтительнее использовать не WPF, а WinForm",
                    "WPF позволяет разделить работу дизайнера и программиста", "WPF несколько медленнее WinForm",
                    "WPF использует процессы видеокарты для отрисовки графических составляющих" }, L2);

                Vvedenie.Add(T2);

                //"Основы интерфейса",
                Image L3_1 = new Image();
                L3_1.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/1.png"));
                Image L3_2 = new Image();
                L3_2.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/2.png"));
                Image L3_3 = new Image();
                L3_3.Margin = new Thickness(0, 15, 0, 15);
                L3_3.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/3.png"));

                StackPanel L3SP = new StackPanel();
                L3SP.Children.Add(L3_1);
                L3SP.Children.Add(L3_3);
                L3SP.Children.Add(L3_2);

                Level L3 = new Level(5, "Основы интерфейса", "\n\tПри создании приложения WPF первое, что мы видим - разделенный экран. В верхней части - пустое программное окно, называемое конструктором, и текстовую панель под " +
                    "ним - называемое вёрсткой. Это две взаимосвязанные части ВИЗУАЛЬНОГО представления программы. Собственно с этим и работают дизайнеры. \n\tИтак, раз есть окно, то, логично предположить, что " +
                    "туда можно положить элементы. Откроем панель элементов, которая располагается в левой стороне окна, или же, если она отключена, её можно открыть через пункт меню Вид -> Панель элементов." +
                    "\n\tВ панели элементов вы можете найти все доступные в вашей установленной версии .NET элементы, такие как контейнеры, кнопки, текстовые поля и другие. Более подробно мы рассмотрим их далее. " +
                    "После открытия найдем элемент Button и переместим его в любое место конструктора. Заметим, что как только кнопка закреписась на окне, изменилась и вёрстка: В ней появилась" +
                    "строка, начинающаяся с <Button... Более подробно об этом поговорим позже. \n\tТеперь выберем нашу кнопку, нажав на неё ЛКМ и нажмем F4. Справа открывается панель Свойства. Здесь мы можем увидеть все" +
                    " свойства и собития, присущие выбранному нами элементу (в данном случае кнопки). Нажмём Упорядочить -> Имя, и отыщем свойство Backgroung. Нажав на прямоугольное поле " +
                    "справа от него, мы откроем палитру, где сможем изменить цвет нашей кнопки. \n\tВ обозревателе решений (раздел в правой части экрана) можно увидеть все файлы и сборки проекта." +
                    " Среди прочих, особую важность представляет файл MainWindow.xaml, являющийся вёрсткой главного окна. Здесь хранится визуальная информация об окне приложения. \n\tНажав на значок развертывания списка (треугольник слева от названия), мы увидим файл " +
                    "MainWindow.xaml.cs, в котором содержится функциональный код на языке C#, принадлежащий главному окну. Все обработчики событий (рассмотрим далее), функции и переменные, необходимые для функционала приложения прописываются здесь, в классе главного окна:" +
                    "\n\tpublic partial class MainWindow : Window\n\t{\n\t\t...\n\t}", L3SP, T2);

                Vvedenie.Add(L3);

                //"Тест по теме\n\"Основы интерфейса\""
                Level T3 = new Level(6, "Тест по теме\n\"Основы интерфейса\"", "Что отображается в панели Свойства?",
                    new List<string>() { "Все свойства и события выбранного элемента", "Описание выбранного элемента", "Все элементы, доступные в установленной версии .NET", "Все файлы проекта" }, L3);

                Vvedenie.Add(T3);
            #endregion

            #region XAML
            //"Знакомство с XAML",
            StackPanel L4SP = new StackPanel();
            DropShadowEffect dropShadow = new DropShadowEffect() { BlurRadius = 15, Color = Colors.White, ShadowDepth = 0 };
                L4SP.Children.Add(new TextBox() {IsReadOnly = true,Effect = dropShadow, TextWrapping = TextWrapping.Wrap, Text = "<Window>\n\t<Grid>\n\t\t<Button x:Name = \"btn1\" Widht = 130 Height = 40 />\n\t</Grid>\n</Window>" });

                Level L4 = new Level(7, "Знакомство с XAML", "\n\tXAML (eXtensible Application Markup Language) - язык разметки, используемый для инициализации объектов в " +
                    "технологиях на платформе .NET. Применительно к WPF данный язык используется прежде всего для создания пользовательского интерфейса декларативным путем." +
                    "\n\tXAML вовсе не является обязательной составляющей, так как визуальную составляющую можно писать и в коде, однако XAML позволяет существенно облегчить работу создания" +
                    " красочного интерфейса, т.к. позволяет разделить работу дизайнера и программиста, а так же его легко поддерживать.\n\tРассмотрим как создавать элементы с помощью XAML." +
                    "\n\tКаждый элемент разметки должен иметь открытый и закрытый теги, на примере кнопки:\n\t\t<Button></Button>\n\tИли же сокращенная запись:\n\t\t<Button/>" +
                    "\n\tВ написании разметки есть четкая логика:" +
                    "\n\t* Вложенные элементы располагаются внутри поддерживаемых компонентов. Сначала идет элемент самого высшего уровня - Window, затем идет вложенный элемент " +
                    "Grid - контейнер для других элементов, и в нем уже определен наш элемент Button, представляющий кнопку." +
                    "\n\t* Для кнопки мы можем определить свойства в виде атрибутов. Здесь определены атрибуты x:Name (имя кнопки), Width, Height и Content. Имя элемента позволит нам обращаться " +
                    "к нему, и, соответственно, его полям, непосредственно из кода. Это нужно, например, для проверки введенных данных, анимации, специфичных действий с элементом и т.д. Более подробно " +
                    "все свойства элементов мы рассмотрим в разделе Элементы управления.", L4SP, T3);

                XAML.Add(L4);

                //"Задание по теме\n\"Знакомство с XAML\"",
                Level Z1 = new Level(8, "Задание по теме\n\"Знакомство с XAML\"", "Заполните все пропуски, чтобы объявить кнопку длиной 135 шириной 40 и именем btn_z в контейнере Grid в окне приложения.",
                    "<~Window~>\n <Grid>\n  <~Button~ Widht = 135 x:~Name~ = \"btn_z\" ~Height~ = 40 />\n </~Grid~>\n</Window>\n", L4);
                XAML.Add(Z1);

                //"Взаимодействие кода C# и XAML",
                StackPanel L5SP = new StackPanel();
                Button b = new Button() { Name = "Hello", Content = "btn z", Height = 40 };
                b.Click += B_Click;
                L5SP.Children.Add(b);

                Level L5 = new Level(9, "Взаимодействие кода C# и XAML", "В приложении часто требуется обратиться к какому-нибудь элементу управления. Для этого надо установить у элемента в XAML свойство Name. " +
                    "\n\tЕще одной точкой взаимодействия между xaml и C# являются события. С помощью атрибутов в XAML мы можем задать события, которые будут связанны с обработчиками в коде C#. Обработчики событий - " +
                    "методы кода, которые выполняются при определённом событии. Для кнопки есть события Click - событие нажатия. Чтобы создать обработик события, необходимо прописать название события в свойствах " +
                    "элемнта в коде разметки. Например, для кнопки:\n\n\t<Button x:Name = \"btn_z\" Click = \"btn_z_Click\" />\n\n\tВ коде C# автоматически сгенерируется каркас обработчика:" +
                    "\n\n\tprivate void btn_z_Click(object sender, RoutedEventArgs e)\n\t{\n\n\t}" +
                    "\n\n\tСделаем так, чтобы при нажатии на кнопку у нас всплывало диалоговое окно:" +
                    "\n\n\tprivate void BtnContinue_Click(object sender, RoutedEventArgs e)\n\t{\n\t\tMessageBox.Show(\"Hello\");\n\t}" +
                    "\n\n\tАргументами обработчика являются сам элемент, от которого произошло событие (sender) и особый для данного события аргумент, который отвечает за обработку. " +
                    "sender всегда типа object, поэтому, для взаимодействовия с ним, его нужно сначала привести к его изначальному типу." +
                    "\n\tК одному и тому же обработчику можно привязать сколько угодно элементов. Это используется, например, для проверки правильности данных во множесте текстовых полей программы, " +
                    "где одинаковы требования к валидации.", L5SP, Z1);

                XAML.Add(L5);

                //"Задание по теме\n\"Взаимодействие кода C# и XAML\"",
                Level Z2 = new Level(10, "Задание по теме\n\"Взаимодействие кода C# и XAML\"", "Заполните пробелы, чтобы получить обработчик события нажатия на кнопку, который выводит в диалоговом окне имя нажатой кнопки:",
                    "private void Btn_Click(~object~ sender, RoutedEventArgs e)\n{\n ~Message~Box.Show((sender as Button).~Name~);\n}\n", L5);

                XAML.Add(Z2);

                //"Создание элементов в коде C#",
                StackPanel L6SP = new StackPanel();
                Button B = new Button() { Name = "New_btn", Content = "NewBtn", Height = 40 };
                B.Click += B_Click;
                L6SP.Children.Add(B);

                Level L6 = new Level(11, "Создание элементов в коде C#", "\tКак уже было ранее сказано, графические элементы необязательно создавать именно в верстке. Их можно создавать программно." +
                    "\n\tНапример, у нас есть верстка:\n\n" +
                    "<Window>\n\t<Grid x:Name = \"LayoutGrid\">\n\n\t</Grid>\n</Window>" +
                    "\n\n\tКак видно, мы присвоили контейнеру Grid имя LayoutGrid. Теперь запишем в коде C# в конструкторе класса окна следующее:\n\n" +
                    " public MainWindow()\n{\nInitializeComponent();\nButton b = new Button()\n{\nContent = \"NewBtn\", \nName = \"New_btn\", \nWidht = 120, \nHeight = 40\n};\n b.Click += Btn_Click;\n LayoutGrid.Children.Add(b);\n}" +
                    "\n\n\tТак мы инициализировали кнопку с именем New_bth, длинной 120, высотой 40 и обработчиком события из предыдущего примера (Для того, чтобы он работал, его нужно прописать в классе окна приложения!) и " +
                    "поместили её в контейнер LayoutGrid" +
                    "\n\tЗапустив приложение, мы увидим, что на нашем окне появилась кнопка с заданными параметрами.", L6SP, Z2);

                XAML.Add(L6);

                //"Задание по теме\n\"Создание элементов через C#\""
                Level Z3 = new Level(12, "Задание по теме\n\"Создание элементов через C#\"", "Заполните пробелы, чтобы объявить и инициализировать кнопку, привязать её событие нажатия к обработчику btn_Click" +
                    " и поместить её в контейнер MainGrid:", "public MainWindow()\n{\n InitializeComponent();\n ~Button~ btn = new Button();\n btn.~Click~ += btn_Click;\n MainGrid.Children.Add(~btn~);\n}\n", L6);

                XAML.Add(Z3);
            #endregion

            #region КОМПОНОВКА
            //"Понятие компоновки",
            Image L7img = new Image();
            L7img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Container.png"));
            StackPanel L7SP = new StackPanel();
            L7SP.Children.Add(L7img);

            Level L7 = new Level(13, "Понятие компоновки", "\n\tКак вы уже заметили, все элементы должны быть помещены внутрь контейнеров - вместилищ для элементов, внутри которых действуют определённые" +
                " правила, присущие типу этого контейнера. Контейнеры сами по себе не имеют графического представления, по сути, это \"разметка\" областей, выделенных под элементы. \n\tАналогом из реальной жизни " +
                "является планировка дома: на схеме отображается, где будет зал, спальня, ванна, как они располагаются, какие у них размеры. Каждый контейнер является такой же схемой, и, так же, как и" +
                " в случае с планировкой дома, где можно составить подробные планировки отдельных комнат (какое будет расстояние между стеной и кроватью, шкафом и дверью), можно создавать контейнеры для " +
                "более подробного описания частей главного контейнера. \n\tУ каждого контейнера есть сойство Children, представляющий собой линейный список типа object. В нем хранятся все элементы данного контейнера." +
                "\n\tК тому же, у каждого элемента есть свойство Parent, в котором хранится ссылка на элемент (Чаще всего это контейнер), к которому он привязан. \n\tВАЖНО! Каждый элемент может быть привязан только к одному контейнеру. \n\tКомпоновкой называется" +
                " процесс расположения элементов на рабочей области визуальной составляющей программы. Процесс компоновки начинается с анализа, какой контейнер подойдет лучше всего для выполнения " +
                "функций программы. Особенности основных типов контейнеров мы рассмотрим в данной главе.", L7SP, Z3);

            Komponovka.Add(L7);

            //"Тест по теме\n\"Понятие компоновки\"",
            Level T4 = new Level(14, "Тест по теме\n\"Понятие компоновки\"", "Компоновка - это...", new List<string> { "Процесс разметки области, для дальнейшего заполнения её рабочими элементами",
            "Процесс планирования визуальной составляющей программы", "Процесс создания привязки элементов между собой", "Общее название элементов-контейнеров"}, L7);

            Komponovka.Add(T4);

            //"Контейнер Grid",
            Grid L8grd = new Grid() { ShowGridLines = true, Height = 150, HorizontalAlignment = HorizontalAlignment.Stretch};
            L8grd.RowDefinitions.Add(new RowDefinition());
            L8grd.RowDefinitions.Add(new RowDefinition());
            L8grd.RowDefinitions.Add(new RowDefinition());
            L8grd.ColumnDefinitions.Add(new ColumnDefinition());
            L8grd.ColumnDefinitions.Add(new ColumnDefinition());
            L8grd.ColumnDefinitions.Add(new ColumnDefinition());

            Button bt = new Button() { Content = "btn" };
            Grid.SetColumn(bt, 2);
            Grid.SetRow(bt, 1);
            L8grd.Children.Add(bt);

            StackPanel L8SP = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch };
            L8SP.Children.Add(L8grd);

            Level L8 = new Level(15, "Контейнер Grid", "\n\tЭто наиболее мощный и часто используемый контейнер, напоминающий обычную таблицу. С ним мы уже встречались, но не раскрыли его потенциал. Он содержит столбцы и строки, количество которых задает " +
                "разработчик. Для определения строк используется свойство RowDefinitions, а для определения столбцов - свойство ColumnDefinitions:\n\n<Grid>\n\t<Grid.RowDefinitions> " +
                "\n\t\t< RowDefinition/>    //это строчная дефиниция\n\t\t < RowDefinition />\n\t\t < RowDefinition />\n\t</ Grid.RowDefinitions >\n\t< Grid.ColumnDefinitions >\n\t\t< ColumnDefinition />" +
                "\n\t\t< ColumnDefinition />\n\t\t< ColumnDefinition />\n\t</ Grid.ColumnDefinitions >\n</Grid>\n\n\t" +
                "\n\tСтолбцы и строки можно определять в пропорциональном соотношении, чтобы при изменении размеров окна ваши элементы сохраняли пропорции. Для этого нужно прописать в свойствах RowDefinition " +
                "Height = \"n*\", где n - любое число (у столбцовой дефиниции - свойство Widht). По умолчанию, когда вы создаете дефиницию, её параметр (длинна или ширина) равна 1* или просто *." +
                "\n\tМожно указывать и явные размеры, для этого нужно прописать то же самое, только без *.\n\tЭтот контейнер применяют для первичной, жёсткой разметки, обозначив где будет располагаться какие элементы.\n\t" +
                "После того, как мы разрезали грид, мы можем помещать в него элементы в нужные нам ячейки, подобно матрице. Например, напишем после разделения дефиниций следующее:\n\n\t<Button Grid.Row = \"1\" Grid.Column " +
                "= \"2\"/>\n\n\tВ данном примере мы поместили нашу кнопку во 2 строку в 3 столбец нашего контейнера.", L8SP, T4);

            Komponovka.Add(L8);

            //"Тест по теме\n\"Контейнер Grid\"",
            Level T5 = new Level(16, "Тест по теме\n\"Контейнер Grid\"", "Допустим, у грида размечено достаточное количество ячеек. Где будет находиться кнопка, если ей прописать Grid.Row = \"3\" и Grid.Column = \"2\"?", 
                new List<string>() { "4 строка 3 столбец", "3 строка 2 столбец", "3 строка 4 столбец", "2 строка 3 столбец"}, L8);

            Komponovka.Add(T5);

            //"Контейнер StackPanel",
            StackPanel L9SP = new StackPanel();
            L9SP.Children.Add(new Button() { Content = "btn1" });
            L9SP.Children.Add(new Button() { Content = "btn2" });
            L9SP.Children.Add(new Button() { Content = "btn3" });

            Level L9 = new Level(17, "Контейнер StackPanel", "\n\tДанный контейнер представляет собой \"стек\" элементов. При добавлении элементов в стекпанель, они будут размещаться под предыдущим, один за другим.\n\t" +
                "Например:\n\n<StackPanel>\n\t<Button/>\n\t<Button/>\n\t<Button/>\n</StackPanel>\n\nИспользуется обычно для создания кнопок меню, текстовых полей в анкетах и местах, где нужно однотипно разместить " +
                "множество элементов.\n\tТакже можно изменить ориентацию стекпанели (по умолчанию элементы идут сверху-вниз) на горизонтальную, сделав так, что элементы будут располагаться слева-направо:" +
                "\n\n<StackPanel Orientation = \"Horizontal\">\n\t...\n</StackPanel>\n\n\tЯвным преимуществом является скорость компоновки, однако стекпанель не обеспечивает такой резиновости интерфейса, как у контейнера Grid.", L9SP, T5);

            Komponovka.Add(L9);

            //"Тест по теме\n\"Контейнер StackPanel\"",
            Level T6 = new Level(18, "Тест по теме\n\"Контейнер StackPanel\"", "Как располагаются элементы в стекпанели?", new List<string>() { "Поочередно, один за другим сверху-вниз или слева-направо", "В заданных ячейках", "Поочередно, один за другим снизу-вверх или справа-налево", "Ни один из вариантов не верный" }, L9);

            Komponovka.Add(T6);

            //"Контейнер WrapPanel",
            StackPanel L10SP = new StackPanel();
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Children.Add(new Button() { Content = "btn1", Width = 100 });
            wrapPanel.Children.Add(new Button() { Content = "btn2", Width = 100 });
            wrapPanel.Children.Add(new Button() { Content = "btn3", Width = 100 });
            wrapPanel.Children.Add(new Button() { Content = "btn4", Width = 100 });
            L10SP.Children.Add(wrapPanel);

            Level L10 = new Level(19, "Контейнер WrapPanel", "\n\tДанный контейнер очень похож на стекпанель, однако имеется несколько ключевых моментов. При добавлении элементов они будут располагаться слева-направо до тех пор, пока элементы не заполнят строку. Как только окажется, что последующий элемент не умещается в строке - он перенесется на следующую." +
                " Например:\n\n<WrapkPanel>\n\t<Button/>\n\t<Button/>\n\t<Button/>\n\t<Button/>\n\t<Button/>\n</WrapPanel>\n\n\tИспользуется редко, и для решения специфичных задач.", L10SP, T6);

            Komponovka.Add(L10);

            //"Тест по теме\n\"Контейнер WrapPanel\"",
            Level T7 = new Level(20, "Тест по теме\n\"Контейнер WrapPanel\"", "Как располагаются элементы в WrapPanel?", new List<string>() { "Поочередно, один за другим слева-направо, с переносом неумещающихся в строке элементов", "В заданных ячейках", "Поочередно, один за другим сверху-вниз или слева-направо", "Сверху-вниз с переносом элементов на следующие строки" }, L10);

            Komponovka.Add(T7);

            //"Контейнер Canvas",
            StackPanel L11SP = new StackPanel();
            Canvas cn = new Canvas() { Height = 200, Width = 400};
            Button bb1 = new Button() { Width = 80, Content = "btn1" };
            Button bb2 = new Button() { Width = 80, Content = "btn1" };
            Button bb3 = new Button() { Width = 80, Content = "btn1" };
            Canvas.SetLeft(bb1, 20);
            Canvas.SetLeft(bb2, 40); 
            Canvas.SetLeft(bb3, 55);
            Canvas.SetTop(bb1, 10);
            Canvas.SetTop(bb2, 30);
            Canvas.SetTop(bb3, 47);

            cn.Children.Add(bb1);
            cn.Children.Add(bb2);
            cn.Children.Add(bb3);

            L11SP.Children.Add(cn);

            Level L11 = new Level(21, "Контейнер Canvas", "\tДанный контейнер является неким подобием холста, и служит для того, чтобы задавать элементам определенное значение. Например:\n\n" +
                "<Canvas>\n\t<Button Canvas.Left = \"20\" Canvas.Top = \"10\"/>\n\t<Button Canvas.Left = \"40\" Canvas.Top = \"30\"/>\n\t<Button Canvas.Left = \"55\" Canvas.Top = \"47\"/>\n</Canvas>\n\n\t" +
                "Разумеется, ни о какой элестичности интерфейса с этим контейнером говорить нельзя, однако, у него другое применение - декоративное. При помощи задачи координат легко создавать анимации, удобно " +
                "размещать графические элементы и легче отслеживать положение мыши.", L11SP, T7);

            Komponovka.Add(L11);

            //"Тест по теме\n\"Контейнер Canvas\"",   
            Level T8 = new Level(22, "Тест по теме\n\"Контейнер Canvas\"", "Как располагаются элементы в контейнере Canvas?", new List<string>() { "Их положение задается при помощи координат", "В заданных ячейках", "Поочередно, один за другим сверху-вниз или слева-направо", "Слева-направо с переносом элементов на следующие строки" }, L11);

            Komponovka.Add(T8);

            //"Свойства компоновки элементов",
            StackPanel L12SP = new StackPanel();
            L12SP.Children.Add(new Button { HorizontalAlignment = HorizontalAlignment.Left, Content = "Left", Margin = new Thickness(0,20,0,0) });
            L12SP.Children.Add(new Button { HorizontalAlignment = HorizontalAlignment.Center, Content = "Center", Margin = new Thickness(0, 20, 0, 0) });
            L12SP.Children.Add(new Button { HorizontalAlignment = HorizontalAlignment.Right, Content = "Right", Margin = new Thickness(0, 20, 0, 0) });
            L12SP.Children.Add(new Button { HorizontalAlignment = HorizontalAlignment.Stretch, Content = "Stretch", Margin = new Thickness(0, 20, 0, 0) });

            Level L12 = new Level(23, "Свойства компоновки элементов", "\tКаждый элемент обладает свойствами компоновки - специальными свойствами, которые хранят в себе значения отступов, выступов, размеры, местоположения, ориентацию в пространсве и т.д.\n\t" +
                "Пожалуй, самыми частовстречающимися свойствами являются длинна и высота. С ними мы уже встречались и знаем как они работают. Их значения представленны типом double, а метрика исчисления по умолчанию производится в пикселях, однако возможно проводить метрику и в дюймах (Height = \"4.5in\"), и в сантиметрах (Height = \"21cm\")\n\t" +
                "Так же это свойство имеет особые поля Max- и Min- Height/Width, при помощи которых мы можем задать максимальную и, соответственно, минимальную длинну элемента. Обычно это свойство используется у окон, дабы не допустить неправильности отображения элементов при изменеии масштаба.\n\t" +
                "Следующим по важности идет свойство выравнивани HorizontalAlignment - по горизонтальной линии, и VerticalAlignment - по вертикальной. Тип данных этих свойств является перечисление, содержащее в себе название крайних точек (для вертикали - это верх и низ, а для горизонтали - лево и право), центр и растягивание Stretch. Выбрав одно из этих свойств, мы выбираем, к какой стороне будет привязана фигура, тем самым мы добьемся того, что при изменении размеров окна наш элемент будет иметь положение согласно заданым параметрам.\n\t" +
                "Отступы Margin задают, на каком расстоянии от краев будет находится текущий элемент. Задавать его можно тремя путями: \n\n\tMargin = \"5\" - в этом случае элемент отступит на 5 пикселей от всех краев\n\tMargin = \"5,10\" - в этом случае элемент будет находиться на расстоянии 5 пикселей от лева и права, и на 10 пикселей от верха и низа/\n\tMargin = \"5,10,15,0\" - в этом случае отступы отчитываются соответственно от лева, верха, права и низа, т.е. 5 пикселей от левой границы, 10 от верхней, 15 от правой и 0 от нижней.\n\n\t",
                L12SP, T8);

            Komponovka.Add(L12);

            //"Задание по теме\n\"Свойства компоновки элементов\""
            Level Z4 = new Level(24, "Задание по теме\n\"Свойства компоновки элементов\"", "Заполните пропуски, чтобы создать две кнопки, первую выровнять по левому краю, с отступами 10 со всех сторон, вторую - по правому, с отступами 10 справа и слева и с отступами 8 сверху и снизу. Поместите их в контейнер стекпанели, отриентированной горизонтально.",
                "< StackPanel Orientation = \"~Horizontal~\" >\n  < Button x:Name = \"btn1\" ~Horizontal~Alignment = \"~Left~\" Margin = \"~10~\" />\n  < Button x:Name = \"btn2\" ~Horizontal~Alignment = \"~Right~\" Margin = \"~10,8~\" />\n</ StackPanel >\n", L12);

            Komponovka.Add(Z4);
            #endregion

            #region ЭЛЕМЕНТЫ УПРАВЛЕНИЯ
            //"Основные свойства элементов управления",
            StackPanel L13SP = new StackPanel();
            L13SP.Children.Add(new Button() { Height = 60, Content = "btn" });
            L13SP.Children.Add(new CheckBox(){ Height = 60, Content = "chkBtn", Foreground = Brushes.Cyan, FontSize=20 });
            L13SP.Children.Add(new RadioButton() { Height = 60, Content = "radBtn", Foreground= Brushes.Cyan, FontSize = 20 });

            Level L13 = new Level(25, "Основные свойства элементов управления", "\tДля начала уточним, что к элементам управления относятся все элементы, с которыми может взаимодействовать пользователь. Это кнопки, слайдеры, раскрывающиеся списки, и т.д.\n\t" +
                "У каждого типа элементов управления есть свои особенности. Про особенности каждого элемента мы поговорим в этом разделе, а пока выделим основные свойства, присущие КАЖДОМУ элементу управления:\n\n\t" +
                "* Name - свойство, определяющее название элемента. Задав элементу имя, мы получим к нему прямой доступ из кода. Свойчтва Name и x:Name в коде разметки имеют равносильные значения.\n\n\t" +
                "* Visible - определяет роль элемента в сборке. Тип данных - перечисление: \n\tCollapsed означает, что элемент не учавствует в сборке, т.е. он не отображается и незанимает место в контейнерах. \n\tHiden означает, что элемент занял свое место в контейнере, но не отображается\n\tVisible означает, что элемент полноценно выполняет свои функции - и отображается, и занимает место в контейнере\n\n\t" +
                "* Opacity - определяет степень прозрачности элемента. Значение от 0 (полностью прозрачен) до 1 (полностью виден).\n\t" +
                "* Tag - свойство типа object, в него можно положить всё, что угодно. Данное свойство очень полезно для разработчиков, так как позволяет хранить в свойстве Tag ссылки на другие объекты. Данное свойство не имеет графического представления, и предназаначено для разработчиков.\n\t" +
                "* Background - определяет окрас данного элемента. Значение этого свойства имеет тип Brush, о котором мы поговорим в разделе Кисти.", L13SP, Z4);

            ElmYpravlenia.Add(L13);

            //"Тест по теме\n\"Основные свойства элементов управления\"",
            Level T9 = new Level(26, "Тест по теме\n\"Основные свойства элементов управления\"", "Что можно хранить в поле Tag?", new List<string> {"Всё, что угодно", "Только ссылки на другие экземпляры", "Значение прозрачности элемента", "Предметы, которые лучше никогда никому не показывать" }, L13);

            ElmYpravlenia.Add(T9);

            //"Простые кнопки (Button)",
            StackPanel L14SP = new StackPanel();
            Button BbB = new Button() { Width = 140, Height = 80, Background = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center};
            BbB.Content = new Button() { Background = Brushes.Red, Width = 70, Height = 40 };
            L14SP.Children.Add(BbB);

            Level L14 = new Level(27, "Простые кнопки (Button)", "\tС этим элементом управления мы с вами уже сталкивались. Простые кнопки являются универсальными, их можно использовать абсолютно во всех случаях.\n\t" +
                "Их легко настраивать, изменять их стиль, поведение. Содержимое кнопки представлено полем Content типа object, что значит, что содержимым кнопки мы можем сделать абсолютно все (это относится ко всем элементам, содержимое которых представлено свойством Content).\n\t" +
                "Давайте сделаем кнопку с кнопкой внутри:\n\n" +
                "<Button x:Name = \"BTN\" Background = \"White\">\n  <Button x:Name = \"btn\" Background = \"Red\"/>\n</Button>\n\n\t" +
                "Вместо кнопки, внутрь мы можем положить контейнер , в который сможем добавить сколько угодно элементов. Разумеется, данное поле сделали типа object не для таких \"ИЗВРАЩЕНИЙ\", а чтобы внутрь кнопки можно было вставлять изображения (Этот элемент рассмотрим позднее в этом разделе).", L14SP, T9);

            ElmYpravlenia.Add(L14);

            //"Тест по теме\n\"Простые кнопки (Button)\"",
            Level T10 = new Level(28, "Тест по теме\n\"Простые кнопки (Button)\"", "В каких случаях используются простые кнопки (Button)?", new List<string>() { "Возможно использовать во всех случаях, т.к. они являются стандартными", "Если нужно положить кнопку внутрь кнопки", "Только в случае крайней необходимости", "В случаях пожара"}, L14);

            ElmYpravlenia.Add(T10);

            //"Кнопки-флаги (CheckBox и Radiobutton)",
            StackPanel L15SP = new StackPanel() { Background = Brushes.White};
            L15SP.Children.Add(new Label() { Content = "Радиокнопки", FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L15SP.Children.Add(new RadioButton() { Content = "RadioButton1", FontSize = 16, IsChecked = true });
            L15SP.Children.Add(new RadioButton() { Content = "RadioButton2", FontSize = 16 });
            L15SP.Children.Add(new RadioButton() { Content = "RadioButton3", FontSize = 16 });
            L15SP.Children.Add(new Label() { Content = "\nЧекбоксы", FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L15SP.Children.Add(new CheckBox() { Content = "CheckBox1", FontSize = 16, IsChecked = true });
            L15SP.Children.Add(new CheckBox() { Content = "CheckBox2", FontSize = 16 });
            L15SP.Children.Add(new CheckBox() { Content = "CheckBox3", FontSize = 16 });

            Level L15 = new Level(29, "Кнопки-флаги", "\tБывают случаи, когда нужно предоставить пользователю видеть текущий выбор, или же выборы, если их несколько. Для этого лучше всего подойдут элементы Radiobutton и CheckBox соответственно.\n\t" +
                "Содержимое этих элементов так же представлено свойством Content (что из этого следует - смотрите в предыдщей главе). У этих элементов тоже есть событие Click, которое можно точно так же обработать, как и у обычной кнопки. \n\tОсновное свойство данных элементов - IsChecked, поле типа bool, обозначающее, выбран ли этот элемент (true) или нет (false).\n\t" +
                "Создадим 3 радиокнопки с выделенной первой кнопкой по умолчанию:\n\n" +
                "<StackPanel>\n <RadioButton Content = \"RadioButton1\" IsChecked = \"true\" />\n <RadioButton Content = \"RadioButton2\" />\n <RadioButton Content = \"RadioButton3\" />\n</StackPanel>\n\n\t" +
                "Попробуем попереключать их. Заметим, что выбор радиокнопки нельзя отменить, нажав на неё повторно, зато, если мы нажмем на другую радиокнопку, выбор перенесется на неё и исчезнет с текущей. Данное правило (по переносу выбора) действует на все радиобаттоны внутри текущего контейнера.\n\n\tТеперь рассмотрим чекбоксы. Так же создадим три экземпляра и пометим первый как выбранный по умолчанию:\n\n" +
                "<StackPanel>\n <CheckBox Content = \"CheckBox1\" IsChecked = \"true\" />\n <CheckBox Content = \"CheckBox2\" />\n <CheckBox Content = \"CheckBox3\" />\n</StackPanel>\n\n\t" +
                "Заметим, что, в отличии от радиобаттонов. чекбоксы свободно отменяют свой выбор и не скидывают свое значение при выборе другого чекбокса. \n\tНе сложно представить где используются эти элементы - опросы, панель с настройками, анкетирование и т.д.", L15SP, T10);

            ElmYpravlenia.Add(L15);

            //"Тест по теме\n\"Кнопки-флаги (CheckBox и Radiobutton)\"",
            Level T11 = new Level(30, "Тест по теме\n\"Кнопки-флаги\"",
                "Допустим, пользователю необходимо выбрать список продуктов, которые он хочет приобрести в магазине, и кассу, на которой он желает оплатить покупки. Какие элементы подойдут для описания модели поведения программы?",
                new List<string>() { "CheckBox для продуктов и Radiobutton для кассы", "Radiobutton для продуктов и CheckBox для кассы", "CheckBox и для касс и для продуктов", "RadioButton и для касс и для продуктов" },L15);

            ElmYpravlenia.Add(T11);

            //"Всплывающие подсказки (ToolTip)",
            StackPanel L16SP = new StackPanel();
            Button bBb = new Button() { Width = 140, Height = 100, Content = "Наведи сюда"};
            bBb.ToolTip = new ToolTip() { Content = "Прикинь, это кнопка!" };
            L16SP.Children.Add(bBb);

            Level L16 = new Level(31, "Всплывающие подсказки", "\tИногда, особенно в сложных интерфейсах, необходимо сделать некоторые пояснения, для чего нужен тот или иной элемент, но слишком мало места в интерфейсе, чтобы вставлять письменные описания. Для решения этой проблемы были созданы всплывающие описания.\n\t" +
                "Создадим кнопку с таким описанием:\n\n" +
                "<Button Content = \"Наведи сюда\">\n  <Button.ToolTip>\n    <ToolTip Content = \"Прикинь, это кнопка!\"/>\n  </Button.ToolTip>\n</Button>\n\n\t" +
                "Как мы заметили, содержимое всплывающей подсказки представлено свойством Content...\n\tВсплывающее описание можно сделать практически для всех элементов платформы WPF.", L16SP, T11);

            ElmYpravlenia.Add(L16);

            //"Задание по теме\n\"Всплывающие подсказки (ToolTip)\"",
            Level Z5 = new Level(32, "Задание по теме\n\"Всплывающие подсказки\"", "Заполните пропуски, чтобы сделать всплывающее " +
                "описание с содержимым \"А это мы рассмотрим позже\" у прямоугольника rectangle", "<Grid>\n<Rectangle x:Name = \"rectangle\">" +
                "\n<Rectangle.~ToolTip~>\n  <ToolTip ~Content~ = \"А это мы рассмотрим ~позже~\"/>\n</Rectangle.~ToolTip~>\n</Rectangle>\n</Grid>", L16);

            ElmYpravlenia.Add(Z5);

            //"Создание прокрутки (ScrollViewer)",
            StackPanel L17SP = new StackPanel();
            for (int i = 0; i < 10; i++)
            {
                L17SP.Children.Add(new Button() { Content = $"Btn{i}", Width=140, Height = 80 });
            }

            Level L17 = new Level(33, "Создание прокрутки", "\tВ случае, если у вас слишком много контента, который нужно разместить на ограниченном пространстве, или же вы хотите обеспечить гибкость программы при внедрении в нее в дальнейшем модификаций и навых данных, то вас однозначно заинтересует элемент ScrollViever, представляющий собой прокрутку. \n\t" +
                "Содержимое прокрутки представлено свойством Content, вмещающее в себя только один элемент. Значит, если мы хотим сделать эту прокрутку для нескольких элементов, нам надо создать контейнер, который поместим в ScrollViever, и уже в этот контейнер будем помещать элементы.\n\t" +
                "Как правило, она нужна в тех случаях, когда у нас контент большого объема, или же генерируется множество элементов.\n\tСоздадим имитацию второго случая. Допустим, в нашем приложении в разметке помечен контейнер с именем \"SP\", и нам надо поместить в него 10 кнопок, однако мы заранее знаем, что в своем обычном виде они туда не полезут, поэтому создаем в коде C# (т.к. нам надо сгенерировать целых !!10!! элементов) следующую конструкцию:\n\n" +
                "ScrollViewer SV = new ScrollViewer();   //создаем экземпляр прокрутки\nStackPanel stackPanel = new StackPanel();    //создаем контейнер для элементов\n//Заполняем контейнер кнопками\nfor (int i = 0; i < 10; i++)\n{\nstackPanel.Children.Add(new Button() { Content = $\"Btn{i}\", Width = 140, Height = 80 });\n}\nSV.Content = stackPanel;        //Помещаем контейнер в прокрутку\nSP.Children.Add(SV);        //помещаем прокрутку в главный контейнер, определенный в разметке\n\n" +
                "\tРазумеется, можно размещать прокрутку сразу в верстке, все делается индивидуально под ваши задачи:\n\n" +
                "<Grid>\n<ScrollViever>\n<StackPanel>\n<Button/>\n<Button/>\n<Button/>\n<Button/>\n<Button/>\n<Button/>\n<Button/>\n</StackPanel>\n</ScrollViever>\n</Grid>\n\n\t", L17SP, Z5);

            ElmYpravlenia.Add(L17);

            //"Задание по теме\n\"Создание прокрутки\"",
            Level Z6 = new Level(34, "Задание по теме\n\"Создание прокрутки\"", "Заполните пропуски, чтобы программно создать прокрутку, поместить в неё контейнер StackPanel, заполнить его в цикле 20-ю кнопками, и поместить готовую прокрутку внутрь контейнера LayoutGrid",
                "~ScrollViever~ scrollViever = new ~ScrollViever~();\nStackPanel stackPanel = new ~StackPanel()~;\nfor (int i = 0; i < ~20~; i++)\n{\n~stackPanel~.Children.Add(new ~Button~());\n}\n~scrollViever~.Content = stackPanel;\nLayoutGrid.Children.Add(~scrollViever~);\n", L17);

            ElmYpravlenia.Add(Z6);

            //"Текстовые элементы управления",
            StackPanel L18SP = new StackPanel() { Background = Brushes.White};
            L18SP.Children.Add(new Label() { Content = "TextBox", FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L18SP.Children.Add(new TextBox() { Text = "Сюда можно вводить данные", FontSize = 20, BorderBrush = Brushes.Black, BorderThickness = new Thickness(0,0,0,3), Foreground = Brushes.Silver,FontFamily = new FontFamily("Stencil") });
            L18SP.Children.Add(new Label() { Content = "TextBlock", FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L18SP.Children.Add(new TextBlock() { Text = "А сюда нельзя", FontSize = 20 });
            L18SP.Children.Add(new Label() { Content = "Label", FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L18SP.Children.Add(new Label() { Content = new Button() { Content = "Кнопка в элементе Label" } });

            Level L18 = new Level(35, "Текстовые элементы управления", "\tРедко какое приложение обходится без текстовых данных. Как минимум название приложения должно представлять из себя текст, а порой необходимо не только выводить текст, но и просить пользователя ввести свои текстовые данные (причем просить лучше как маленьких детишек, чтобы они точно поняли куда и что ввести, чтобы ничего не сломалось)\n\t" +
                "Для решения этих задач существуют специальные элементы управления - TextBox, TextBlock и Label.\n\n\t" +
                "TextBox предназначен для ввода данных. Его значение представлено полем Text типа string, а так же возможна настройка шрифта: Foreground определяет цвет текста, FontSize - размер шрифта, FontFamily - стиль текста, MaxLenght - максимальное количество символов, которое запишется в текстовое поле, BorderThickness - толщина границ, имеет такое же определение, к и у свойства Margin (рассматривали в разделе Компоновка), BorderBrush - определяет цвет границ, значение представлено типом Brush (как ни удивительно о.О). По умолчанию текст идет в одну строчку, однако, если вы работаете с большими объемами, то можно установить свойство TextWrapping=\"Wrap\", и тогда неумещающиеся слова будут переноситься на другую строчку.\n\t" +
                "Давайте создадим текстовое поле с прозраным фоном, подчеркнутой нижней границей и шрифтом с настройками цвета, размера шрифта и стилем:\n" +
                "<TextBox Background = \"Transparent\" BorderThickness = \"0,0,0,3\" BorderBrush = \"Black\" Foreground = \"Silver\" FontSize = \"20\" FontFamily = \"Stencyl\" />\n\n\t" +
                "*от автора: Обращаю ваше внимание, что большинство шрифтов не поддерживаются на русской раскладке.\n\n\t" +
                "TextBlock предназначен для вывода текста на экран. Имеет абсолютно такие же свойства, как и у элемента TextBox. Содержимое представлено так же свойством Text типа string." +
                "\n\tВНИМАНИЕ!! Копировать текст из элемента TextBlock НЕЛЬЗЯ, но если это необходимо реализовать, то можно создать TextBox и задать ему свойство IsReadOnly=\"True\", это даст соответствующий эффект.\n\n\t" +
                "Label предназначен для вывода абсолютно любого объекта: начиная от текста, и заканчивая... Чем угодно! Его содержимое представлено полем Content, а с ним мы уже знакомы... Тем ни менее, используется этот элемент в основном для вывода текста.", L18SP, Z6);

            ElmYpravlenia.Add(L18);

            //"Тест по теме\n\"Текстовые элементы управления\"",
            Level T12 = new Level(36, "Тест по теме\n\"Текстовые элементы управления\"", "В чем главное отличие элементов TextBox от TextBlock?",
                new List<string>() { "В TextBox можно вводить данные, а в TextBlock нельзя", "В TextBlock можно вводить данные, а в TextBox нельзя", "Отличий нет","Из элемента TextBlock можно копировать текст, а из TextBox нельзя"}, L18);

            ElmYpravlenia.Add(T12);

            //"Раскрывающийся список (Combobox)",
            StackPanel L19SP = new StackPanel();
            
            cb.Items.Add(new TextBlock() { Text = "Первый элемент", FontSize = 20 });
            cb.Items.Add(new TextBlock() { Text = "Второй элемент", FontSize = 20 });
            cb.Items.Add(new TextBlock() { Text = "Третий элемент", FontSize = 20 });
            cb.Items.Add(new TextBlock() { Text = "Четвертый элемент", FontSize = 20 });
            cb.Items.Add(new TextBlock() { Text = "Пятый элемент", FontSize = 20 });
            cb.SelectionChanged += cbx_SelectionChanged;
            L19SP.Children.Add(cb);

            Level L19 = new Level(37, "Раскрывающийся список (Combobox)", "\tРаскрывающийся список - очень полезный элемент, если вам надо ограничить выбор пользователя только определенными заданными элементами.\n\t" +
                "В WPF этот элемент представлен как ComboBox. Содержимое комбобокса определено линейным списком Items, в котором хранятся все элементы, вложенные в него. Это свойство подобно свойству Children у контейнеров.\n\t" +
                "Основные свойства данного элемента:\n\n\t" +
                "SelectedItem - поле типа object, возвращает выбранный элемент из списка, или NULL, если не выбранно ничего.\n\n\t" +
                "SelectedIndex - поле типа int. Возвращает индекс выбранного элемента, или -1, если ничего не выбрано.\n\n\t" +
                "Основное событие для данного элемента - SelectionChanged, срабатывает каждый раз, когда мы выбираем новый элемент в комбобоксе.\n\t" +
                "Давайте создадим элемент Combobox и обработаем событие изменения элемента:\n\n" +
                "XAML:\n\n" +
                "<Combobox x:Name = \"cbx\" SelectioChanged = \"cbx_SelectionChanged\">\n<TextBlock Content = \"Первый элемент\"/>\n<TextBlock Content = \"Второй элемент\"/>\n<TextBlock Content = \"Третий элемент\"/>\n<TextBlock Content = \"Четвертый элемент\"/>\n<TextBlock Content = \"Пятый элемент\"/>\n</Combobox>\n\n" +
                "C#:\n\n" +
                "private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)\n{\nMessageBox.Show(\"Ты выбрал \" + (cbx.SelectedItem as TextBlock).Text);\n}", L19SP, T12);

            ElmYpravlenia.Add(L19);

            //"Задание по теме\n\"Раскрывающийся список (Combobox)\"",
            Level Z7 = new Level(38, "Задание по теме\n\"Раскрывающийся список\"", "Допустим в коде разметки у нас определен комбобокс с именем cbx, обработчиком изменения элемента cbx_SelectionChanged и пятью элементами типа TextBox в качестве содержимого. Заполните пропуски, чтобы при обработке этого события выводился в диалоговом окне индекс выбранного элемента",
                "private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)\n{\nMessageBox.Show(cbx.~SelectedIndex~.~ToString~());\n}\n", L19);
           
            ElmYpravlenia.Add(Z7);

            //"Создание вкладок (TabControl)",
            StackPanel L20SP = new StackPanel();
            TabControl TC = new TabControl() { Height = 200};
            TC.Items.Add(new TabItem() { Header = "вкладка 1", Content = new Button() { Content = "Кнопка на вкладке 1" } });
            TC.Items.Add(new TabItem() { Header = "вкладка 2", Content = new Button() { Content = "Кнопка на вкладке 2" } });
            TC.Items.Add(new TabItem() { Header = "вкладка 3", Content = new Button() { Content = "Кнопка на вкладке 3" } });
            L20SP.Children.Add(TC);

            Level L20 = new Level(39, "Создание вкладок (TabControl)", "\tВсем нам хорошо известные из браузеров вкладки. \n\tХоть используются они не так часто, однако, им можно найти применение.\n\t" +
                "Содержимое элемента TabControl представлено списком Items, в котором хранятся элементы TabItem - непосредственно сами вкладки, они то и представляют для нас интерес.\n\t" +
                "Каждый элемент TabItem содержит поля Header - то, что будет отображаться в заголовке, представленно типом данных object, что означает, что туда можно положить все, что угодно, и Content - непосредственно само содержимое вкладки.\n\t" +
                "Давайте создадим табконтрол с тремя вкладками, в которые поместим кнопки:\n\n" +
                "<TabControl>\n<TabItem Header = \"вкладка 1\">\n<Button Content = \"Кнопка на вкладке 1\"/>\n</TabItem>\n\n<TabItem Header = \"вкладка 2\">\n<Button Content = \"Кнопка на вкладке 2\"/>\n</TabItem>\n\n<TabItem Header = \"вкладка 3\">\n<Button Content = \"Кнопка на вкладке 3\"/>\n</TabItem>\n</TabControl>\n\n" +
                "", L20SP, Z7);

            ElmYpravlenia.Add(L20);

            //"Тест по теме\n\"Создание вкладок (TabControl)\"",
            Level T13 = new Level(40, "Тест по теме\n\"Создание вкладок\"", "Какие элементы являются содержимым элемента TаbControl?", 
                new List<string>() {"TabItem", "Абсолютно любые","Все, что указаны в свойстве Content", "Все, что указаны в свойстве Header" }, L20);

            ElmYpravlenia.Add(T13);

            //"Ползунки (ProgressBar и Slider)",
            StackPanel L21SP = new StackPanel();

            ProgressBar PB = new ProgressBar() { Value = 0, Width = 200, Height = 10, Foreground = Brushes.Cyan };
            Slider SD = new Slider() { Minimum = 0, Maximum = 100, Width = 200 };

            Binding bdn = new Binding();
            bdn.Source = SD;
            bdn.Path = new PropertyPath("Value");
            PB.SetBinding(ProgressBar.ValueProperty, bdn);

            L21SP.Children.Add(new Label() { Content = "ProgressBar", Background = Brushes.White, FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center });
            L21SP.Children.Add(PB);
            L21SP.Children.Add(new Label() { Content = "Slider", Background = Brushes.White, FontSize = 20, Margin = new Thickness(0,20,0,0), HorizontalAlignment = HorizontalAlignment.Center });
            L21SP.Children.Add(SD);

            Level L21 = new Level(41, "Ползунки (ProgressBar и Slider)", "\tВсем нам знакомые полосы загрузки и ползунки громкости. Эти элементы представлены двумя типами: ProgressBar и Slider. Отличие между ними такое же, как и у TextBox и TextBlock - один создан для принятия входных данных, а второй - только для вывода.\n\t" +
                "Разберем поподробнее каждый элемент:\n\n\t" +
                "ProgressBar  - элемент, предназначенный для вывода информации. Его текущее значение представлено полем Value типа double, принимающее значения от 0 до 100 включительно. Цвет ползунка можно изменить, задав параметр полю Foreground.\n\n\t" +
                "Slider предназначен для ввода данных, путем передвижения ползунка. Его текущее значение представлено полем Value типа double. Этому элементу можно задать максимум (полем Maximum) и минимум (поле Minimum). Также данный элемент имеет событие изменения значения ValueChanged.\n\n\t" +
                "Давайте создадим оба этих элемента и зададим зависимость значения прогрессбара от значения слайдера:\n\n" +
                "XAML:\n" +
                "<StackPanel>\n< ProgressBar x: Name = \"PB\" Height = \"10\" Width = \"100\" Value = \"0\" Foreground = \"Cyan\" />\n< Slider x: Name = \"SD\" Maximum = \"100\" Minimum = \"0\" Width = \"100\" Value = \"0\" ValueChanged = \"SD_ValueChanged\" />\n</ StackPanel > \n\n" +
                "C#:\n" +
                " private void SD_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)\n{\nPB.Value = SD.Value;\n}\n\n\t" +
                "В обработчике мы определили, что при изменении значения слайдера, его значение присваивается прогрессбару, итог мы видим: при передвижении ползунка у нас изменяется значение прогрессбара.", L21SP, T13);

            ElmYpravlenia.Add(L21);

            //"Задание по теме\n\"Ползунки (ProgressBar и Slider)\"",
            Level Z8 = new Level(42, "Задание по теме\n\"ProgressBar и Slider\"","Заполните пропуски, чтобы получить правильный текст", "Элемент ProgressBar предназначен для ~вывода~ данных, в отличие от элемента ~Slider~.\nТекущее значение у обоих элементов представлено полем ~Value~\nСвойство ~Foreground~ позволяет изменить цвет полосы загрузки у элемента ProgressBar\nСобытие Value~Changed~ активируется каждый раз при изменении значения слайдера\n", L21);

            ElmYpravlenia.Add(Z8);

            //"Изображения (Image)",
            Level L22 = new Level(43, "Изображения (Image)", "\tПоследним рассмотренным нами элементом в этой главе будет изображение (Image).\n\tСодержимым данного элемента является свойство Source, представляющий из себя адрес к нужному элементу.\n\tНапример:\n\n" +
                "<Image Source = \"C:\\Users\\user\\Desktop\\1.png\" />\n\n\t" +
                "В данном случае загрузится изображение 1.png, расположеное на рабочем столе.\n\t" +
                "Однако указывать абсолютные пути крайне не рекомендовано, т.к. программа должна работать на любом компьютере, а не только на том, где на рабочем столе есть файл 1.png.\n\t" +
                "Если же вы решили использовать в своей программе сторонние файлы (к примеру те же изображения), то их рекомендуется включать в проект. Для этого открываем обозреватель решений, нажимаем правой кнопкой мыши по проекту (НЕ ПУТАТЬ С РЕШЕНИЕМ!) и переходим Добавить -> Существующий элемент. Откроется файловый проводник, где мы сможем выбрать нужный файл (по умолчанию вы видите только файлы Visual C#, чтобы увидеть все файлы, в правом нижнем углу стоит выбрать раздел Все файлы), после чего нажмем на кнопку Добавить.\n\t" +
                "Теперь данный файл является частью проекта, и доступ к нему можно получить следующим образом:\n\n" +
                "<Image Source = \"1.png\" />\n\n\t" +
                "Однако стоит учитывать, что любой файл, добавленный в проект будет собран вместе с ним, что увеличит вес вашего приложения, так что не стоит копить ненужные файлы и сразу удалять ненужные (пкм по элементу -> Удалить -> Ок).\n\n\t" +
                "Изображения используются в основном как фон, ибо компилятору легче загрузить изображение, нежели самому отрисовывать кучу элементов.\n\n\t" +
                "ВНИМАНИЕ!!! Лучше всего использовать оригинальные (сделание вами самими изображениями) или же картинками, находящимися в свободном доступе, ибо АВТОРСКИЕ ПРАВА НИКТО НЕ ОТМЕНЯЛ, и из-за одной картинки можно сесть на 2 года, помните об этом.", L1SP, Z8);

            ElmYpravlenia.Add(L22);

            //"Задание по теме\n\"Изображения (Image)\""
            Level Z9 = new Level(44, "Задание по теме\n\"Изображения (Image)\"", "Заполните пропуски, чтобы создать кнопку с изображением 1.png внутри", 
                "<~Button~>\n" +
                "<Image ~Source~ = \"~1.png~\" />" +
                "\n<~/Button~>\n", L22);

            ElmYpravlenia.Add(Z9);
            #endregion

            #region МОДЕЛЬ СОБЫТИЙ
            //"Маршрутизация событий",
            StackPanel L23SP = new StackPanel();
            Button L23B = new Button() { Name = "Button_Click", Content = "Кнопка с обработчиком нажатия" };
            L23B.Click += B_Click;
            L23SP.Children.Add(L23B);

            Level L23 = new Level(45, "Маршрутизация событий", "\tКак мы уже видели ранее, события - это метод, который активируется при определенном событии.\n\t" +
                "Например, событие нажатия на кнопку (Click) активируется при её нажатии, событие изменения значения у слайдера (ValueChanged) активируется при изменении его значения и т.д.\n\t" +
                "Все события можно разделить на несколько больших групп:\n*  События мыши;\n*  События клавиатуры;\n*  События стилуса;\n*  События сенсорной панели;\n*  События жизненного цикла;\n\n\t" +
                "Так же можно разделить и по типу действия.\n\t" +
                "У некоторых событий, например у события нажатия кнопки KeyDown есть аналог PreviewKeyDown, который выполняет по сути те же функции, но с одним отличием: события, начинающиеся с Preview- выполняются ДО того, как действие будет совершено.\n\t" +
                "То есть при событии KeyDown у элемента TextBox, нажатие на клавишу сначала запишет символ, а только потом вызовет обработчик.\n\t" +
                "А в случае с его Preview-аналогом - он сначала вызовет обработчик события, где можно будет проверить что именно пользователь нажал, а только потом запишет этот символ, если в обработчике не прервется анализ.\n\t" +
                "Как подключать обработчики события мы уже разобрали, однако давайте повторим:\n\n" +
                "XAML:\n" +
                "<Button Click=\"Button_Click\" />\n\n" +
                "C#:\n" +
                "private void btn_Click(object sender, RoutedEventArgs e)\n{}\n\n\t" +
                "Oбработчики нужны нам, чтобы задавать определенное поведение программы при определенных действиях пользователя.\n\t" +
                "Cобытия жизненного цикла.\n\tЭто события, возникающие при определённых ситуациях жизненного цикла элемента: начало загрузки (Loading), конец загрузки (Loaded), начало закрытия (Closing), завершение закрывания (Closed). \n\n\t" +
                "Чаще всего используются эти события либо для того, чтобы сохранять файлы при выходе из приложения, либо для того, чтобы инициализировать программные элементы при запуске приложения, либо для создания анимации.", L23SP, Z9);

            ModelSobitii.Add(L23);

            //"Тест по теме\n\"Маршрутизация событий\"",
            Level T14 = new Level(46, "Тест по теме\n\"Маршрутизация событий\"", "Что такое события?", new List<string>() { "Методы, которые вызываются при происшествии определенного действия над элементом","Элемент платформы WPF","Методы, определенные пользователем","То, что нужно отметить!"}, L23);

            ModelSobitii.Add(T14);

            //"События мыши",
            StackPanel L24SP = new StackPanel();
            Button L24B = new Button() { Content = "Обработка MouseEnter и MouseLeave", Margin = new Thickness(0,20,0,0)};
            L24B.MouseEnter += L24B_MouseEnter;
            L24B.MouseLeave += L24B_MouseLeave;
            L24SP.Children.Add(TBTB);
            L24SP.Children.Add(L24B);

            Level L24 = new Level(47, "События мыши", "\tК этим событиям, как и следует из названия, относятся все события мыши, среди них уже известное нам событие Click.\n\t" +
                "Основные используемые события, помимо Click являются события MouseEnter и MouseLeave, происходящие когда мышка заходит на территорию элемента, и когда выходит за её границы соответственно.\n\t" +
                "Давайте создадим кнопку, которая будет выводить на текстовое поле информацию о том, находится ли мышка на её территории, или же вне её:\n\n" +
                "Изначально в коде разметки у нас имеется контейнер LayoutGrid, а у главного окна определен обработчик окончания загрузки MainWindow_Loaded.\nC#:\n" +
                "TextBlock TB;      //объявляем как глобальную переменную\n\n" +
                "private void MainWindow_Loaded(object sender, RoutedEventArgs e)\n" +
                "{\n" +
                "StackPanel SP = new StackPanel();\n" +
                "//инициализируем текстовое поле\n" +
                "TB = new TextBlock() \n{ \nBackground = Brushes.White, \nFontSize = 20, \nWidth = 260, \nHeight = 40, \nText=\"Мышка НЕ над кнопкой\" \n};\n" +
                "\n//инициализируем кнопку\n" +
                "Button btn = new Button() \n{ \nContent = \"Обработка MouseEnter и MouseLeave\", \nMargin = new Thickness(0,20,0,0)\n};\n" +
                "//Задаем кнопке соответствующие обработчики событий\n" +
                "btn.MouseEnter += btn_MouseEnter;\n" +
                "btn.MouseLeave += btn_MouseLeave;\n\n" +
                "//Добавляем все элементы в нужные контейнеры\n" +
                "SP.Children.Add(TBTB);\n" +
                "SP.Children.Add(L24B);\n" +
                "LayoutGrid.Children.Add(SP);\n" +
                "}\n\n" +
                "//объявляем нужные обработчики\n" +
                "private void btn_MouseLeave(object sender, MouseEventArgs e)\n" +
                "{" +
                "\nTB.Text = \"Мышка НЕ над кнопкой\";" +
                "\n}\n\n" +
                "private void btn_MouseEnter(object sender, MouseEventArgs e)\n" +
                "{\n" +
                "TB.Text = \"Мышка над кнопкой\";\n" +
                "}", L24SP, T14);

            ModelSobitii.Add(L24);

            //"Задание по теме\n\"События мыши\"",
            Level Z10 = new Level(48, "Задание по теме\n\"События мыши\"", "Заполните пропуски чтобы создать в верстке кнопку btn, с черными границами толщиной 2, длинной 200 и высотой 60, привязать к ней событие входа мыши btn_MouseEnter и событие выхода мыши btn_MouseLeave, а в коде C# задайте обработчикам настройки, чтобы при входе мыши границы кнопки становились 10 со всех сторон, а при выходе - 2.", 
                "XAML:\n" +
                "<StackPanel>\n<~Button~ x: Name = \"~btn~\" ~MouseEnter~ = \"btn_MouseEnter\" MouseLeave = \"~btn_MouseLeave~\" ~Width~ = \"200\" Height = \"60\" BorderThickness = \"~2~\" BorderBrush = \"~Black~\" />\n</ StackPanel >\n" +
                " \n \n \nC#:\n" +
                "private void Button_MouseEnter(object sender, MouseEventArgs e)\n{\n~btn~.BorderThickness = ~new~ Thickness(~10~);\n}\n" +
                "private void Button_MouseLeave(object sender, MouseEventArgs e)\n{\n~btn.BorderThickness~ = new Thickness(~2~);\n}\n", L24);

            ModelSobitii.Add(Z10);

            //"События клавиатуры",
            StackPanel L25SP = new StackPanel();
            TextBox textBox = new TextBox() {Effect = dropShadow, Height = 40, HorizontalAlignment = HorizontalAlignment.Stretch, FontSize = 30 };
            textBox.PreviewKeyDown += SpaceProtection;
            textBox.PreviewTextInput += CheckValidData;
            L25SP.Children.Add(textBox);

            Level L25 = new Level(49, "События клавиатуры", "\tСобытия клавиатуры, наверное, представляют для нас большую ценность, чем что-либо.\n\t" +
                "Вот пример:\n\t" +
                "Нужно сделать текстовое поле, куда можно записывать только цифры и поместить его в главную стекпанель SP в верстке окна приложения\n\t" +
                "Давайте сделаем это:\n\n" +
                "TextBox textBox = new TextBox() \n{ \nHeight = 40, \nHorizontalAlignment = HorizontalAlignment.Stretch, \nFontSize = 30 \n};\n" +
                "textBox.PreviewKeyDown += SpaceProtection;\n" +
                "textBox.PreviewTextInput += CheckValidData;\n" +
                "SP.Children.Add(textBox);\n\n" +
                "private void CheckValidData(object sender, TextCompositionEventArgs e)" +
                "\n{" +
                "\ndouble value;\n" +
                "if (double.TryParse(e.Text, out value))\n" +
                "{\n" +
                "return;\n" +
                "}\n" +
                "e.Handled = true;\n" +
                "}\n\n" +
                "private void SpaceProtection(object sender, KeyEventArgs e)\n" +
                "{\n" +
                "if (e.Key == Key.Space || e.Key == Key.Enter)\n" +
                "{\n" +
                "e.Handled = true;\n" +
                "}\n" +
                "}\n\n\t" +
                "Теперь давайте разберем, что у нас происходит при попытке ввода любого символа в наше текстовое поле:\n\t" +
                "1) Когда мы нажимаем на клавишу, сразу же вызывается обработчик SpaceProtection, обрабатывающий код нажатой клавиши (т.к. тип события KeyDown), и, если клавиша является пробелом или энтером (переводом строки), то обработка прерывается (e.Handled), и наша программа забывает, что вы что-то нажимали.\n\t" +
                "Если же обработка не прервалась, то затем вызывается обработчик события CheckValidData. Теперь мы работаем со строчным представлением нажатой клавиши.\n\t" +
                "Если введеное значение можно представить в виде действительного числа (т.е. цифры в том числе) - то выходим из обработчика, и этот символ записывается в текстовое поле (ТОЛЬКО СЕЙЧАС, после всех этих проверок), если же нет - прерываем обработку.\n\t" +
                "Такая нехитрая конструкция позволяет нам избежать многих ошибок, в том числе и ошибки копирования (если в текстовое поле вставить сразу текст, то событие SpaceProtection не вызовется, т.к. оно работает только с клавишами).", L25SP, Z10);

            ModelSobitii.Add(L25);

            //"Тест по теме\n\"События клавиатуры\""
            Level T15 = new Level(50, "Тест по теме\n\"События клавиатуры\"", "Что означает e.Handled?", 
                new List<string>() { "Что обработка события прерывается, и дальше идти не будет", "Что-то непонятное", "Что программа прерывается и перестает обрабатывать все остальные входящие данные", "Что аргумент, присущий данному обработчику события переходит на ручное управление пользователя" }, L25);

            ModelSobitii.Add(T15);
            #endregion

            #region КИСТИ
            //"Свойства окраски элементов WPF",
            StackPanel L26SP = new StackPanel();

            Rectangle rect = new Rectangle() { Width = 180, Height = 140, Margin = new Thickness(0,0,0,20) };
            rect.Fill = Brushes.Blue;
            L26SP.Children.Add(rect);

            Rectangle rectangle = new Rectangle() { Width = 180, Height = 140, Margin = new Thickness(0, 0, 0, 20) };
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.BlueViolet, 0.3));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Violet, 0.5));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.7));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 1));
            rectangle.Fill = linearGradientBrush;

            L26SP.Children.Add(rectangle);

            Ellipse ellipse = new Ellipse() { Height = 140, Width = 140 };
            RadialGradientBrush radialGradientBrush = new RadialGradientBrush();
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.2));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0.3));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 0.5));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0.6));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.9));
            radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 1));
            ellipse.Fill = radialGradientBrush;
            L26SP.Children.Add(ellipse);

            Level L26 = new Level(51, "Свойства окраски элементов WPF", "\tКаждый визуальный элемент (даже контейнеры) в WPF имеет свойства окраски.\n\t" +
                "В основном, за неё отвечают поля Background (у кнопок, контейнеров, текстовых полей и др.), Fill (у элементов декораций, таких как прямоугольник, эллипс и др.), но есть и другие названия, всё зависит от элемента.\n\t" +
                "Одно у них схожее - все они имеют тип Brush, который определяет окрас , используя элементы Color - наименьших структурных единиц окраски.\n\t" +
                "Чем же отличаются Brush от Color? Всё очень просто. Brush oпределяет поведение цветов относительно друг друга (какой цвет идет за каким, какого больше и как они расположены)," +
                " в то время как Color определяет сам цвет.\n\t" +
                "Аналогом из реальной жизни можно взять ту же картину, нарисованную красками. Каждый цвет - это Color, а вся картина, на которой определено где какие цвета за чем идут - Brush.\n\t" +
                "!!При необходимости создать прозрачный цвет используйте цвет Tansparent или задавайте прозрачность элемента в 0." +
                "\n\tИменно это вызывает у новичков некоторые трудности, однако, с этим легко разобраться.\n\t" +
                "Следует отметить, что существует множество видов кистей, таких как сплошная, градиентная, визуальна и др.\n\tОсновные из них мы рассмотрим в этой главе.", L26SP, T15);

            Kisti.Add(L26);

            //"Тест по теме\n\"Свойства окраски элементов WPF\"",
            Level T16 = new Level(52, "Тест по теме\n\"Свойства окраски элементов WPF\"", "Классы Brush и Color относятся друг к другу как:", new List<string>() { "Контейнер и составлющее","Торт и ложка","Между ними ничего нет!","Это одно и то же"}, L26);

            Kisti.Add(T16);

            //"Монотонная заливка (SolidColorBrush)",
            StackPanel L27SP = new StackPanel();
            Rectangle rect1 = new Rectangle() { RadiusX = 10, RadiusY = 10, Width = 180, Height = 140 };
            rect1.Fill = Brushes.Blue;
            L27SP.Children.Add(rect1);

            Level L27 = new Level(53, "Монотонная заливка (SolidColorBrush)", "\tСтарая добрая сплошная заливка... Мммммммммм... Как в пэйнте...\n\t" +
                "Идеально подходит для случаев, когда не надо заморачиваться с интерфейсом и сделать простой контраст между элементами.\n\t" +
                "Представлено классом SolidColorBrush, значения которого программно можно задавать через перечисление Brushes (не путать с классом Brush!), которое содержит в себе все простые монотонные закраски\n\t" +
                "Определяется довольно просто:\n\n" +
                "<Rectangle RadiusX = \"10\" RadiusY = \"10\" Fill = \"Blue\"/>\n\n" +
                "Или же в коде C#:\n\n" +
                " Rectangle rectangle = new Rectangle() \n{ \nRadiusX = 10, \nRadiusY = 10, \nWidth = 180, \nHeight = 140\n};\n" +
                "rectangle.Fill = Brushes.Blue;\n\n" +
                "В данном случае мы объявили прямоугольник с скругленными краями (свойства RadiusX и RadiusY) и объявили, что заполним его сплошным цветом, в данном случае синим.\n\t" +
                "\"А где же SolidColorBrush??\" спросят немногие.\n\t" +
                "Ответ прост: наведите в коде С# на Blue в Brushes.Blue, и увидите, что она возвращает именно этот тип данных. А в верстке такие вещи преобразуются по умолчанию, вот и все.", L27SP, T16);

            Kisti.Add(L27);

            //"Задание по теме\n\"Монотонная заливка\"",
            Level Z11 = new Level(54, "Задание по теме\n\"Монотонная заливка\"", "Заполните пропуски, чтобы получилось правильное высказывание", 
                "Сплошная заливка представлена классом ~SolidColorBrush~. Используется в случаях, когда нет необходимости создавать сложный интерфейс." +
                "\nИмеет самую высокую скорость прорисовки.\nВ коде C# легче всего достать нужную заливку из перечисления ~Brushes~, обратившись к его нужжному полю, и да пусть разнообразие цветов вас не пугает: белый - он и в Африке ~белый~, а не какой-то там WhiteSmoke...\n", L27);

            Kisti.Add(Z11);

            //"Линейная градиентная закраска (LinearGradientBrush)",
            StackPanel L28SP = new StackPanel();
            Rectangle rectangle1 = new Rectangle() { Width = 180, Height = 140, Margin = new Thickness(0, 0, 0, 20) };
            LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush();
            linearGradientBrush1.StartPoint = new Point(0, 0);
            linearGradientBrush1.EndPoint = new Point(1, 1);
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.White, 0));
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.Pink, 0.25));
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.Transparent, 0.3));
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.White, 0.35));
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.Pink, 0.6));
            linearGradientBrush1.GradientStops.Add(new GradientStop(Colors.White, 1));
            rectangle1.Fill = linearGradientBrush1;

            L28SP.Children.Add(rectangle1);

            Level L28 = new Level(55, "Линейная градиентная закраска (LinearGradientBrush)", "\tУже что-от интересное!\n\t" +
                "С этой кистью нам открывается всё больше возможностей! Только представьте какие переливы можно сделать с помощью нее!\n\t" +
                "Ну а теперь о более приземленном.\n\t" +
                "Никакой магии в градиенте нет, тут всё элементарно просто: линейный градиент состоит из основных линий градиента, цвет и положение которых мы указываем, и из переходов цветов между этими линиями.\n\t" +
                "Основные поля линейной градиентной кисти - точка начала (StartPoint) и точка конца (ЕndPoint), определяющие направление градиента, " +
                "линейный список GradientStops, в котором хранятся все элементы GradientStop - цветные линии с указаной позицией.\n\t" +
                "Рассмотрим их поподробнее:\n\t" +
                "У каждого элемента GradientStop имеется два основных поля: Color, задающий цвет, и Offset, принимающий значение типа double от 0 до 1, отпределяющий положение линии градиента в процентном отнощении вектора направления.\n\t" +
                "Давайте содадим какой-нибудь извращенский градиент, чтобы продемонстрировать его действие:\n\n" +
                "<Rectangle>\n" +
                "<Rectangle.Fill>" +
                "\n<LinearGradientBrush StartPoint = \"0,0\" EndPoint = \"1,1\">\n<GradientStop Color = \"White\" Offset = \"0\"/>" +
                "\n<GradientStop Color = \"Pink\" Offset = \"0.25\"/>" +
                "\n<GradientStop Color = \"Transparent\" Offset = \"0.3\"/>\n" +
                "<GradientStop Color = \"White\" Offset = \"0.35\"/>" +
                "\n<GradientStop Color = \"Pink\" Offset = \"0.6\"/>\n" +
                "<GradientStop Color = \"White\" Offset = \"1\"/>\n" +
                "</LinearGradientBrush>" +
                "\n</Rectangle.Fill>\n" +
                "</Rectangle>", L28SP, Z11);

            Kisti.Add(L28);

            //"Задание по теме\n\"Линейная градиентная закраска (LinearGradientBrush)\"",
            Level Z12 = new Level(56, "Задание по теме\n\"Линейная градиентная закраска\"", "Заполните пропуски, чтобы сделать вертикальный, направленный сверху-вниз линейный градиент с переливами белый-синий-красный на позициях 0, 0.5 и 1 соответственно",
                  "<Rectangle>\n" +
                  "<Rectangle.~Fill~>\n" +
                  "<LinearGradientBrush StartPoint = \"~0.5~,0\" EndPoint = \"0.5,~1~\">\n" +
                  "<GradientStop ~Color~ = \"White\" Offset = \"0\"/>" +
                "\n<GradientStop Color = \"~Blue~\" Offset = \"~0.5~\"/>\n" +
                "<GradientStop Color = \"Red\" ~Offset~ = \"1\"/>\n" +
                "</~LinearGradientBrush~>\n" +
                "</Rectangle.Fill>\n" +
                "</Rectangle>\n", L28);

            Kisti.Add(Z12);

            //"Круговая градиентная заливка (RadialGradientBrush)",
            StackPanel L29SP = new StackPanel();
            Ellipse ell = new Ellipse() { Height = 140, Width = 140 };
            RadialGradientBrush radialGradientBrush1 = new RadialGradientBrush();
            radialGradientBrush1.GradientStops.Add(new GradientStop(Colors.DarkGray, 0));
            radialGradientBrush1.GradientStops.Add(new GradientStop(Colors.White, 0.2));
            radialGradientBrush1.GradientStops.Add(new GradientStop(Colors.DarkGray, 0.3));
            radialGradientBrush1.GradientStops.Add(new GradientStop(Colors.White, 0.9));
            radialGradientBrush1.GradientStops.Add(new GradientStop(Colors.DarkGray, 1));
            ell.Fill = radialGradientBrush1;

            L29SP.Children.Add(ell);

            Level L29 = new Level(57, "Круговая градиентная заливка (RadialGradientBrush)", "\tЕсли нам необходимо заполнить нечто круглое, " +
                "или же мы хотим задать эфект закругленности, то радиальный градиент - то, что создатели WPF так старались сделать для таких, как мы.\n\t" +
                "В основе принципа его закраски лежит схожий с линейныйм градиентом закраски позиционированных линий, однако там они представлены прямыми, а здесь - кругами.\n\t" +
                "По своей структуре он очень похож на линейный градиент, только вместо точек начала и конца имеется точка центра, которая так и называется - Center." +
                " Она задает откуда наш градиент начнет отсчет, таким образом, сместив центр закраски. Так же, как и в линейном градиенте, у нас имеется список " +
                "линий закраски GradientStops.\n\t" +
                "Следует учитывать, что свойство Оffset будет распределять линии вдоль радиуса, а не диаметра.\n\t" +
                "Отличий от объявления линейного градиента нет, так что давайте создадим программно такой градиент:\n\n" +
                " Ellipse ellipse = new Ellipse() { Height = 140, Width = 140 };\nRadialGradientBrush radialGradientBrush = new RadialGradientBrush();\n" +
                "radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0));\n" +
                "radialGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.2));\n" +
                "radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0.3));\n" +
                "radialGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.9));\n" +
                "radialGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 1));\n" +
                "ellipse.Fill = radialGradientBrush; ", L29SP, Z12);

            Kisti.Add(L29);

            //"Задание по теме\n\"Круговая градиентная заливка (RadialGradientBrush)\"",
            Level Z13 = new Level(58, "Задание по теме\n\"Круговая градиентная заливка", "Заполните пропуски, чтобы создать радиальный градиент, с переливами от центра: красный-синий-белый-прозрачный на позициях 0, 0.5, 0.9 и 1 соответственно",
                 "<Rectangle>\n" +
                  "<Rectangle.~Fill~>\n" +
                  "<RadialGradientBrush >\n" +
                  "<GradientStop ~Color~ = \"Red\" Offset = \"0\"/>" +
                "\n<GradientStop Color = \"~Blue~\" Offset = \"~0.5~\"/>\n" +
                "<GradientStop Color = \"White\" ~Offset~ = \"0.9\"/>\n" +
                "<GradientStop Color = \"~Transparent~\" ~Offset~ = \"1\"/>\n" +
                "</~RadialGradientBrush~>\n" +
                "</Rectangle.Fill>\n" +
                "</Rectangle>\n", L29);

            Kisti.Add(Z13);

            //"Заполнение изображением (ImageBrush)",
            StackPanel L30SP = new StackPanel();
            Rectangle R = new Rectangle() { Width = 180, Height = 140, Stroke = Brushes.Black, StrokeThickness = 2 };
            R.Fill = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/DarkBackground.jpg")) };
            L30SP.Children.Add(R);

            Level L30 = new Level(59, "Заполнение изображением (ImageBrush)", "\tВсегда стоит учитывать, что в некоторых случаях приложению легче загрузить уже готовое изображение, чем отрисовывать " +
                "каждый прописанный вами элемент со всеми его причудами и окрасками.\n\tПоэтому в качестве заднего фона многие приложения используют либо сплошную" +
                " заливку, либо подгружают имеющиеся в проекте изображения.\n\t" +
                "Если вам не нужна анимированная заставка, но не хочется оставлять все слишком однотонным - то рекомедуется использовать заполнение элемента изображением.\n\t" +
                "По сути, ImageBrush есть обычная заливка изображением, что заполняет собой всё пространство, однако стоит учитывать, что при изменении размеров наше изображение так же будет растягиваться.\n\t" +
                "Единственное, что нужно при объявлении - поле ImageSource, которое подобно полю Source у класса Image (лучше перечитайте эту статью в разделе элементы управления)." +
                "\n\tНе будем тянуть и сразу создадим такую заливку:\n\n" +
                 "<Rectangle>\n" +
                  "<Rectangle.Fill>\n" +
                  "<ImageBrush  ImageSource=\"Resources/1.jpg\"/>\n" +
                "</Rectangle.Fill>\n" +
                "</Rectangle>\n", L30SP, Z13);

            Kisti.Add(L30);

            //"Тест по теме\n\"Заполнение изображением (ImageBrush)\"",
            Level T17 = new Level(60, "Тест по теме\n\"Заполнение изображением", "Где рекомендуется хранить все используемые изображения?", 
                new List<string>() { "Включить их в проект и хранить в нем", "Желательно, чтобы они были на рабочем столе", "В защищенной папке",
                    "На флешке в кармане лучшего друга" }, L30);

            Kisti.Add(T17);

            //"Привязка заливки (VisualBrush)",
            StackPanel L31SP = new StackPanel();
            Rectangle RR = new Rectangle() { Width = 140, Height = 140 };
            RR.Fill = new VisualBrush() { Visual = BtnForward};
            L31SP.Children.Add(new TextBlock() { Text = "А это прямоугольник с VusualBrush направленным на кнопку \"Вперед\". Попробуйте нажать на неё...", TextWrapping = TextWrapping.Wrap, Background = Brushes.White, FontSize = 13,  Margin = new Thickness(10) });
            L31SP.Children.Add(RR);

            Level L31 = new Level(61, "Привязка заливки (VisualBrush)", "\tСледует отметить еще одну интересную кисть, так называемую визуальную.\n\t" +
                "Действует она подобно мимикрии." +
                "\n\tВ контейнере примеров явно показана одна из возможностей использования данной кисти. Так же этот элемент используют в браузерах, чтобы увидеть уменьшенную версию вкладки" +
                " или же в интернет магазинах, чтобы получше разглядеть изображение товара, возможностей много.\n\t" +
                "Значимое поле - Visual, указывает, с какого элемента необходимо брать привязку (под какой элемент мимикрировать).\n\t" +
                "ВНИМАНИЕ!!! Не пытайтесь создать рекурсию с помощью данного элемента, это ведет к гигантским потерям памяти!\n\t" +
                "Хотя славный дотнетовский компилятор все равно не позволит вам запустить это приложение.\n\tДавайте создадим аналог из контейнера примеров:\n\n" +
                "<StackPanel>" +
                "\n<Button x:Name = \"btn\"/>" +
                "\n<Rectangle>" +
                "\n<Rectangle.Fill>\n" +
                "<VisualBrush Visual=\"{ Binding ElementName = btn }\"/>" +
                "\n</Rectangle.Fill>\n" +
                "</Rectangle>\n" +
                "</StackPanel>", L31SP, T17);

            Kisti.Add(L31);

            //"Задание по теме\n\"Привязка заливки (VisualBrush)\""
            Level T18 = new Level(62, "Задание по теме\n\"Привязка заливки (VisualBrush)\"", "Что будет, если VisualBrush не указать с какого элемента брать изображение?",
                new List<string>() { "Элемент станет прозрачным", "Элемент станет белым", "Произойдет ошибка компиляции", "Произойдет ошибка во время исполнения программы" }, L31);

            Kisti.Add(T18);
            #endregion

            #region РЕСУРСЫ
            //"Назначение ресурсов",
            StackPanel L32SP = new StackPanel();
            TextBox rt = new TextBox() { Effect = dropShadow, Background = Brushes.White, TextWrapping = TextWrapping.Wrap, IsReadOnly=true};
            rt.Text = "<StackPanel>\n" +
                "<Button>" +
                "\n\n<Button.Background>" +
                "\n<LinearGradientBrush x:Key=\"GrayBlueGradientBrush\" StartPoint=\"0, 0\" EndPoint=\"1, 1\">\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"0\" />\n" +
                "< GradientStop Color = \"White\" Offset = \"0.5\" />\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"1\" />\n" +
                "</ LinearGradientBrush >" +
                 "\n</Button.Background>\n" +
                "\n</Button>\n\n" +
                 "<Button>" +
                "\n\n<Button.Background>" +
                "\n<LinearGradientBrush x:Key=\"GrayBlueGradientBrush\" StartPoint=\"0, 0\" EndPoint=\"1, 1\">\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"0\" />\n" +
                "< GradientStop Color = \"White\" Offset = \"0.5\" />\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"1\" />\n" +
                "</ LinearGradientBrush >" +
                 "\n</Button.Background>\n" +
                "\n</Button>\n\n" +
                 "<Button>" +
                "\n\n<Button.Background>" +
                "\n<LinearGradientBrush x:Key=\"GrayBlueGradientBrush\" StartPoint=\"0, 0\" EndPoint=\"1, 1\">\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"0\" />\n" +
                "< GradientStop Color = \"White\" Offset = \"0.5\" />\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"1\" />\n" +
                "</ LinearGradientBrush >" +
                 "\n</Button.Background>\n" +
                "\n</Button>\n" +
                "</StackPanel>";

            L32SP.Children.Add(rt);

            Level L32 = new Level(63, "Назначение ресурсов", "\tВам надоело писать один и тот же градиент двадцати практически одинаковым элементам?\n\t" +
                "Вас раздражает беспорядок в верстке??\n\t" +
                "Постоянно теряетесь в разметке???\n" +
                "\tВы гениальный художник и создали принципиально новый цвет, но не хотите, чтобы он назывался #FF121039?\n\n\t" +
                "Теперь можете облегченно выдохнуть, ведь сейчас мы изучим ресурсы!\n\t" +
                "Как мы уже знаем, в проект мы можем включать физические ресурсы, такие как изображения, аудио-, видео-  и прочие файлы. Однако существуют еще и так называемые логические ресурсы, " +
                "задача которых выносить общее за скобку.\n\tИтак, что же это такое?\n\t" +
                "Ресурсы - сегмент кода, содержащий колекцию именованных элементов, каждый элемент которой можно вызывать из любого участка кода по его названию (далее ключу).\n\t" +
                "Давайте представим такую ситуацию: Мы делаем красочный интерфейс, используя одну и ту же градиентную раскраску для трех кнопок, то есть каждый раз прописываем линейный градиент в " +
                "свойстве Background у каждой кнопки.\n\tСамо собой наш код становится крайне громоздким и неэффективным, даже в некоторой степени костыльным, ибо трудночитаем.\n\t" +
                "А теперь на нас с вами резко снизошло озарение: \"У каждого элемента есть встроенное поле Resources, в котором можно объявить общие элементы, а затем использовать их во всех " +
                "дочерних элементах!!\" о.О\n\t" +
                "И мы, глядя на то, что в данном контейнере мы используем один и тот же градиент для всех её дочерних элементов (для 3х кнопок), задумаемся и вынесем общее за скобку:\n\n" +
                "<StackPanel>\n" +
                "<StackPanel.Resources>\n" +
                "<LinearGradientBrush x:Key=\"GrayBlueGradientBrush\" StartPoint=\"0, 0\" EndPoint=\"1, 1\">\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"0\" />\n" +
                "< GradientStop Color = \"White\" Offset = \"0.5\" />\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"1\" />\n" +
                "</ LinearGradientBrush >\n" +
                "<StackPanel.Resources>\n\n" +
                "<Button Background = \"{StaticResource GrayBlueGradientBrush}\"/>\n" +
                "<Button Background = \"{StaticResource GrayBlueGradientBrush}\"/>\n" +
                "<Button Background = \"{StaticResource GrayBlueGradientBrush}\"/>\n" +
                "</StackPanel>\n\n" +
                "Мы объявили градиентную кисть в ресурсах стекпанели и дали ей имя GrayBlueGradientBrush, значит мы сделали её доступной для всех элементов внутри стекпанели.\n\t" +
                "Далее мы видим, что у каждой кнопочке мы присволили свойству Background странное значение - {StaticResource GrayBlueGradientBrush}. Таким образом мы обращаемся к ресурсу и присваиваем его значение полю Background.\n\t" +
                "Рассмотрим его поподробнее:\n\t" +
                "Так как мы не собираемся в течении выполнении программы менять значение окраски кнопки, то имеет смысл обратиться к ресурсу как к статичной составляющей (это слегка сэкономит память процессов), " +
                "для этого мы указываем способ доступа к ресурсу StaticResource и дальше указываем имя нужного нам ресурса.\n\t" +
                "ВНИМАНИЕ!!! Ресурсам кистей ВСЕГДА необходимо указывать ключ доступа x:Key.", L32SP, T18);

            Resyrsi.Add(L32);

            //"Задание по теме\n\"Назначение ресурсов\"",
            Level Z14 = new Level(64, "Задание по теме\n\"Назначение ресурсов\"", "Заполните пропуски чтобы создать ресурс радиального градиента, доступного всем элементам ОКНА и " +
                "статично присвойте его полям окраски трех элементов: кнопке, прямоугольнику и текстовому полю",
                 "Window.~Resources~>\n" +
                "<RadialGradientBrush x:~Key~=\"GrayBlueGradientBrush\">\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"0\" />\n" +
                "< GradientStop Color = \"White\" Offset = \"0.5\" />\n" +
                "< GradientStop Color = \"DarkGray\" Offset = \"1\" />\n" +
                "</ RadialGradientBrush >\n" +
                "<Window.~Resources~>\n\n"
                +"<StackPanel>\n" +
                "<Button ~Background~ = \"{~Static~Resource GrayBlueGradientBrush}\"/>\n" +
                "<~Rectangle~ Fill = \"{StaticResource GrayBlueGradientBrush}\"/>\n" +
                "<TextBlock ~Background~ = \"{StaticResource GrayBlueGradientBrush}\"/>\n" +
                "</StackPanel>\n", L32);

            Resyrsi.Add(Z14);

            //"Создание ресурсов",
            StackPanel L33SP = new StackPanel();
            Image CodLapsha = new Image() { Margin = new Thickness(10) };
            CodLapsha.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/kod.jpg"));
            L33SP.Children.Add(CodLapsha);

            Level L33 = new Level(65, "Создание ресурсов", "\tПрежде всего нужно определить на каком промежутке выполнения программы вам будет нужен ресурс: только в одном контейнере, " +
                "окне, или же во всем приложении.\n\t Это нужно опять же для того, чтобы сделать код более читаемым, а так же чтобы не засорять ресурсы компьютера ненужными задачами.\n\t" +
                "Как создавать ресурсы в элементах мы рассмотрели, однако что, если нам все же пригодится ресурс на протяжении всей программы, во всех её окнах и страницах?\n\t" +
                "Для этого нужно знать, что любая программа, построенная на платформе WPF, начинает своё выполнения с файла App.xaml.\n\tЗначит, этот модуль является контейнером для " +
                "всех остальных составляющих программы, а это значит, что поместив туда ресурс, он будет доступен из любого места программы!\n\t" +
                "Однако учитывайте, что лишних ресурсов лучше не " +
                "добавлять туда, где они не нужны, ибо при компиляции оно соберется и будет висеть в памяти без надобности.\n\t" +
                "ВНИМАНИЕ!!! Пожалуйста, не бойтесь интервалов между отдельными ресурсами (да и строчками вообще)! Давайте делать код читабельнее, а не кровоглаз! Пусть слипшиеся строчки кода остануться только в кино.\n\t" +
                "Если вы хотите задать ресурс ПРОГРАММНО какому-либо элементу, используйте метод (приведение_к_нужному_типу)TryFindResource(\"ключ_ресурса\"):\n\n" +
                "TextBlock tb = new TectBlock(); \n" +
                "tb.Background = (LinearGradientBrush)TryFindResource(\"GrayBlueGradientBrush\");", L33SP, Z14);

            Resyrsi.Add(L33);

            //"Тест по теме\n\"Создание ресурсов\"",
            Level T19 = new Level(66, "Тест по теме\n\"Создание ресурсов\"", "Как определяется доступность ресурса?", 
                new List<string>() { "Он доступент всем элементам, лежащим  внутри элемента, в котором прописан ресурс", "Он доступен из любой точки приложения",
                    "Уровнем доступа и инкапсуляцией", "Временем выполнения приложения" }, L33);

            Resyrsi.Add(T19);

            //"Виды ресурсов",
            StackPanel L34SP = new StackPanel();

            Button button1 = new Button() { Content = "1", Width = 60, Height = 30};
            button1.Click += Button_Click;
            button1.Background = (Brush)this.TryFindResource("Simple");

            Button button2 = new Button() { Content = "2", Width = 60, Height = 30, Margin = new Thickness(30)};
            button2.Click += Button_Click;
            button2.SetResourceReference(Button.BackgroundProperty, "Simple");

            L34SP.Children.Add(button1);
            L34SP.Children.Add(button2);

            Level L34 = new Level(67, "Виды ресурсов", "\tЧуть ранее мы встречали упоминание о статическом ресурсе, что это своего рода уровень доступа элемента к какому-либо ресурсу.\n\t" +
                "Давайте поподробнее рассмотрим этот аспект:\n\t" +
                "Существует два вида доступа к ресурсам - статический и динамический. \n\tК одному и тому же ресурсу можно одновременно обращаться и статически и динамически из разных элементов.\n\t" +
                "Соответственно применяется один из этих видов доступа в зависимости от необходимого функционала.\n\t" +
                "Создадим для наглядного примера ресурс кисти и две кнопки. Одной кнопке присвоим значение ресурса статически, а другой - динамически:\n\n" +
                "<StackPanel>\n" +
                "<StackPanel.Resources>\n" +
                "<SolidColorBrush x:Key=\"Simple\" Color=\"LightBlue\"/>\n" +
                "<StackPanel.Resources>\n\n" +
                "<Button Background = \"{StaticResource Simple}\"/>\n" +
                "<Button Background = \"{DynamicResource Simple}\"/>\n" +
                "</StackPanel>\n\n" +
                "Для того, чтобы понять, в чем главное отличие уровней доступа к ресурсам, зададим каждой кнопке следующее событие Click:\n\n" +
                "private void Button_Click(object sender, RoutedEventArgs e)\n{\nthis.Resources[\"Simple\"] = new SolidColorBrush(Colors.LimeGreen);\n}\n\n" +
                "Теперь по щелчку любой кнопки наш ресурс Simple изменит своё значение с синего цвета на зеленый.\n\t" +
                "Попробуем нажать на кнопку...\n\t" +
                "Заметим, что кнопка с динамически заданным ресурсом изменила значение своего цвета вместе с изменением цвета у ресурса, а кнопка со статически заданным свойством осталась без изменений.\n\t" +
                "Это и есть основное отличие видов доступа к ресурсам.\n\t" +
                "Применяются динамические ресурсы в основном для анимаций, визуальных эфектов и прочих декоративных целях, однако стоит учитывать, " +
                "что динамически заданные ресурсы расходуют чуть больше пямяти, нежели статически заданные.", L34SP, T19);

            Resyrsi.Add(L34);

            //"Тест по теме\n\"Виды ресурсов\"",
            Level T20 = new Level(68, "Тест по теме\n\"Виды ресурсов\"", "Стоит задача: использовать ресурс на элемент. Как вы уточните задачу и какие выводы по реализации сделаете?",
                new List<string>() { "Элемент должен будет изменять своё значение в соответствии со значением ресурса?\nДа - будем использовать динамический доступ, нет - статический",
                    "Элемент должен будет изменять своё значение в соответствии со значением ресурса?\nДа - будем использовать статический доступ, нет - динамический", 
                    "Мне хватит данных, буду смотреть по ходу реализации", "Ух, костыли мои костыли..." }, L34);

            Resyrsi.Add(T20);
            #endregion

            #region ПРИВЯЗКА
            //"Назначение привязки",
            //"Тест по теме\n\"Назначение привязки\"",

            //"Программное создание привязки",
            //"Тест по теме\n\"Программное создание привязки\"",

            //"Основные свойства привязки",
            //"Тест по теме\n\"Основные свойства привязки\""
            #endregion
        }

        #region Методы для примеров
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Simple"] = new SolidColorBrush(Colors.LimeGreen);
        }

        private void CheckValidData(object sender, TextCompositionEventArgs e)
        {
            double value;
            if (double.TryParse(e.Text, out value))
            {
                return;
            }
            e.Handled = true;
        }
        private void SpaceProtection(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void L24B_MouseLeave(object sender, MouseEventArgs e)
        {
            TBTB.Text = "Мышка НЕ над кнопкой";
        }

        private void L24B_MouseEnter(object sender, MouseEventArgs e)
        {
            TBTB.Text = "Мышка над кнопкой";
        }

        private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Ты выбрал " + (cb.SelectedItem as TextBlock).Text);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Name);
        }
        #endregion

        void OpenMenu()
        {
            Menu.Visibility = Visibility.Visible;
        }

        void CloseMenu()
        {
            Menu.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Событие нажатия на кнопку "Меню"
        /// </summary>
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Menu.Visibility == Visibility.Hidden)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Продолжить" в меню
        /// </summary>
        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        /// <summary>
        /// Событие закрытия меню при долгом отсутствии мыши
        /// </summary>
        private void DoubleAnimation_CloseMenu_Completed(object sender, EventArgs e)
        {
            CloseMenu();
        }


        /// <summary>
        /// Событие нажатия на кнопку "Разделы"
        /// </summary>
        private void BtnChapters_Click(object sender, RoutedEventArgs e)
        {
            ClosingDoorsBeforeFoundPage("Chapters.xaml");
        }

        /// <summary>
        /// Событие нажатия на кнопку "Главное меню". Выходит в главное меню
        /// </summary>
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            ClosingDoorsBeforeFoundPage("MainMenu.xaml");
        }


        /// <summary>
        /// Событие нажатия кнопки "Выход"
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            ClosingConfirmWindow confirmWindow = new ClosingConfirmWindow();
            bool DoClose = confirmWindow.ShowDialog(1);

            if (DoClose)
            {
                Application.Current.Shutdown();
            }
        }



        /// <summary>
        /// Событие нажатия кнопки навигации "Назад"
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                ClosingDoorsBeforeNavigation(false);
        }

        public StackPanel SaveStackPanel;

        /// <summary>
        /// Событие нажатия кнопки навигации "Вперед"
        /// </summary>
        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward)
                ClosingDoorsBeforeNavigation(true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClosingConfirmWindow confirmWindow = new ClosingConfirmWindow();
            bool DoClose = confirmWindow.ShowDialog(1);

            if (!DoClose)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Открывание дверей анимация
        /// </summary>
        void OpenDoors()
        {
            DoubleAnimationUsingKeyFrames OpenLeftDoor = new DoubleAnimationUsingKeyFrames();
            OpenLeftDoor.Duration = TimeSpan.FromSeconds(0.9);
            OpenLeftDoor.DecelerationRatio = 0.6;
            OpenLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(LeftPartOfDoor.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            OpenLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(45, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));

            DoubleAnimationUsingKeyFrames OpenRightDoor = new DoubleAnimationUsingKeyFrames();
            OpenRightDoor.Duration = TimeSpan.FromSeconds(0.9);
            OpenRightDoor.DecelerationRatio = 0.6;
            OpenRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(RightPartOfDoor.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            OpenRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(45, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));

            DoubleAnimationUsingKeyFrames LogoAnimation = new DoubleAnimationUsingKeyFrames();
            LogoAnimation.Duration = TimeSpan.FromSeconds(0.9);
            LogoAnimation.DecelerationRatio = 0.6;
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth - 335, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));

            LeftPartOfDoor.BeginAnimation(WidthProperty, OpenLeftDoor);
            RightPartOfDoor.BeginAnimation(WidthProperty, OpenRightDoor);
            Logotype.RenderTransform.BeginAnimation(TranslateTransform.XProperty, LogoAnimation);

            ThisWindow.ResizeMode = ResizeMode.CanResize;
            Canvas.SetZIndex(Block, -2);
        }

        /// <summary>
        /// Анимация закрывания дверей
        /// </summary>
        /// <param name="CloseLeftDoor">Анимация двери, к которой привязан обработчик завершения анимации</param>
        void CloseDoors(DoubleAnimationUsingKeyFrames CloseLeftDoor)
        {
            CloseLeftDoor.Duration = TimeSpan.FromSeconds(1.3);
            CloseLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(190, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            CloseLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25))));
            CloseLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            CloseLeftDoor.KeyFrames.Add(new LinearDoubleKeyFrame(280, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            CloseLeftDoor.AccelerationRatio = 0.7;

            DoubleAnimationUsingKeyFrames CloseRightDoor = new DoubleAnimationUsingKeyFrames();
            CloseRightDoor.Duration = TimeSpan.FromSeconds(1.3);
            CloseRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(400, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            CloseRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(340, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25))));
            CloseRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(340, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            CloseRightDoor.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth - 335, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            CloseRightDoor.AccelerationRatio = 0.7;

            DoubleAnimationUsingKeyFrames LogoAnimation = new DoubleAnimationUsingKeyFrames();
            LogoAnimation.Duration = TimeSpan.FromSeconds(1.3);
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(540, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(600, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25))));
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(600, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            LogoAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            LogoAnimation.AccelerationRatio = 0.7;

            Logotype.RenderTransform.BeginAnimation(TranslateTransform.XProperty, LogoAnimation);
            LeftPartOfDoor.BeginAnimation(WidthProperty, CloseLeftDoor);
            RightPartOfDoor.BeginAnimation(WidthProperty, CloseRightDoor);
        }


        //ОТКРЫТИЕ МОДУЛЕЙ
        Level CurrentLvl;
        /// <summary>
        /// Закрывание дверей анимация при загрузке лекционных и тестировочных модулей
        /// </summary>
        /// <param name="lvl">
        /// Загружаемый уровень
        /// </param>
        public void ClosingDoorsBeforeModel(Level lvl)
        {
            ThisWindow.ResizeMode = ResizeMode.NoResize;
            Canvas.SetZIndex(Block, 10);

            CurrentLvl = lvl;
            DoubleAnimationUsingKeyFrames CloseLeftDoor = new DoubleAnimationUsingKeyFrames();
            CloseLeftDoor.Completed += CloseDoorsBeforeModel_Completed;

            CloseDoors(CloseLeftDoor);
        }

        private void CloseDoorsBeforeModel_Completed(object sender, EventArgs e)
        {
            if (MainFrame.Content is Theory)
            {
                (MainFrame.Content as Theory).Example.Children.Clear();
            }

            CurrentLvl.Load();
            double w = this.ActualWidth - 335;
            RightPartOfDoor.Width = w;

            OpenDoors();
        }


        //ОТКРЫТИЕ СТАРТОВЫХ СТРАНИЦ
        Uri CurrentChaptersUri;
        /// <summary>
        /// Закрывание дверей анимация при загрузке стартовых страниц
        /// </summary>
        public void ClosingDoorsBeforeFoundPage(string URI)
        {
            CurrentChaptersUri = new Uri(URI, UriKind.Relative);

            if (MainFrame.CurrentSource == CurrentChaptersUri)
            {
                return;
            }

            ThisWindow.ResizeMode = ResizeMode.NoResize;
            Canvas.SetZIndex(Block, 10);

            CloseMenu();

            DoubleAnimationUsingKeyFrames CloseLeftDoor = new DoubleAnimationUsingKeyFrames();
            CloseLeftDoor.Completed += CloseDoorsBeforeFoundPage_Completed;

            CloseDoors(CloseLeftDoor);
        }

        private void CloseDoorsBeforeFoundPage_Completed(object sender, EventArgs e)
        {
            if (MainFrame.Content is Theory)
            {
                (MainFrame.Content as Theory).Example.Children.Clear();
            }

            MainFrame.NavigationService.Navigate(CurrentChaptersUri);

            double w = this.ActualWidth - 335;
            RightPartOfDoor.Width = w;

            OpenDoors();
        }


        //ОТКРЫТИЕ ПРИ НАВИГАЦИИ
        bool NavigateToNext;
        /// <summary>
        /// Закрывание дверей анимация при навигации
        /// </summary>
        public void ClosingDoorsBeforeNavigation(bool IsNext)
        {
            ThisWindow.ResizeMode = ResizeMode.NoResize;
            Canvas.SetZIndex(Block, 10);

            NavigateToNext = IsNext;

            DoubleAnimationUsingKeyFrames CloseLeftDoor = new DoubleAnimationUsingKeyFrames();
            CloseLeftDoor.Completed += CloseDoorsBeforeNavigation_Completed;

            CloseDoors(CloseLeftDoor);
        }

        private void CloseDoorsBeforeNavigation_Completed(object sender, EventArgs e)
        {
            if(NavigateToNext)
            {
                MainFrame.GoForward();
            }
            else
            {
                if(MainFrame.Content is Theory)
                {
                    SaveStackPanel = (MainFrame.Content as Theory).Example.Children[0] as StackPanel;
                    (MainFrame.Content as Theory).Example.Children.Clear();
                }
                
                MainFrame.GoBack();
            }

            double w = this.ActualWidth - 335;
            RightPartOfDoor.Width = w;

            OpenDoors();
        }

        /// <summary>
        /// Загрузочный экран при входе в приложение
        /// </summary>
        bool IsStarting = true;
        private void ThisWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsStarting)
            {
                IsStarting = false;
                HelloyTxt.Visibility = Visibility.Collapsed;
                this.ResizeMode = ResizeMode.CanResize;
                BtnMenu.IsEnabled = true;
                BtnBack.IsEnabled = true;
                BtnForward.IsEnabled = true;
                OpenDoors();
            }

            ThisWindow.PreviewKeyDown -= ThisWindow_PreviewKeyDown;
        }
    }
}
