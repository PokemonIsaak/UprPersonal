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

namespace RomanovEPractice3.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMenuPage.xaml
    /// </summary>
    public partial class AdminMenuPage : Page
    {
        public AdminMenuPage()
        {
            InitializeComponent();
        }

        private void BtnDataGridForm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersDataGridPage());
        }

        private void BtnCharts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChartsPage());
        }

        private void BtnGraphic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RasxodsGraphicPage());
        }

        private void BtnProgul_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProgulPage());
        }

        private void BtnNewsAdminPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewsAdminPage());
        }
    }
}
