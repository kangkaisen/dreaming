using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelp.Models
{
    public class UserModel:ModelBase
    {
        [AutoIncrement,PrimaryKey,Unique]
        public int Id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }

        public string image { get; set; }

        public string dream { get; set; }

        public string day { get; set; }

        

        private bool read;

        public bool isRead
        {
            get { return read; }
            set
            { this.SetProperty(ref this.read, value); }
        }
        
        
    }
}
