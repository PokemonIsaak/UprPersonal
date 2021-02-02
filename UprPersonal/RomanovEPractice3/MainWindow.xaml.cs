using RomanovEPractice3.Entities;
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

namespace RomanovEPractice3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var currentNews = AppData.Context.NewsItem.ToList()[12];
            //currentNews.Photo = File.ReadAllBytes(@"C:\Users\user\source\repos\RomanovEPractice3\Photo\abba14.jpg");
            //AppData.Context.SaveChanges();

            //var password = "admin123";
            //var hash = new Hashing.Sha1Hassing().HasString(password);

            //var allUsers = AppData.Context.AvtorisationP.ToList();
            //var hashing = new Hashing.Sha1Hassing();
            //foreach (var user in allUsers)
            //{
                //user.Password = hashing.HasString(user.Password);
            //}
            //AppData.Context.SaveChanges();

            MainFrame.Navigate(new Pages.LoginPage());                              
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var page = MainFrame.Content as Page;
            if (page is Pages.AdminMenuPage || page is Pages.UserMenuPage)
            {
                Properties.Settings.Default.UserId = 0;
                Properties.Settings.Default.Save();
            }
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            var page = (sender as Frame).Content as Page;

            if (page is Pages.LoginPage)
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility = Visibility.Visible;
            }
        }
    }
}
