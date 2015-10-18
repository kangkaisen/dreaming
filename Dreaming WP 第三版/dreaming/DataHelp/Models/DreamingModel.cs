using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
   
   public class DreamingModel
    {
        public string _id { get; set; }
        public string user_image { get; set; }

        public string user_name { get; set; }
        public string user_phone { get; set; }

        public string time { get; set; }

        public string content { get; set; }
        public List<imageModel> image { get; set; }
        public string song { get; set; }
        public int comment_count { get; set; }
        public int praise_count { get; set; }

        public int type { get; set; } //0 梦想 1经验 2吐槽 3问答



        public class imageModel
        {

            public string i { get; set; }


        }
       
    }
}
