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
    /// Логика взаимодействия для UserMenuPage.xaml
    /// </summary>
    public partial class UserMenuPage : Page
    {
        public UserMenuPage()
        {
            InitializeComponent();
        }

        private void BtnProjectPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectsPage());
        }
    }
}
