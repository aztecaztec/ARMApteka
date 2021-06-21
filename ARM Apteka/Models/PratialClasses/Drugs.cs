using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Controllers;

namespace ARM_Apteka.Models
{
    public partial class Drugs
    {
        Drugs_imgController obj = new Drugs_imgController();
        public byte[] img
        {
            get { return obj.GetFirstImage(drug_id);}
            set { }
        }
        public double discountPrice
        {
            get { return drug_price - drug_price / 100 * Convert.ToDouble(discount); }
            set { }
        }
    }
}
