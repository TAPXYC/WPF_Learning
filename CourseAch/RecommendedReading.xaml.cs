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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace CourseAch
{
    /// <summary>
    /// Логика взаимодействия для RecommendedReading.xaml
    /// </summary>
    public partial class RecommendedReading : Page
    {
        public RecommendedReading()
        {
            InitializeComponent();
        }

        string RecommendedSources = "\t֍ https://professorweb.ru/my/WPF/base_WPF/level1/info_WPF.php \n\n\t֍ https://docs.microsoft.com/ru-ru/dotnet/ " +
            "\n\n\t֍ https://metanit.com/sharp/wpf/ \n\n\t֍ https://coderlessons.com/tutorials/microsoft-technologies/vyuchit-wpf/wpf-kratkoe-rukovodstvo";

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Literature.Text = RecommendedSources;
        }
    }
}
