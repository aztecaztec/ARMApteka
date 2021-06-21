using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;

namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Drugs
    /// </summary>
    public class DrugsController
    {
        Core db = new Core();
        /// <summary>
        /// Получение массива данных о препаратах из базы данных по атх категории
        /// </summary>
        /// <param name="Code">АТХ - код</param>
        /// <returns>
        /// Массив данных о препаратах
        /// </returns>
        public List<Drugs> GetDrugs(string Code)
        {
            List<Drugs> arrayDrugs = db.context.Drugs.Where(x=>x.atc_code == Code).ToList();
            return arrayDrugs;
        }
        /// <summary>
        /// Получение названия препарата
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Имя препарата</returns>
        public string GetDrugsName(int id)
        {
            string value = db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault().drug_name;
            return value;
        }
        /// <summary>
        /// Получение кол-ва препаратов на складе
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Кол-во препаратов на складе</returns>
        public int GetDrugsCount(int id)
        {
            int value = db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault().drug_count;
            return value;
        }
        /// <summary>
        /// Получение цены препарата
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Цена препарата</returns>
        public double GetDrugsPrice(int id)
        {
            double value = db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault().drug_price;
            return value;
        }
        /// <summary>
        /// Получение значения скидки препарата
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>Скидка препарата</returns>
        public double GetDrugsDiscount(int id)
        {
            double value = Convert.ToDouble(db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault().discount);
            return value;
        }
        /// <summary>
        /// Получение id препарата
        /// </summary>
        /// <param name="name">название препарата</param>
        /// <returns>id препарата</returns>
        public int GetDrugsID(string name)
        {
            int value = db.context.Drugs.Where(x => x.drug_name == name).FirstOrDefault().drug_id;
            return value;
        }
        /// <summary>
        /// Удаление препарата из базы данных
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <returns>
        /// true, если удаление успешно
        /// false или исключение, если удаление провалилось
        /// </returns>
        public bool DeleteDrugs(int id)
        {
            try
            {
                db.context.Drugs.Remove(db.context.Drugs.Where(x=>x.drug_id == id).FirstOrDefault());
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка удаления из бд"); }
        }
        /// <summary>
        /// Добавление препарата в базу данных
        /// </summary>
        /// <param name="name">название препарата</param>
        /// <param name="count">кол-во на складе</param>
        /// <param name="price">Цена</param>
        /// <param name="atc_code">Код АТХ</param>
        /// <param name="discount">значение скидки</param>
        /// <returns>
        /// true, если добавление успешно
        /// false или исключение, если добавление провалено
        /// </returns>
        public bool AddDrug(string name,int count,double price,string atc_code,double discount)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(name) == false 
                    && String.IsNullOrWhiteSpace(count.ToString()) == false 
                    && String.IsNullOrWhiteSpace(price.ToString()) == false 
                    && String.IsNullOrWhiteSpace(atc_code) == false 
                    && String.IsNullOrWhiteSpace(discount.ToString()) == false)
                {
                    if (price > 0 && discount >= 0 && discount <= 100 && count >= 0)
                    {
                        if (db.context.Drugs.Where(x => x.drug_name == name).Count() == 0)
                        {
                            Drugs objDrugs = new Drugs
                            {
                                drug_name = name,
                                drug_count = count,
                                drug_price = price,
                                atc_code = atc_code,
                                discount = discount
                            };
                            db.context.Drugs.Add(objDrugs);
                            db.context.SaveChanges();
                            if (db.context.SaveChanges() == 0) return true; else return false;
                        }
                        else throw new Exception("Данны неправильные значения для добавления");
                    }
                    else throw new Exception("Данны неправильные значения для добавления");

                }else throw new Exception("Данны неправильные значения для добавления");

            }
            catch { throw new Exception("ошибка добавления в бд"); }

        }
        /// <summary>
        /// Редактирование информации о препарате
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <param name="name">Новое название препарата</param>
        /// <param name="count">Новое кол-во на складе</param>
        /// <param name="price">Новая цена</param>
        /// <param name="discount">Новое значение скидки</param>
        /// <returns>
        /// true, если обновление успешно
        /// false или исключение, если обновление провалено
        /// </returns>
        public bool UpdateDrug(int id,string name,int count, double price, double discount)
        {
            
                if (String.IsNullOrWhiteSpace(name) == false
                    && String.IsNullOrWhiteSpace(count.ToString()) == false
                    && String.IsNullOrWhiteSpace(price.ToString()) == false
                    && String.IsNullOrWhiteSpace(id.ToString()) == false
                    && String.IsNullOrWhiteSpace(discount.ToString()) == false)
                {
                    if (price > 0 && discount >= 0 && discount <= 100 && count >= 0)
                    {
                        var con = db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault();
                        con.drug_name = name;
                        con.drug_count = count;
                        con.drug_price = price;
                        con.discount = discount;
                            db.context.SaveChanges();
                            if (db.context.SaveChanges() == 0) return true; else return false;
                    }
                        else throw new Exception("Данны неправильные значения обновления");
                } else throw new Exception("Данны неправильные значения для обновления");

        }

        /// <summary>
        /// Обновление кол-ва препарата на складе в случае продажи
        /// </summary>
        /// <param name="id">id препарата</param>
        /// <param name="count">кол-во на складе</param>
        /// <returns>
        /// true, если обновление успешно
        /// false или исключение, если обновление провалено
        /// </returns>
        public bool UpdateDrugCount(int id, int count)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(count.ToString()) == false
                    && String.IsNullOrWhiteSpace(id.ToString()) == false)
                {
                    if (count >= 0)
                    {
                        var con = db.context.Drugs.Where(x => x.drug_id == id).FirstOrDefault();
                        con.drug_count = count;
                        db.context.SaveChanges();
                        if (db.context.SaveChanges() == 0) return true; else return false;
                    }
                    else throw new Exception("Данны неправильные значения обновления");
                }
                else throw new Exception("Данны неправильные значения для обновления");
            }
            catch { throw new Exception("ошибка добавления в бд"); }
        }


    }
}
