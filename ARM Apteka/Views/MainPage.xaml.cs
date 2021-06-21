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
using ARM_Apteka.Properties;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        /// <summary>
        /// Инициализация компонентов главной страницы
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            if(Settings.Default.saveRoleString != "1")
            {
                UsersButton.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Переход на страницу "Категории лекарств"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CategoryPage());
        }

        /// <summary>
        /// Переход на страницу "История продаж"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HistoryPage());
        }

        /// <summary>
        /// Переход на страницу "Пользователи"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UsersPage());
        }
    }
}
