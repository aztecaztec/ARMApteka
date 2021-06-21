using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Controllers;

namespace ARM_Apteka.Models
{
    public partial class Users
    {
        GroupsController obj = new GroupsController();
        /// <summary>
        /// Получение названия роли по его id
        /// </summary>
        public string role_name
        {
            get { return obj.GetRoleName(user_group); }
            set { }
        }
    }
}
