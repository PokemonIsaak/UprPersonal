using Microsoft.Win32;
using RomanovEPractice3.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace RomanovEPractice3.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersDataGridPage.xaml
    /// </summary>
    public partial class UsersDataGridPage : Page
    {       

        public UsersDataGridPage()
        {
            InitializeComponent();
            dataGridView1.ItemsSource = AppData.Context.Workers.ToList();

            var positionsList = AppData.Context.WorkersPosition.ToList();
            positionsList.Insert(0, new Entities.WorkersPosition
            {
                PositionId = 0,
                PositionName = "Все должности"
            });

            doljnosty.ItemsSource = positionsList;
            UpdateUsers();
        }

        private void phonesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void TextBoxSearching_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void doljnosty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void UpdateUsers()
        {
            var usersList = AppData.Context.Workers.ToList();
            if (doljnosty.SelectedIndex > 0)
            {
                usersList = usersList.Where(p => p.WorkersPosition == doljnosty.SelectedItem as Entities.WorkersPosition && p.IsDeleted == false).ToList();

                if (TextBoxSearching.Text != "")
                {
                    string keyWord = TextBoxSearching.Text.ToLower();
                    usersList = usersList
                        .Where(p => p.FIO.ToLower().Contains(keyWord) ||
                        p.DateOfBirth.ToString().Contains(keyWord) ||
                        p.Salary.ToString().Contains(keyWord) ||
                        p.WorkersPosition.PositionName.ToLower().Contains(keyWord))
                        .Where(p => p.WorkersPosition == doljnosty.SelectedItem as Entities.WorkersPosition && p.IsDeleted == false)
                        .ToList();
                }                
            }
            else if (doljnosty.SelectedIndex == 0)
            {
                if (TextBoxSearching.Text != "")
                {
                    string keyWord = TextBoxSearching.Text.ToLower();
                    usersList = usersList
                        .Where(p => p.FIO.ToLower().Contains(keyWord) ||
                        p.DateOfBirth.ToString().Contains(keyWord) ||
                        p.Salary.ToString().Contains(keyWord) ||
                        p.WorkersPosition.PositionName.ToLower().Contains(keyWord))
                        .Where(p => p.IsDeleted == false)
                        .ToList();
                }    
            }

            dataGridView1.DataContext = usersList;

            if (dataGridView1 != null)
            {
                dataGridView1.ItemsSource = usersList;
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {

            AddWorkerWindow AddWor = new AddWorkerWindow(null);
            NavigationService.Navigate(new AddWorkerWindow(null));
            UpdateUsers();            
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem is Workers currentUsers)
            {
                AddWorkerWindow AddS = new AddWorkerWindow(currentUsers);
                NavigationService.Navigate(new AddWorkerWindow(currentUsers));
                UpdateUsers();
            }       
            else
            {
                MessageBox.Show("Выберите работника");
            }
        }

        private void BtnDelUser_Click(object sender, RoutedEventArgs e)
        {            
            if (dataGridView1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Yes?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (dataGridView1.SelectedItem is Workers currentUsers)
                    {
                        AppData.Context.Workers.Remove((Workers)dataGridView1.SelectedItem);
                        AppData.Context.SaveChanges();
                        UpdateUsers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void BtnFallshDelUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Yes?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (dataGridView1.SelectedItem is Workers currentUsers)
                    {
                        currentUsers.IsDeleted = true;
                        AppData.Context.SaveChanges();
                        UpdateUsers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrintServicesPage());
        }

        private void BtnToExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application application =
                new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet worksheet =
                application.ActiveSheet;

            worksheet.Cells[1, 1] = "Id_Worker";
            worksheet.Cells[1, 2] = "FIO";
            worksheet.Cells[1, 3] = "DateOfBirth";
            worksheet.Cells[1, 4] = "Salary";
            worksheet.Cells[1, 5] = "Position";

            var cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 5]];

            var allWorkers = AppData.Context.Workers.ToList();

            for (int i = 0; i < allWorkers.Count(); i++)
            {
                var currentWorkers = allWorkers[i];

                worksheet.Cells[i + 2, 1] = currentWorkers.Id_Worker;
                worksheet.Cells[i + 2, 2] = currentWorkers.FIO;
                worksheet.Cells[i + 2, 3] = currentWorkers.DateOfBirth.ToString("dd:MM:yyyy HH:mm");
                worksheet.Cells[i + 2, 4] = currentWorkers.Salary;
                worksheet.Cells[i + 2, 5] = currentWorkers.WorkersPosition.PositionName;
            }
            checkExportDirectory();

            worksheet.SaveAs($"{AppDomain.CurrentDomain.BaseDirectory}Export" +
                $"\\Export_Workers_XLSX_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
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
    }
}
