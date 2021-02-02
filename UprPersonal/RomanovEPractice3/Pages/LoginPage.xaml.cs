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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void BtnNavigate_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxLogin.Text != "" || TBoxPassword.Password != "")
            {
                var hashedPassword = new Hashing.Sha1Hassing().HasString(TBoxPassword.Password);

                var currentUser = AppData.Context.AvtorisationP
                    .ToList().FirstOrDefault(p =>
                    p.Login == TBoxLogin.Text &&
                    p.Password == hashedPassword);
                if (currentUser != null)
                {
                    NavigateUser(currentUser);
                }
                else
                {
                    MessageBox.Show("Пользователь не найден",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Все поля обязательны для заполнения", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateUser(Entities.AvtorisationP currentUser)
        {
            switch (currentUser.RoleId)
            {
                case 1:
                    SaveUserSettings(currentUser.ExpertId, CheckRemember.IsChecked.Value);
                    NavigationService.Navigate(new AdminMenuPage());
                    break;
                case 2:
                    SaveUserSettings(currentUser.ExpertId, CheckRemember.IsChecked.Value);
                    NavigationService.Navigate(new UserMenuPage());
                    break;
                default:
                    MessageBox.Show("Для данной роли функционал не предусмотрен",
            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void SaveUserSettings(int id, bool isChecked)
        {
            if (isChecked)
            {
                Properties.Settings.Default.UserId = id;
                Properties.Settings.Default.Save();
            }            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var userId = Properties.Settings.Default.UserId;
            if (userId > 0)
            {
                var currentUser = AppData.Context.AvtorisationP.ToList().FirstOrDefault(p => p.ExpertId == userId);
                if (currentUser != null)
                {
                    NavigateUser(currentUser);
                }
            }
        }

        private void BtnNews_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewsPage());
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartRegistrPage());
        }

        private void CheckRemember_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
