using DataHelp.Common;
using DataHelp.Models;
using dreaming.Command;
using dreaming.ControlHelp;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace dreaming.ViewModels
{
    class UserDreamingViewModel:ModelBase
    {
        private List<DreamingModel> dreamingList;

        public List<DreamingModel> DreamingList
        {
            get { return dreamingList; }
            set
            { this.SetProperty(ref this.dreamingList, value); }
        }

        private string userimage;

        public string UserImage
        {
            get { return userimage; }
            set
            { this.SetProperty(ref this.userimage, value); }
        }

        private string userday;

        public string UserDay
        {
            get { return userday; }
            set
            { this.SetProperty(ref this.userday, value); }
        }

        private string username;

        public string UserName
        {
            get { return username; }
            set
            { this.SetProperty(ref this.username, value); }
        }

        private string userdream;

        public string UserDream
        {
            get { return userdream; }
            set
            { this.SetProperty(ref this.userdream, value); }
        }
        public string userId { get; set; }
        public UserModel User { get; set; }
        public DelegateCommand FollowerCommand { get; set; }
      
        public UserDreamingViewModel(string id)
        {
            Load(id);
            FollowerCommand = new DelegateCommand(Follower);
            
        }

        public async void Load(string id)
        {

            string uri = Config.apiUserInfo+ id;
            string response = await HttpGet.HttpGets(uri);
            try
            {
                JsonObject user = JsonObject.Parse(response);
                userId = user.GetNamedString("phone");
                UserImage = Config.apiFile+ user.GetNamedString("image");
                UserName = user.GetNamedString("name");
                UserDay = user.GetNamedString("date");
                UserDream = user.GetNamedString("dream");
                User = new UserModel { uid = userId, name = UserName, image = UserImage, dream = UserDream, day = UserDay };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }

            string dreamingUri = Config.apiDreamingUserId + id;
            string dreamingResponse = await HttpGet.HttpGets(dreamingUri);
            try
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(List<DreamingModel>));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(dreamingResponse));
                List<DreamingModel> dreaming = ds.ReadObject(ms) as List<DreamingModel>;

          
                DreamingList = dreaming;




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }


        void Follower()
        {
            HttpPostUserFollower.HttpPost(userId, UserName, UserImage, UserDream, Config.UserPhone, Config.UserName, Config.UserImage, Config.UserDream);
            FollowerCommand.CanExecutes = false;
        }

        

    }
}
