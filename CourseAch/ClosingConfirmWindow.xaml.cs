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
using System.Windows.Shapes;

namespace CourseAch
{
    /// <summary>
    /// Логика взаимодействия для ClosingConfirmWindow.xaml
    /// </summary>
    public partial class ClosingConfirmWindow : Window
    {
        public ClosingConfirmWindow()
        {
            InitializeComponent();

            this.Owner = (MainWindow)Application.Current.MainWindow;
        }

        bool Result;

        public bool ShowDialog(int i)
        {
            ShowDialog();
            return Result;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }
    }
}
