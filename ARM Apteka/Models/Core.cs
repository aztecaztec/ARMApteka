using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Apteka.Models
{
    /// <summary>
    /// класс подключения к базе данных
    /// </summary>
    public class Core
    {
        public Entities context = new Entities();
    }
}
