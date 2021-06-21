using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;
using System.Runtime;
namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Property
    /// </summary>
    public class PropertyController
    {
        Core db = new Core();

        /// <summary>
        /// Получение названия свойства
        /// </summary>
        /// <param name="id">id свойства</param>
        /// <returns>Название свойства</returns>
        public string GetProperty(int id)
        {
            string property = db.context.Property.Where(x => x.id == id).FirstOrDefault().property1;
            return property;
        }
        
        /// <summary>
        /// Добавление свойства в базу данных
        /// </summary>
        /// <param name="property">Название свойства</param>
        /// <returns>
        /// true, если добавление успешно
        /// else или исключение, если добавление провалено
        /// </returns>
        public bool AddProperty(string property)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(property) == false)
                {
                    if(db.context.Property.Where(x=>x.property1 == property).Count() == 0)
                    {
                        Property objProperty = new Property
                        {
                            property1 = property
                        };
                        db.context.Property.Add(objProperty);
                        db.context.SaveChanges();
                        if (db.context.SaveChanges() == 0) return true; else return false;
                    }
                    
                }
                throw new Exception("Данны неправильные значения для добавления");
                
            }
            catch { throw new Exception("ошибка добавления в бд"); }

        }
        /// <summary>
        /// Получение Масива данных с информацие о свойствах
        /// </summary>
        /// <returns>Возвращает свойства</returns>
        public List<Property> GetAllProperties()
        {
            List <Property> properties = db.context.Property.ToList();
            return properties;
        }
        /// <summary>
        /// Удаление свойства из базы данных
        /// </summary>
        /// <param name="property"></param>
        /// <returns>
        /// true, если удаление успешно
        /// else или исключение, если удаление провалено
        /// </returns>
        public bool DeleteProperty(string property)
        {
            try
            {
                db.context.Property.Remove(db.context.Property.Where(x => x.property1 == property).FirstOrDefault());
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка удаления из бд"); }
        }
    }
}
