using RomanovEPractice3.Entities;
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
    /// Логика взаимодействия для StartRegistrPage.xaml
    /// </summary>
    public partial class StartRegistrPage : Page
    {
        public StartRegistrPage()
        {
            InitializeComponent();
            TxtKodUser.Text = Convert.ToString(AppData.Context.AvtorisationP.ToList().Max(p => p.ExpertId) + 1);            
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void BtnYesR_Click(object sender, RoutedEventArgs e)
        {
            if (TbxFamaly.Text != "" && TbxName.Text != "" && TbxPhone.Text != "" && TbxLogin.Text != "" && TbxPassword.Text != "")
            {
                //AppData.Context.WorkersPosition.Add(position);
                //AppData.Context.SaveChanges();
                var hashedPassword = new Hashing.Sha1Hassing().HasString(TbxPassword.Text);
                var expert = new AvtorisationP
                {
                    ExpertId = AppData.Context.AvtorisationP.ToList().Max(p => p.ExpertId) + 1,
                    RoleId = 2,
                    FirstName = Convert.ToString(TbxFamaly.Text),
                    LastName = Convert.ToString(TbxName.Text),
                    Phone = Convert.ToString(TbxPhone.Text),
                    DateOfBirth = DateTime.Now,
                    Login = Convert.ToString(TbxLogin.Text),
                    Password = hashedPassword
                };
                AppData.Context.AvtorisationP.Add(expert);
                AppData.Context.SaveChanges();
                MessageBox.Show("Добавление успешно выполнено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
