using System;
using System.Collections.Generic;
using System.Data;
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
using ARM_Apteka.Models;
using ARM_Apteka.Controllers;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        ATCController objATC;
        /// <summary>
        /// Инициализация компонентов для страницы "Категория АТХ"
        /// </summary>
        public CategoryPage()
        {
            InitializeComponent();
            objATC = new ATCController();
            CategoryDataGrid.ItemsSource = objATC.GetATC();



        }

        /// <summary>
        /// Переход к дочерней категории АТХ или на страницу "Препараты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ATC item = CategoryDataGrid.SelectedItem as ATC;
                if (objATC.CheckATCChildren(item.ATCCode))
                {
                    CategoryDataGrid.ItemsSource = objATC.GetATCChildren(item.ATCCode);
                }
                else
                {
                    this.NavigationService.Navigate(new DrugsPage(item.ATCCode));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Возврат к изначальному списку категорий АТХ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryDataGrid.ItemsSource = objATC.GetATC();
        }

    }
}
