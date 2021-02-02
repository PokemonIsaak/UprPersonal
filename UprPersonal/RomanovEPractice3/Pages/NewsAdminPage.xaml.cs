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

namespace RomanovEPractice3.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewsAdminPage.xaml
    /// </summary>
    public partial class NewsAdminPage : Page
    {
        public NewsAdminPage()
        {
            InitializeComponent();
            var currentAutors = AppData.Context.Author.ToList();
            currentAutors.Insert(0, new Entities.Author
            {
                AuthorId = 0,
                NickName = "Все авторы"
            });
            ComboAutors.ItemsSource = currentAutors;
            UpdateItems();
        }
        private void UpdateItems()
        {
            var allNews = AppData.Context.NewsItem.ToList();
            if (ComboAutors.SelectedIndex > 0)
            {
                allNews = allNews.Where
                    (p => p.Author == ComboAutors.SelectedItem as Entities.Author).ToList();
            }

            switch (ComboSort.SelectedIndex)
            {
                case 0:
                    allNews = allNews.OrderBy(p => p.CreationDate).ToList();
                    break;
                case 1:
                    allNews = allNews.OrderBy(p => p.Header).ToList();
                    break;
                case 2:
                    allNews = allNews.OrderBy(p => p.Text).ToList();
                    break;
                default:
                    allNews = allNews.OrderBy(p => p.Author.NickName).ToList();
                    break;
            }
            if (LViewNews != null)
            {
                LViewNews.ItemsSource = allNews;
            }
            //var currentAuthor = ComboAutors.SelectedItem as Entities.Author;
            //if (currentAuthor != null)
            //{
            //LViewNews.ItemsSource = AppData.Context.NewsItem.ToList()
            //.Where(p=>p.Author == currentAuthor).ToList();
            //}
            //else
            //{
            //LViewNews.ItemsSource = AppData.Context.NewsItem.ToList();
            //}            
        }

        private void ComboAutors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void BtnAdddNews_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewsAddPage());
            UpdateItems();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (RbCSV.IsChecked == true)
            {
                ExportToCSV();
            }
            if (RbXLSX.IsChecked == true)
            {
                ExportToXLSX();
            }            
        }

        private void ExportToCSV()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Id, Header, Text, CreationData, Author");

            var allNews = AppData.Context.NewsItem.ToList().
                OrderBy(p => p.CreationDate).ToList();

            foreach(var newsItem in allNews)
            {
                stringBuilder.AppendLine($"{newsItem.Id}, {CheckTextForCSV(newsItem.Header)}, " +
                    $"{ CheckTextForCSV(newsItem.Text)}, { newsItem.CreationDate.ToString("dd:MM:yyyy HH:mm")}, " +
                    $"{CheckTextForCSV(newsItem.Author.NickName)}");
            }
            checkExportDirectory();
            File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}Export" +
                $"\\Export_News_CSV_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.csv",
                stringBuilder.ToString());
            MessageBox.Show("Данные выгружены");
        }
        private void ExportToXLSX()
        {
            Microsoft.Office.Interop.Excel.Application application = 
                new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet worksheet =
                application.ActiveSheet;

            worksheet.Cells[1, 1] = "ID";
            worksheet.Cells[1, 2] = "Header";
            worksheet.Cells[1, 3] = "Text";
            worksheet.Cells[1, 4] = "CreationData";
            worksheet.Cells[1, 5] = "Author";

            var cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 5]];

            var allNews = AppData.Context.NewsItem.ToList().
                OrderBy(p => p.CreationDate).ToList();

            for (int i = 0; i < allNews.Count(); i++)
            {
                var currentNews = allNews[i];

                worksheet.Cells[i + 2, 1] = currentNews.Id;
                worksheet.Cells[i + 2, 2] = currentNews.Header;
                worksheet.Cells[i + 2, 3] = currentNews.Text;
                worksheet.Cells[i + 2, 4] = currentNews.CreationDate.ToString("dd:MM:yyyy HH:mm");
                worksheet.Cells[i + 2, 5] = currentNews.Author.NickName;
            }
            checkExportDirectory();

            worksheet.SaveAs($"{AppDomain.CurrentDomain.BaseDirectory}Export" + 
                $"\\Export_News_XLSX_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
            application.Quit();
            MessageBox.Show("Данные выгружены");
        }
        private void checkExportDirectory()
        {
            var directory = new DirectoryInfo("Export");
            if (directory.Exists == false)
            {
                directory.Create();
            }
        }
        private string CheckTextForCSV(string text)
        {
            if (text.Contains(','))
            {
                return $"\"{text}\"";
            }
            else return text;
        }
    }
}
