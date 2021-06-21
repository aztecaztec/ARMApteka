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
using ARM_Apteka.Models;
using ARM_Apteka.Controllers;
using Microsoft.Win32;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateDrugPage.xaml
    /// </summary>
    public partial class UpdateDrugPage : Page
    {
        int id;
        string category;
        DrugsController objDrugs = new DrugsController();
        PropertyController objProperty = new PropertyController();
        Drugs_imgController objDrug_img = new Drugs_imgController();
        Drugs_propertyController objDrugs_property = new Drugs_propertyController();
        /// <summary>
        /// Инициализация компонентов на странице "Обновить информацию о препарате"
        /// </summary>
        /// <param name="id_get">id обновляемого препарата</param>
        /// <param name="ATCCode">Код АТХ Категории</param>
        public UpdateDrugPage(int id_get,string ATCCode)
        {
            InitializeComponent();
            id = id_get;
            category = ATCCode;
            NameTextBox.Text = objDrugs.GetDrugsName(id);
            WarehousTextBox.Text = objDrugs.GetDrugsCount(id).ToString();
            PriceTextBox.Text = objDrugs.GetDrugsPrice(id).ToString();
            DiscountTextBox.Text = objDrugs.GetDrugsDiscount(id).ToString();
            PropertyComboBox.ItemsSource = objProperty.GetAllProperties();
            PropertyListView.ItemsSource = objDrugs_property.GetDrugs_Properties(id);
            ImageDrugListView.ItemsSource = objDrug_img.GetImages(id);

        }

        /// <summary>
        /// Добавление изображений в галерею изображений препарата 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "Bitmaps|*.bmp|PNG files|*.png|JPEG files|*.jpg|GIF files|*.gif|TIFF files|*.tif|Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";
                if (op.ShowDialog() == true)
                {
                    objDrug_img.AddImage(objDrug_img.ImageToByteConvert(new BitmapImage(new Uri(op.FileName))),id);
                    ImageDrugListView.ItemsSource = objDrug_img.GetImages(id);
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Удаление выбранных изображений из галереи препарата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ImageDrugListView.SelectedItem != null)
                {
                    int imgID;
                    Drugs_img item = (Drugs_img)ImageDrugListView.SelectedItem;
                    imgID = item.img_id;
                    if (objDrug_img.DeleteImage(imgID))
                    {
                        MessageBox.Show("Удалено");
                        ImageDrugListView.ItemsSource = objDrug_img.GetImages(id);
                    }
                    
                }
                else throw new Exception("Выберите фото");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Добавление нового свойства в препарат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPropertyDrugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PropertyComboBox.SelectedItem != null)
                {
                    Property item = (Property)PropertyComboBox.SelectedItem;
                    if (objDrugs_property.InspectDrugs_propertiesMatching(item.id,id))
                    {
                        objDrugs_property.AddDrugs_Properties(item.id, id);
                        PropertyListView.ItemsSource = objDrugs_property.GetDrugs_Properties(id);
                    }

                }
                else throw new Exception("Выберите, какое свойство добавить");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        /// <summary>
        /// Удаление выбранного свойства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePropertyButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Property item = (Property)PropertyComboBox.SelectedItem;
                if (PropertyComboBox.SelectedItem != null)
                {
                    if (PropertyListView.Items.Contains(PropertyComboBox.SelectedItem) == true)
                    {
                        PropertyListView.Items.Remove(PropertyComboBox.SelectedItem);
                        
                    }
                    objProperty.DeleteProperty(item.property1);
                    PropertyComboBox.ItemsSource = objProperty.GetAllProperties();
                    PropertyListView.ItemsSource = objDrugs_property.GetDrugs_Properties(id);
                }

                else throw new Exception("Не удалось удалить свойство");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        /// <summary>
        /// Создание нового свойства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objProperty.AddProperty(AddNewPropertyTextBox.Text);
                PropertyComboBox.ItemsSource = objProperty.GetAllProperties();
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        /// <summary>
        /// Удаление выбранного свойства из препарата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePropertyDrugButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (PropertyListView.SelectedItem != null)
                {
                    Drugs_property item =(Drugs_property)PropertyListView.SelectedItem;
                    objDrugs_property.DeleteProperty(item.id);
                    PropertyListView.ItemsSource = objDrugs_property.GetDrugs_Properties(id);
                }
                else throw new Exception("Сначала выберите, что удалять");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обновление информации о препарате и переход на страницу "Препараты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDrugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(objDrugs.UpdateDrug(id, NameTextBox.Text, int.Parse(WarehousTextBox.Text), double.Parse(PriceTextBox.Text), double.Parse(DiscountTextBox.Text)))
                {
                    MessageBox.Show("Успешное обновление");
                    this.NavigationService.Navigate(new DrugsPage(category));
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
