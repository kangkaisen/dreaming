using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
    public class FollowerModel
    {
        public string _id { get; set; }
        public string cphone { get; set; }
        public string cname { get; set; }
        public string cimage { get; set; }
        public string cdream { get; set; }

        public string fphone { get; set; }
        public string fname { get; set; }
        public string fimage { get; set; }

        public string fdream { get; set; }  
    }
}
