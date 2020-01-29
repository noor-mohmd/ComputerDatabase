using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer.Database.Data
{
    public class ComputerData
    {
        public string ComputerName { get; set; }
        public string IntroducedDate { get; set; }
        public string DiscontinuedDate { get; set; }
        public string Company { get; set; }
        public string ErrorOnField { get; set; }

        public string NewComputerName { get; set; }
        public string NewIntroducedDate { get; set; }
        public string NewDiscontinuedDate { get; set; }
        public string NewCompany { get; set; }
    }
}
