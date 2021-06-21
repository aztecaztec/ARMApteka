using Microsoft.Win32;
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
using System.Drawing;

namespace ARM_Apteka.Views
{
    /// <summary>
    /// Логика взаимодействия для AddDrugPage.xaml
    /// </summary>
    public partial class AddDrugPage : Page
    {
        PropertyController objProperty = new PropertyController();
        Drugs_propertyController objDrugs_property = new Drugs_propertyController();
        DrugsController objDrugs = new DrugsController();
        Drugs_imgController objImg = new Drugs_imgController();

        string category;
        
        /// <summary>
        /// Инициализация компонентов страницы "Добавить новый препарат"
        /// </summary>
        /// <param name="atccode">Категория АТХ</param>
        public AddDrugPage(string atccode)
        {
            InitializeComponent();
            category = atccode;
            PropertyComboBox.ItemsSource = objProperty.GetAllProperties();
        }

        /// <summary>
        /// Добавление стартового изображения для препарата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFirstImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Bitmaps|*.bmp|PNG files|*.png|JPEG files|*.jpg|GIF files|*.gif|TIFF files|*.tif|Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source =  new BitmapImage(new Uri(op.FileName));
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
        /// Добавление свойства к будующему препарату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTempPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PropertyComboBox.SelectedItem != null)
                {
                    if(PropertyListView.Items.Contains(PropertyComboBox.SelectedItem) == false)
                    {
                        PropertyListView.Items.Add(PropertyComboBox.SelectedItem);
                    }                    
                }
                else throw new Exception("Не удалось добавить свойство");

            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        /// <summary>
        /// Удаление свойства из будущего препарата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTempPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(PropertyListView.SelectedItem != null)
                {
                    PropertyListView.Items.Remove(PropertyListView.SelectedItem);
                }
                else throw new Exception("Не удалось удалить свойство");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        /// <summary>
        /// Удалить выбранное свойство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePropertyButton_Click(object sender, RoutedEventArgs e)
        {
            Property item = (Property)PropertyComboBox.SelectedItem;
            try
            {
                if(PropertyComboBox.SelectedItem != null)
                {
                    if (PropertyListView.Items.Contains(PropertyComboBox.SelectedItem) == true)
                    {
                        PropertyListView.Items.Remove(PropertyComboBox.SelectedItem);
                    }
                    objProperty.DeleteProperty(item.property1);
                    PropertyComboBox.ItemsSource = objProperty.GetAllProperties();
                }
                
                else throw new Exception("Не удалось удалить свойство");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        /// <summary>
        /// Добавить препарат и перейти на страниуц "Препараты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objDrugs.AddDrug(NameTextBox.Text,int.Parse(WarehousTextBox.Text),double.Parse(PriceTextBox.Text),category,double.Parse(DiscountTextBox.Text)))
                {
                    
                    if (imgPhoto.Source != null)
                    {
                        objImg.AddImage(objImg.ImageToByteConvert(imgPhoto.Source as BitmapImage), objDrugs.GetDrugsID(NameTextBox.Text));
                    }
                    if(PropertyListView.Items.Count != 0)
                    {
                        foreach(Property i in PropertyListView.Items)
                        {
                            objDrugs_property.AddDrugs_Properties(i.id, objDrugs.GetDrugsID(NameTextBox.Text));
                        }
                                                
                    }
                    MessageBox.Show("Данные добавленны");
                    this.NavigationService.Navigate(new DrugsPage(category));
                }

                
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }
    }
}
