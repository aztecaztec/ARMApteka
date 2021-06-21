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
using ARM_Apteka.Controllers;
using ARM_Apteka.Models;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        UsersController objUser = new UsersController();
        GroupsController objGroup = new GroupsController();
        /// <summary>
        /// Инициализация компонентов страницы "Добавить нового пользователя"
        /// </summary>
        public AddUserPage()
        {
            InitializeComponent();
            GroupComboBox.ItemsSource = objGroup.GetGroups();
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GroupComboBox.SelectedItem != null)
                {
                    if (String.IsNullOrWhiteSpace(LoginTextBox.Text) == false
                    && String.IsNullOrWhiteSpace(EmailTextBox.Text) == false
                    && String.IsNullOrWhiteSpace(NameTextBox.Text) == false)
                    {
                        Groups item = (Groups)GroupComboBox.SelectedItem;
                        if (objUser.AddUsers(LoginTextBox.Text, NameTextBox.Text, EmailTextBox.Text, item.id))
                        {
                            MessageBox.Show("Добавление произошло, авторизационные данные высланы");
                            this.NavigationService.Navigate(new UsersPage());
                        }
                    }
                    else throw new Exception("Поля должны быть наполнены");
                }
                else throw new Exception("Сначала выберите роль");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
