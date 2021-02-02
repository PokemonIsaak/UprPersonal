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
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Page
    {
        List<WorkersPosition> ListPosition = new List<WorkersPosition>();
        bool isUpdate = false;

        private Entities.Workers _currentWorker;

        public AddWorkerWindow(Entities.Workers selectedWorker)
        {
            InitializeComponent();
            TbxId.Text = (AppData.Context.Workers.ToList().Max(p => p.Id_Worker) + 1).ToString();
            CmbPosition.ItemsSource = AppData.Context.WorkersPosition.ToList();
            CmbPosition.SelectedIndex = 0;

            if (selectedWorker != null)
            {
                _currentWorker = selectedWorker;

                TbxF.Text = _currentWorker.FIO;
                DtpD.SelectedDate = _currentWorker.DateOfBirth;

                TbxId.Text = _currentWorker.Id_Worker.ToString();

                TbxS.Text = _currentWorker.Salary.ToString();
                CmbPosition.SelectedItem = _currentWorker.WorkersPosition;
            }
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            if (TbxId.Text != "" && TbxF.Text != "" && TbxS.Text != "" && DtpD.SelectedDate != null)
            {
                if (_currentWorker != null)
                {
                    _currentWorker.FIO = TbxF.Text;
                    _currentWorker.Id_Worker = Convert.ToInt32(TbxId.Text);
                    _currentWorker.PositionId = CmbPosition.SelectedIndex + 1;
                    _currentWorker.Salary = Convert.ToDecimal(TbxS.Text);
                    _currentWorker.DateOfBirth = DtpD.SelectedDate.Value;
                    _currentWorker.TimeOfBirth = TimeSpan.Zero;
                    _currentWorker.IsDeleted = false;
                }
                else
                {
                    _currentWorker = new Entities.Workers
                    {
                        FIO = TbxF.Text,
                        Id_Worker = AppData.Context.Workers.ToList().Max(p => p.Id_Worker) + 1,
                        PositionId = CmbPosition.SelectedIndex + 1,
                        Salary = Convert.ToDecimal(TbxS.Text),
                        DateOfBirth = DtpD.SelectedDate.Value,
                        TimeOfBirth = TimeSpan.Zero,
                        IsDeleted = false
                    };
                    AppData.Context.Workers.Add(_currentWorker);
                }                
                
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

        private void CmbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
