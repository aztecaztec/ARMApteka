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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ARM_Apteka.Models;
using ARM_Apteka.Properties;
using ARM_Apteka.Controllers;
using System.IO;
using System.ComponentModel;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для DrugsPage.xaml
    /// </summary>
    public partial class DrugsPage : Page
    {
        string Category;
        int idDrugs;
        DrugsController objDrugs = new DrugsController();
        Drugs_imgController objDrugs_img = new Drugs_imgController();
        Drugs_propertyController objDrugs_property = new Drugs_propertyController();
        SellingsController objSell = new SellingsController();


        /// <summary>
        /// Инициализация страницы "Препараты"
        /// </summary>
        /// <param name="ATCCategory">Код АТХ категории</param>
        public DrugsPage(string ATCCategory)
        {
            InitializeComponent();
            if(Settings.Default.saveRoleString != "1" && Settings.Default.saveRoleString != "2")
            {
                AddDrugsButton.Visibility = Visibility.Collapsed;
                ChangeDescButton.Visibility = Visibility.Collapsed;
                DeleteDrugButton.Visibility = Visibility.Collapsed;

            }
            if (objDrugs.GetDrugs(ATCCategory).Count() == 0)
            {
                MessageStackPanel.Visibility = Visibility.Visible;
            }
            Category = ATCCategory;
            DrugsListView.ItemsSource = objDrugs.GetDrugs(ATCCategory);
        }

        /// <summary>
        /// Переход на страницу "Добавить новое лекарство"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrugsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddDrugPage(Category));   
        }


        /// <summary>
        /// Изменение информации для подробной информации о препарате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrugsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DrugsListView.SelectedItem != null)
            {
                Drugs item = (Drugs)DrugsListView.SelectedItem;
                idDrugs = item.drug_id;
                MemoryStream byteStream = new MemoryStream(objDrugs_img.GetFirstImage(idDrugs));
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                FocusedImage.Source = image;
                ImageDrugListView.ItemsSource = objDrugs_img.GetImages(idDrugs);
                NameDrugTextBlock.Text = item.drug_name;
                CountDrugTextBlock.Text = item.drug_count.ToString();
                PriceDrugTextBlock.Text = item.drug_price.ToString();
                DiscountDrugTextBlock.Text = item.discountPrice.ToString();
                ATCDrugTextBlock.Text = item.atc_code;
                PropertiesDrugListView.ItemsSource = objDrugs_property.GetDrugs_Properties(idDrugs);
            }
            
        }

        /// <summary>
        /// Переход на страницу "Изменить информацию о препарате"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDescButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UpdateDrugPage(idDrugs,Category));
        }

        /// <summary>
        /// Удаление выделенного препарата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDrugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DrugsListView.SelectedItem != null)
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
                    doubleAnimation.From = Animation.ActualWidth;
                    doubleAnimation.To = 0;
                    Animation.BeginAnimation(Grid.WidthProperty, doubleAnimation);
                    Drugs item = (Drugs)DrugsListView.SelectedItem;

                    int id = item.drug_id;

                    
                    if (objDrugs.DeleteDrugs(id))
                    {
                        MessageBox.Show("Препарат успешно удален");
                        DrugsListView.ItemsSource = objDrugs.GetDrugs(Category);
                    }
                }
                else MessageBox.Show("выбери вариант");
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}

        /// <summary>
        /// Добавление информации о проданных препаратах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellDrugsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Drugs item = (Drugs)DrugsListView.SelectedItem;
                if (SellCountTextBox.Text.Contains(" ") == false && String.IsNullOrWhiteSpace(SellCountTextBox.Text) == false)
                {
                    if (int.TryParse(SellCountTextBox.Text, out int number))
                    {
                        if (number > 0 && number <= objDrugs.GetDrugsCount(idDrugs))
                        {
                           if (objDrugs.UpdateDrugCount(idDrugs, (objDrugs.GetDrugsCount(idDrugs) - number)))
                           {
                                objSell.AddSellings(idDrugs, number, item.discountPrice * number);
                                DoubleAnimation doubleAnimation = new DoubleAnimation();
                                doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
                                doubleAnimation.From = Animation.ActualWidth;
                                doubleAnimation.To = 0;
                                Animation.BeginAnimation(Grid.WidthProperty, doubleAnimation);
                                MessageBox.Show("Продажа успешно совершена");
                                DrugsListView.ItemsSource = objDrugs.GetDrugs(Category);

                            }
                        }
                        else throw new Exception("Введенное значение меньше или равно нулю или больше, чем имеется на складе");
                    }
                    else throw new Exception("Введенное значение не целое число");
                }
                else throw new Exception("Поле пусто или имеет пробелы");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Закрыть вкладку с подробной информацией о препаратах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseInfoButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimation.From = Animation.ActualWidth;
            doubleAnimation.To = 0;
            Animation.BeginAnimation(Grid.WidthProperty, doubleAnimation);
        }

        /// <summary>
        /// Открыть подробную информацию о выделенном препарате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDrugInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Drugs)DrugsListView.SelectedItem != null)
            {
                DoubleAnimation showAnimation = new DoubleAnimation();
                showAnimation.Duration = TimeSpan.FromSeconds(0.3);
                showAnimation.From = Animation.ActualWidth;
                showAnimation.To = 800;
                Animation.BeginAnimation(Grid.WidthProperty, showAnimation);
            }
            else MessageBox.Show("выбери вариант");
        }


        /// <summary>
        /// Выбор фильтра сортировки препаратов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(SortingComboBox.SelectedItem != null)
                {
                    switch (SortingComboBox.SelectedIndex)
                    {
                        case 0:
                            DrugsListView.Items.SortDescriptions.Clear();
                            break;
                        case 1:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("drug_name", ListSortDirection.Ascending));
                            break;
                        case 2:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("drug_name", ListSortDirection.Descending));
                            break;
                        case 3:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("drug_price", ListSortDirection.Ascending));
                            break;
                        case 4:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("drug_price", ListSortDirection.Descending));
                            break;
                        case 5:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("discountPrice", ListSortDirection.Ascending));
                            break;
                        case 6:
                            DrugsListView.Items.SortDescriptions.Clear();
                            DrugsListView.Items.SortDescriptions.Add(new SortDescription("discountPrice", ListSortDirection.Descending));
                            break;

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Выбор изображения для показа в более высоком разрешении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageDrugListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(ImageDrugListView.SelectedItem != null)
                {
                    Drugs_img item = (Drugs_img)ImageDrugListView.SelectedItem;
                    int idimg = item.img_id;
                    MemoryStream byteStream = new MemoryStream(objDrugs_img.GetImage(idimg));
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = byteStream;
                    image.EndInit();
                    FocusedImage.Source = image;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
