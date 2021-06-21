using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;

namespace ARM_Apteka.Controllers
{
    
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Sellings
    /// </summary>
    public class SellingsController
    {
        Core db = new Core();
        /// <summary>
        /// Получение Массива данных с информацией о продажах
        /// </summary>
        /// <returns>Массив данных</returns>
        public List<Sellings> GetSellings()
        {
            try
            {
                List<Sellings> value = db.context.Sellings.ToList();
                return value;
            }
            catch { throw new Exception("что-то пошло не так"); }

        }
        /// <summary>
        /// Получение Массива данных с информацией о продажах по определенной дате
        /// </summary>
        /// <param name="dateStart">Дата начала</param>
        /// <param name="dateEnd">Дата конца</param>
        /// <returns>Массив данных</returns>
        public List<Sellings> GetSFilteredSellings(DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                var result = DateTime.Compare(dateStart, dateEnd);
                if (result < 0 || result == 0)
                {
                    List<Sellings> value = db.context.Sellings.Where(x => x.date >= dateStart && x.date <= dateEnd).ToList();
                    return value;
                }
                else throw new Exception("что-то пошло не так");


            }
            catch { throw new Exception("что-то пошло не так"); }

        }
        /// <summary>
        /// Добавить информацию о продажах в базу данных
        /// </summary>
        /// <param name="drug_id">id препарата</param>
        /// <param name="count">кол-во проданных препаратов</param>
        /// <param name="fullprice">Полная цена, за которую были проданны препараты</param>
        /// <returns>
        /// true, если добавление успешно
        /// else или исключение, если добавление провалено
        /// </returns>
        public bool AddSellings(int drug_id,int count,double fullprice)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(drug_id.ToString()) == false 
                    && String.IsNullOrWhiteSpace(count.ToString()) == false 
                    && String.IsNullOrWhiteSpace(fullprice.ToString()) == false)
                {
                    if (count > 0 && fullprice >= 0)
                    {
                        
                        Sellings objSell = new Sellings
                        {
                            drug_id = drug_id,
                            count = count,
                            fullprice = fullprice,
                            date = DateTime.Now.Date
                        };
                        db.context.Sellings.Add(objSell);
                        db.context.SaveChanges();
                        if (db.context.SaveChanges() == 0) return true; else return false;
                        
                        
                    }
                    else throw new Exception("Данны неправильные значения для добавления");

                }else throw new Exception("Данны неправильные значения для добавления");

            }
            catch { throw new Exception("ошибка добавления в бд"); }

        }
    }
}
