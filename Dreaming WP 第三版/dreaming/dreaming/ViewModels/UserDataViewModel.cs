using DataHelp.Common;
using DataHelp.Models;
using dreaming.Command;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreaming.ViewModels
{
   public class UserDataViewModel:ModelBase
    {
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set
            { this.SetProperty(ref this.imagePath, value); }
        }


        private string  name;

        public string  Name
        {
            get { return name; }
            set
            { this.SetProperty(ref this.name, value); }
        }

        private string dream;

        public string Dream
        {
            get { return dream; }
            set
            { this.SetProperty(ref this.dream, value); }
        }

        private string tag;

        public string Tag
        {
            get { return tag; }
            set
            { this.SetProperty(ref this.tag, value); }
        }
        
        public DelegateCommand UpdateCommand { get; set; }

        public UserDataViewModel()
        {
            ImagePath = Config.UserLocalImage;
            Name = Config.UserName;
            Dream = Config.UserDream;
            Tag = Config.UserTag;
            UpdateCommand = new DelegateCommand(Update);
        }

        public void Update()
        {
            HttpPostUserData.HttpUserPost(Config.UserPhone, Name, Dream, ImagePath,Tag);
            Config.UserLocalImage = ImagePath;
        }
    }
}
