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
using ARM_Apteka.Controllers;
using ARM_Apteka.Views;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        /// <summary>
        /// Инициализация компонентов страницы "Авторизация"
        /// </summary>
        public AuthorizationPage()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Авторизация пользователя и переход на главную страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignInbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                UsersController uc = new UsersController();
                if (uc.Auth(LoginTextBox.Text, PasswordPasswordBox.Password))
                {
                    if (uc.GetCurrentUsersRole(LoginTextBox.Text) == 1)
                    {
                        Settings.Default.saveRoleString = "1";

                    }
                    if (uc.GetCurrentUsersRole(LoginTextBox.Text) == 2)
                    {
                        Settings.Default.saveRoleString = "2";

                    }
                    if (uc.GetCurrentUsersRole(LoginTextBox.Text) == 3)
                    {
                        Settings.Default.saveRoleString = "3";

                    }

                    this.NavigationService.Navigate(new MainPage());
                }
                else throw new Exception("Авторизация провалена");
                
            }
            catch (Exception g)
            {
                MessageBox.Show(g.ToString());
            }
        }
        
    }
}
