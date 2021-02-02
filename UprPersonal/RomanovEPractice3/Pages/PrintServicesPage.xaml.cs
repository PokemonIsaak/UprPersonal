using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrintDialog = System.Windows.Controls.PrintDialog;

namespace RomanovEPractice3.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintServicesPage.xaml
    /// </summary>
    public partial class PrintServicesPage : Page
    {
        public PrintServicesPage()
        {
            InitializeComponent();
            var listOfWorkers = AppData.Context.Workers.ToList();
            var result = new StringBuilder();

            // Основные теги перед генерацией таблицы.
            result.Append("<html>");
            result.Append("<meta charset='utf-8'/>");
            result.Append("<body>");
            // Заголовок отчета.
            result.Append("<p align='center'> <b>Отчет по сотрудникам сайта</b> </p>");
            // Тег с параметрами таблицы.
            result.Append("<table width=100% border=1 bordercolor=#000 style='border-collapse:collapse;'>");
            // Настройка строк и столбцов внутри. tr - строка, td - столбец.
            result.Append("<tr>");
            // Необходимые заголовки таблицы.
            result.Append("<td align=center><b>Код</b></td>");
            result.Append("<td align=center><b>Фамилия Имя Отчество</b></td>");
            result.Append("<td align=center><b>Дата</b></td>");
            result.Append("<td align=center><b>Время</b></td>");
            result.Append("<td align=center><b>Должность</b></td>");
            result.Append("<td align=center><b>Зарплата</b></td>");
            result.Append("</tr>");
            // Генерация содержимого через цикл.
            foreach (var item in listOfWorkers)
            {
                // Настройка строк и столбцов внутри. tr - строка, td - столбец.
                result.Append("<tr>");
                // Обращение к переменной item и получение необходимого свойства в соответствии с заголовком.
                result.Append($"<td align=center>{item.Id_Worker}</td>");
                result.Append($"<td align=center>{item.FIO}</td>");
                result.Append($"<td align=center>{item.DateOfBirth.ToString("dd.MM.yyyy")}</td>");
                result.Append($"<td align=center>{item.TimeOfBirth.ToString()} руб.</td>");
                result.Append($"<td align=center>{item.WorkersPosition.PositionName}</td>");
                result.Append($"<td align=center>{item.Salary}</td>");
                result.Append("</tr>");
            }
            // Закрытие основных тегов.
            result.Append("</table>");
            result.Append("</body>");
            result.Append("</html>");
            // Загрузка отчета в WebBrowser.
            BrowserReport.NavigateToString(result.ToString());
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            // Чтобы подключить объект IHTMLDocument2 необходимо добавить ссылку на Microsoft.mshtml (References - Assemblies).
            var document = BrowserReport.Document as IHTMLDocument2;
            document.execCommand("Print");
        }

        private void BtnPrintPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog PD = new PrintDialog();
            if (PD.ShowDialog() == true)
            {
                var currentWindowsState = App.Current.MainWindow.WindowState;
                App.Current.MainWindow.WindowState = WindowState.Maximized;
                PD.PrintVisual(this, "PDF");
                App.Current.MainWindow.WindowState = currentWindowsState;
            }
        }
    }
}

