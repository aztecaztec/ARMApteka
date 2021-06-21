using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;

namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Groups
    /// </summary>
    public class GroupsController
    {
        Core db = new Core();
        /// <summary>
        /// Получение названия группы пользователя
        /// </summary>
        /// <param name="role_id">id группы </param>
        /// <returns>Название группы</returns>
        public string GetRoleName(int role_id)
        {
            string value = db.context.Groups.Where(x => x.id == role_id).FirstOrDefault().name;
            return value;
        }
        /// <summary>
        /// Получение массива данных о группах пользователей
        /// </summary>
        /// <returns>Массив данных с информацией о группах пользователей</returns>
        public List<Groups> GetGroups()
        {
            List<Groups> value = db.context.Groups.Where(x => x.id > 1).ToList();
            return value;
        }

    }
}
