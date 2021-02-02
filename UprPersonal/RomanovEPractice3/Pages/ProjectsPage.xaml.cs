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
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            InitializeComponent();
            var currentProjects = AppData.Context.Zapros.ToList();
            currentProjects.Insert(0, new Entities.Zapros
            {
                ZaprosId = 0,
                NameZap = "Все заказчики"
            });
            ComboAutors.ItemsSource = currentProjects;
            UpdateItems();
        }
        private void UpdateItems()
        {
            var allProjects = AppData.Context.ProjectItem.ToList();
            if (ComboAutors.SelectedIndex > 0)
            {
                allProjects = allProjects.Where
                    (p => p.Zapros == ComboAutors.SelectedItem as Entities.Zapros).ToList();
            }

            switch (ComboSort.SelectedIndex)
            {
                case 0:
                    allProjects = allProjects.OrderBy(p => p.CreationDate).ToList();
                    break;
                case 1:
                    allProjects = allProjects.OrderBy(p => p.Header).ToList();
                    break;
                case 2:
                    allProjects = allProjects.OrderBy(p => p.Text).ToList();
                    break;
                default:
                    allProjects = allProjects.OrderBy(p => p.Zapros.NameZap).ToList();
                    break;
            }
            if (LViewNews != null)
            {
                LViewNews.ItemsSource = allProjects;
            }      
        }
        private void ComboAutors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }
    }
}
