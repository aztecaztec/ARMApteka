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
using ARM_Apteka.Controllers;
using ARM_Apteka.Models;
using Microsoft.Win32;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        SellingsController obj = new SellingsController();
        /// <summary>
        /// Инициализация компонентов страницы "История продаж"
        /// </summary>
        public HistoryPage()
        {
            InitializeComponent();
            HistoryDataGrid.ItemsSource = obj.GetSellings();
            DateEndDatePicker.SelectedDate = DateTime.Now.Date;
            
        }

        /// <summary>
        /// Проверка правильности выбора дат в DatePicker'ах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateStartDatePicker.SelectedDate != null && DateEndDatePicker.SelectedDate != null)
            {
                var result = DateTime.Compare(Convert.ToDateTime(DateStartDatePicker.SelectedDate), Convert.ToDateTime(DateEndDatePicker.SelectedDate));
                if (result > 0)
                {
                    MessageBox.Show("Надо чтобы начальная дата не была выше конечной");
                    DateStartDatePicker.SelectedDate = DateEndDatePicker.SelectedDate;
                }
            }
            
        }

        /// <summary>
        /// Отправка данных в CSV файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCSVButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog file = new SaveFileDialog();
                file.FileName = "Test";
                string fileCSV = "";
                file.Filter = "Text files(*.csv)|*.csv";

                file.Title = "Сохранение файла csv";
                if (file.ShowDialog() == true)
                {
                    for (int f = 0; f < HistoryDataGrid.Columns.Count; f++)
                    {
                        fileCSV += (HistoryDataGrid.Columns[f].Header.ToString() + ";");
                    }

                    fileCSV += "\t\n";

                    for (int i = 0; i < HistoryDataGrid.Items.Count; i++)
                    {


                        Sellings item = (Sellings)HistoryDataGrid.Items[i];
                        fileCSV += (item.drug_name).ToString() + ";";
                        fileCSV += (item.count).ToString() + ";";
                        fileCSV += (item.fullprice).ToString() + ";";
                        fileCSV += (item.date).ToString() + ";";

                        fileCSV += "\n";
                        StreamWriter wr = new StreamWriter(file.FileName, false, Encoding.GetEncoding("UTF-8"));
                        wr.Write(fileCSV);
                        wr.Close();

                    }
                }


                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    

        /// <summary>
        /// Фильтрация DataGrid'а по выбранной дате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateStartDatePicker.SelectedDate != null && DateEndDatePicker.SelectedDate != null)
                {
                    HistoryDataGrid.ItemsSource = obj.GetSFilteredSellings(Convert.ToDateTime(DateStartDatePicker.SelectedDate), Convert.ToDateTime(DateEndDatePicker.SelectedDate));
                }
                else throw new Exception("Выберите дату"); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
