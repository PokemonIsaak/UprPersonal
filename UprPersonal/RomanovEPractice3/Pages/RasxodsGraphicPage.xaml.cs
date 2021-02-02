using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace RomanovEPractice3.Pages
{
    /// <summary>
    /// Логика взаимодействия для RasxodsGraphicPage.xaml
    /// </summary>
    public partial class RasxodsGraphicPage : Page
    {
        private RomanovEPractice3.Entities.RomanovAEntities _context = new RomanovEPractice3.Entities.RomanovAEntities();

        public RasxodsGraphicPage()
        {
            InitializeComponent();
            ChartPlayments.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true        
            };
            ChartPlayments.Series.Add(currentSeries);
            //var currentProjects = AppData.Context.Workers.ToList();
            //currentProjects.Insert(0, new Entities.Workers
            //{
                //Id_Worker = 0,
                //FIO = "Все работники"
            //});
            ComboUsers.ItemsSource = _context.Workers.ToList();
            ComboChartTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));

        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUsers.SelectedItem is Entities.Workers currentUser && 
                ComboChartTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartPlayments.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categoriesList = _context.ShopIdName.ToList();
                foreach (var category in categoriesList)
                {
                    currentSeries.Points.AddXY(category.NameShop,
                        _context.WorkersExpenses.ToList().Where(p => p.Workers == currentUser
                        && p.ShopIdName == category).Sum(p=>p.Summ));

                }
            }
        }

        
    }
}
