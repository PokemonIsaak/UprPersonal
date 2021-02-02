using Microsoft.Win32;
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
    /// Логика взаимодействия для NewsAddPage.xaml
    /// </summary>
    public partial class NewsAddPage : Page
    {
        private Entities.NewsItem _currentNewsItem;
        private byte[] _previewData;
        public NewsAddPage()
        {
            InitializeComponent();
            TbxId.Text = (AppData.Context.NewsItem.ToList().Max(p => p.Id) + 1).ToString();
        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images | *.jpg;*.png";

            if (ofd.ShowDialog() == true)
            {
                _previewData = File.ReadAllBytes(ofd.FileName);
                ImagePreview.DataContext = _previewData;
            }
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            if (TbxId.Text != "" && TbxHeader.Text != "" && TbxText.Text != "" && _previewData != null)
            {
                _currentNewsItem = new Entities.NewsItem
                {
                    Id = AppData.Context.NewsItem.ToList().Max(p => p.Id) + 1,
                    Header = TbxHeader.Text,
                    Text = TbxText.Text,
                    CreationDate = DateTime.Now,
                    AuthorId = 2
                };

                if (_previewData != null)
                {
                    _currentNewsItem.Photo = _previewData;
                }

                AppData.Context.NewsItem.Add(_currentNewsItem);
                AppData.Context.SaveChanges();
                MessageBox.Show("Добавление успешно выполнено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
