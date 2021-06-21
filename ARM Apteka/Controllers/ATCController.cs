using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;

namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей ATC
    /// </summary>
    class ATCController
    {
        Core db = new Core();

        /// <summary>
        /// Метод получение данных о начальных категориях АТХ без родителей
        /// </summary>
        /// <returns>
        ///List массив данных без кода родителя
        /// </returns>
        public List<ATC> GetATC()
        {
            List<ATC> arrayATC = db.context.ATC.Where(x => x.ParentATCCode == "").ToList();
            return arrayATC;
        }

        /// <summary>
        /// Метод получения данных по дочерним категориям АТХ
        /// </summary>
        /// <param name="pid">АТХ-код родителя</param>
        /// <returns>List массив с данными о дочерних категориях</returns>
        public List<ATC> GetATCChildren(string pid)
        {
            List<ATC> arrayATC = db.context.ATC.Where(x => x.ParentATCCode == pid).ToList();
            return arrayATC;
        }
        /// <summary>
        /// Метод проверки АТХ категории на наличие дочерних категорий
        /// </summary>
        /// <param name="pid">АТХ-код родителя</param>
        /// <returns>
        /// true - если есть
        /// false - если нет
        /// </returns>
        public bool CheckATCChildren (string pid)
        {
            if (db.context.ATC.Where(x => x.ParentATCCode == pid).Count() > 0) return true; else return false;
        }

        

    }
}
