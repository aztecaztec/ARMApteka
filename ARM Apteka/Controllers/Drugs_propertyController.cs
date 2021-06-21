using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;
namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Drugs_property
    /// </summary>
    public class Drugs_propertyController
    {
        Core obj = new Core();
        /// <summary>
        /// Получение массива свойств, относящихся к препарату
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Массив данных</returns>
        public List<Drugs_property> GetDrugs_Properties(int id)
        {
            List<Drugs_property> drugs_Properties = obj.context.Drugs_property.Where(x => x.drug_id == id).ToList();
            return drugs_Properties;
        }
        /// <summary>
        /// Проверка совпадения свойств в препарате
        /// </summary>
        /// <param name="property">id свойства</param>
        /// <param name="drug">id препарата</param>
        /// <returns>
        /// true, если совпадения отсутствуют
        /// false, если совпадения присутствуют
        /// исключение, если произойдет ошибка
        /// </returns>
        public bool InspectDrugs_propertiesMatching(int property, int drug)
        {
            try
            {
                if (obj.context.Drugs_property.Where(x => x.property_id == property && x.drug_id == drug).Count() == 0)
                {
                    return true;
                }
                else return false;
            }
            catch { throw new Exception("ошибка поиска"); }
            
        }
        /// <summary>
        /// Добавление свойства в препарат
        /// </summary>
        /// <param name="IDProperties">id свойства</param>
        /// <param name="IDDrugs">id препарата</param>
        /// <returns>
        /// true, если добавление успешно
        /// false, если добавление провалено
        /// исключение, если есть совпадения
        /// </returns>
        public bool AddDrugs_Properties(int IDProperties, int IDDrugs)
        {

           
                if (InspectDrugs_propertiesMatching(IDProperties, IDDrugs))
                {
                    Drugs_property objDrugs_property = new Drugs_property
                    {
                        drug_id = IDDrugs,
                        property_id = IDProperties
                    };
                    obj.context.Drugs_property.Add(objDrugs_property);
                    obj.context.SaveChanges();
                    if (obj.context.SaveChanges() == 0) return true; else return false;

                }
                else throw new Exception("есть совпадения");



        }
        /// <summary>
        /// Удаление свойства из препарата
        /// </summary>
        /// <param name="drugProperty_id">id свойства в препарате</param>
        /// <returns>
        /// true, если удаление успешно
        /// false или исключение, если удаление провалено
        /// </returns>
        public bool DeleteProperty(int drugProperty_id)
        {
            try
            {
                Console.WriteLine(drugProperty_id);
                obj.context.Drugs_property.Remove(obj.context.Drugs_property.Where(x => x.id == drugProperty_id).FirstOrDefault());
                obj.context.SaveChanges();
                if (obj.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка удаления из бд"); }
        }
    }
}
