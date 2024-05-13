using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Models
{
    public class Patient
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public int ci {  get; set; }
        public string bloodType { get; set; }
        public string patientCode { get; set; }

        public string GetInfo()
        {
            return $"{name},{lastName},{ci},{bloodType},{patientCode}\n";
        }
    }
}
