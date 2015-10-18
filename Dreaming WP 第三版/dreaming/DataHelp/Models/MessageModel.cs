using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
   public class MessageModel
    {

        [AutoIncrement, PrimaryKey,Unique]
        public int Id { get; set; }
        public string msg { get; set; }
        [Indexed]
        public string myPhone { get; set; }
        public string myImage { get; set; }
        public string myName { get; set; }
        public string myDream { get; set; }
       [Indexed]
        public string toPhone { get; set; }
        public string toImage { get; set; }
        public string toName { get; set; }
        public string toDream { get; set; }
        public string time { get; set; }
        public int type { get; set; } //0 文字　１语音　２图片　３表情
    }
}
