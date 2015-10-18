using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
   public  class CommentModel
    {
        public string post_id { get; set; }
        public string user_phone { get; set; }
        public string user_name { get; set; }
        public string user_image { get; set; }
        public string time { get; set; }

        public string content { get; set; }
        public string at_name { get; set; }
        public string at_phone { get; set; }
        public string at_image { get; set; }
    }
}
