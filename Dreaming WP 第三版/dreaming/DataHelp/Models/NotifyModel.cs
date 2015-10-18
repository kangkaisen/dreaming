using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
   public class NotifyModel
    {
        [AutoIncrement, PrimaryKey,Indexed]
        public int Id { get; set; }
        public string phone{ get; set; }
        public string image{ get; set; }
        public string name { get; set; }
        public string pid { get; set; } //文章ID
        public string cid { get; set; } //评论ID
        public string time { get; set; }
        [Indexed]
        public int type { get; set; } //0 赞　１评论  2回复评论  3关注
    }
}
