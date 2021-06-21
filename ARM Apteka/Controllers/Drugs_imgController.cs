using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.IO;
using ARM_Apteka.Properties;
using ARM_Apteka.Models;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Drug_img
    /// </summary>
    public class Drugs_imgController
    {
        Core db = new Core();
        
        /// <summary>
        /// Получение первого изображения
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>
        /// byteArray - изображение, если оно найдено
        /// placeholder.toByteArray - если не найдено изображений
        /// </returns>
        public byte[] GetFirstImage(int id)
        {
            ImageConverter convert = new ImageConverter();
            byte[] byteArray;
            if (db.context.Drugs_img.Where(x => x.drug_id == id).Count() > 0)
            {
                byteArray = db.context.Drugs_img.Where(x => x.drug_id == id).FirstOrDefault().img;
                return byteArray;
            }
            else return (byte[])convert.ConvertTo(Resources.placeholder,typeof(byte[]));
            
        }
        /// <summary>
        /// Получение изображения
        /// </summary>
        /// <param name="id">id изображения</param>
        /// <returns>
        /// byteArray - изображение, если оно есть
        /// placeholder.toByteArray - если не найдено изображений
        /// </returns>
        public byte[] GetImage(int id)
        {
            ImageConverter convert = new ImageConverter();
            byte[] byteArray;
            if (db.context.Drugs_img.Where(x => x.img_id == id).Count() > 0)
            {
                byteArray = db.context.Drugs_img.Where(x => x.img_id == id).FirstOrDefault().img;
                return byteArray;
            }
            else return (byte[])convert.ConvertTo(Resources.placeholder, typeof(byte[]));

        }
        /// <summary>
        /// Получения массива изображения
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Массив изображений</returns>
        public List<Drugs_img> GetImages(int id)
        {
            
            List<Drugs_img> arrayImg;
            if (db.context.Drugs_img.Where(x => x.drug_id == id).Count() > 0)
            {
              return arrayImg = db.context.Drugs_img.Where(x => x.drug_id == id).ToList();
              
            }
            else
            {
                AddImage(ImageToByteConvert(Resources.placeholder), id);
                return arrayImg = db.context.Drugs_img.Where(x => x.drug_id == id).ToList();
            }
             
        }
        /// <summary>
        /// Конвертация изображения для базы данных
        /// </summary>
        /// <param name="image">Bitmap изображение</param>
        /// <returns>Массив байтов, содержащее изображение</returns>
        public byte[] ImageToByteConvert(Bitmap image)
        {
            ImageConverter convert = new ImageConverter();
            byte[] byteArray = (byte[])convert.ConvertTo(image, typeof(byte[]));
            return byteArray;
        }
        /// <summary>
        /// Конвертация изображения для базы данных
        /// </summary>
        /// <param name="imageC">BitmapImage изображение</param>
        /// <returns>Массив байтов, содержащее изображение</returns>
        public byte[] ImageToByteConvert(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        /// <summary>
        /// Добавление изображения в базу данных
        /// </summary>
        /// <param name="image">массив байт, содаржащее изображение</param>
        /// <param name="id">id препарата</param>
        /// <returns>
        /// true - если добавление успешно
        /// false или исключение, если добавление провалено
        /// </returns>
        public bool AddImage(byte[] image, int id)
        {
            try
            {
                Drugs_img objDrugs_img = new Drugs_img
                {
                    drug_id = id,
                    img = image
                };
                db.context.Drugs_img.Add(objDrugs_img);
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка добавления в бд"); }
            
        }
        /// <summary>
        /// Удаление изображения из базы данных
        /// </summary>
        /// <param name="ImageId">id - изображения</param>
        /// <returns>
        /// true - если удаление успешно
        /// false или исключение, если удаление провалено
        /// </returns>
        public bool DeleteImage(int ImageId)
        {
            try
            {
                db.context.Drugs_img.Remove(db.context.Drugs_img.Where(x => x.img_id == ImageId).FirstOrDefault());
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка удаления из бд"); }
        }
    }
}
