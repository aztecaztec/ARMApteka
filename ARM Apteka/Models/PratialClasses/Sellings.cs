using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Controllers;


namespace ARM_Apteka.Models
{
    public partial class Sellings
    {

        DrugsController objDrugs = new DrugsController();
        public string drug_name
        {
            get { return objDrugs.GetDrugsName(drug_id); }
            set { }
        }

    }
}
