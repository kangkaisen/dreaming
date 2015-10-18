using DataHelp;
using DataHelp.Common;
using DataHelp.Help;
using DataHelp.Models;
using dreaming.Cache;
using dreaming.Command;
using dreaming.ControlHelp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace dreaming.ViewModels
{
    public class MainViewModel : ModelBase
    {
        private ObservableCollection<UserModel> msgList;
        public ObservableCollection<UserModel> MsgList
        {
            get { return msgList; }
            set
            { this.SetProperty(ref this.msgList, value); }
        }

        
   
        private string userImage;

        public string UserImage
        {
            get { return userImage; }
            set
            { this.SetProperty(ref this.userImage, value); }
        }
        private string userName;

        public string UserName 
        {
            get { return userName; }
            set
            { this.SetProperty(ref this.userName, value); }
        }

        private string userDream;

        public string UserDream
        {
            get { return userDream; }
            set
            { this.SetProperty(ref this.userDream, value); }
        }

        private int userCare;

        public int UserCare
        {
            get { return userCare; }
            set
            { this.SetProperty(ref this.userCare, value); }
        }

        private int userFollow;

        public int UserFollow
        {
            get { return userFollow; }
            set
            { this.SetProperty(ref this.userFollow, value); }
        }
        private int userPost;

        public int UserPost
        {
            get { return userPost; }
            set
            { this.SetProperty(ref this.userPost, value); }
        }
        
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public MainViewModel()
        {
            UserInit();
            Init();
            DeleteCommand = new DelegateCommand(Delete);
            RefreshCommand = new DelegateCommand(Refresh);
         
        }


        async void Init()
        {
            var List= await DbService.Current.FindUser();
            MsgList = new ObservableCollection<UserModel>(List);

        }

        async void UserInit()
        {
            if (Config.IsNetWork)
            {
                string uri = Config.apiUserInfo + Config.UserPhone;
                string response = await HttpGet.HttpGets(uri);
                if (response != null)
                {
                    try
                    {
                        JsonObject user = JsonObject.Parse(response);
                        UserName = user.GetNamedString("name");
                        UserImage = Config.apiFile + user.GetNamedString("image");
                        UserDream = user.GetNamedString("dream");

                        UserCare = (int)user.GetNamedNumber("follow_count");
                        UserFollow = (int)user.GetNamedNumber("fans_count");
                        UserPost = (int)user.GetNamedNumber("post_count");
                        Config.UserTag = user.GetNamedString("tag");
                        Config.UserImage = Config.apiFile + user.GetNamedString("image");
                        Config.UserName = UserName;
                        Config.UserDream = UserDream;
                        Config.UserCare = UserCare;
                        Config.UserFollow = UserFollow;
                        Config.UserPost = UserPost;
                    }
                    catch(Exception ex)
                    {
                        HelpMethods.Msg(ex.Message.ToString());
                    }
                 
                }
            }
            else
            {
                UserName = Config.UserName;
                UserImage = Config.UserLocalImage;
                UserDream = Config.UserDream;
                UserCare = Config.UserCare;
                UserFollow = Config.UserFollow;
                UserPost = Config.UserPost;
            }
      
           
        }

        private void Delete(Object paramater)
        {
            string id = paramater as string;
            if (id != null)
            {
                UserModel user = MsgList.FirstOrDefault(u => u.uid == id);
                MsgList.Remove(user);
                DbService.Current.DeleteUser(user);
                DbService.Current.DeleteMsg(id);
            }
            
       }

        void Refresh()
        {
            Init();
        }
    }
}
