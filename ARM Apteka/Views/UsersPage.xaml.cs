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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UsersController objUser = new UsersController();
        GroupsController objGroups = new GroupsController();
        /// <summary>
        /// Инициализация компонентов страницы "Пользователи"
        /// </summary>
        public UsersPage()
        {
            InitializeComponent();
            GroupsComboBox.ItemsSource = objGroups.GetGroups();
            UsersDataGrid.ItemsSource = objUser.GetUsers();

        }

        /// <summary>
        /// Переход на страницу "Добавить нового пользователя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUsersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new AddUserPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Изменение принадлежности к группе выделенного пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeUserGroupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GroupsComboBox.SelectedItem != null)
                {
                    if(UsersDataGrid.SelectedItem != null)
                    {
                        Groups item = (Groups)GroupsComboBox.SelectedItem;
                        Users item2 = (Users)UsersDataGrid.SelectedItem;
                        if (objUser.UpdateUserGroup(item2.login,item.id))
                        {
                            MessageBox.Show("Успешное обновление");
                            UsersDataGrid.ItemsSource = objUser.GetUsers();
                        }
                    }
                    else throw new Exception("Выберите пользователя");
                }
                else throw new Exception("Сначала выберите роль");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Удаление выделенного пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (UsersDataGrid.SelectedItem != null)
                {
                    Users item = (Users)UsersDataGrid.SelectedItem;
                    if (objUser.DeleteUser(item.login))
                    {
                        MessageBox.Show("Успешное удаление");
                        UsersDataGrid.ItemsSource = objUser.GetUsers();
                    }
                }
                else throw new Exception("Выберите пользователя");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
