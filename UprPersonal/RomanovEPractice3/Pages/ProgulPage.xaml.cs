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
    /// Логика взаимодействия для ProgulPage.xaml
    /// </summary>
    public partial class ProgulPage : Page
    {
        public ProgulPage()
        {
            InitializeComponent();
        }

        private void BtnProverka_Click(object sender, RoutedEventArgs e)
        {
            var progulList = AppData.Context.WorkerBe.ToList();

            if (TbxId.Text != "" && DtpD.SelectedDate != null)
            {
                int KodWor = Convert.ToInt32(TbxId.Text);
                DateTime datePr = (DateTime)DtpD.SelectedDate;

                var workerBe = AppData.Context.WorkerBe.ToList().FirstOrDefault(p => p.Id_Worker == KodWor &&  p.Date == datePr);


                try
                {
                    if (workerBe.WorkerIsBe == 1)
                    {
                        MessageBox.Show("В этот день сотрудник работал в офисе");
                    }
                    else if (workerBe.WorkerIsBe == 0)
                    {
                        MessageBox.Show("В этот день сотрудник работал дистанционно");
                    }
                    else
                    {
                        MessageBox.Show("В этот день сотрудник работал дистанционно");
                    }
                }
                catch
                {
                    MessageBox.Show("В этот день сотрудник работал дистанционно");
                }
            }                
            //.Where(p => p.CreationDate.Date == PickerStart.SelectedDate)
        }
    }
}
