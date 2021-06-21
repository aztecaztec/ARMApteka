using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Controllers;

namespace ARM_Apteka.Models
{
    public partial class Drugs_property
    {
        DrugsController objDrugs = new DrugsController();
        PropertyController objProperty = new PropertyController();

        public string drug_name
        {
            get { return objDrugs.GetDrugsName(drug_id); }
            set { }
        }
        public string property
        {
            get { return objProperty.GetProperty(property_id); }
            set { }
        }
    }
}
