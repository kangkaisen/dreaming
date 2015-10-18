using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace dreaming.ViewModels
{
   public class UserCommentViewModel:ModelBase
    {
        private ObservableCollection<UserModel> userList;

        public ObservableCollection<UserModel> UserList
        {
            get { return userList; }
            set
            { this.SetProperty(ref this.userList, value); }
        }

        public UserCommentViewModel()
        {
            UserList = new ObservableCollection<UserModel>();
            Load();
        }

        async void Load()
        {

            string uri = Config.apiCommentUserGet+ Config.UserPhone;
            string response = await HttpGet.HttpGets(uri);
            if (response != null)
            {
                try
                {
                    JsonArray jsonList = JsonArray.Parse(response);
                    foreach (var item in jsonList)
                    {
                        JsonObject user = item.GetObject();
                        UserModel userModel = new UserModel();
                        userModel.uid = user.GetNamedString("user_phone");
                        userModel.name = user.GetNamedString("user_name");
                        userModel.image = user.GetNamedString("user_image");
                        userModel.day = user.GetNamedString("post_id"); //文章ID
                
                        UserList.Add(userModel);
                    }
                }
                catch (Exception ex)
                {
                    HelpMethods.Msg(ex.Message.ToString());
                }
            }
        }
    }
}
