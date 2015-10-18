using DataHelp.Common;
using DataHelp.Models;
using dreaming.Command;
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
   public class UserFindViewModel:ModelBase
    {
       private ObservableCollection<UserModel> userList;

       public ObservableCollection<UserModel> UserList
        {
            get { return userList; }
            set
            { this.SetProperty(ref this.userList, value); }
        }

        private string word;

        public string Word
        {
            get { return word; }
            set
            { this.SetProperty(ref this.word, value); }
        }

        public DelegateCommand FindCommand { get; set; }
        public UserFindViewModel()
        {
            FindCommand = new DelegateCommand(Find);
            UserList = new ObservableCollection<UserModel>();
        }

       async  void Find()
        {
            string uri = Config.apiUserFind + Word;
            string response=await HttpGet.HttpGets(uri);
            if (response != null)
            {
                try
                {
                    JsonArray jsonList = JsonArray.Parse(response);
                    foreach (var item in jsonList)
                    {
                        JsonObject user = item.GetObject();
                        UserModel userModel = new UserModel();
                        userModel.uid = user.GetNamedString("phone");
                        userModel.name = user.GetNamedString("name");
                        userModel.image = Config.apiFile + user.GetNamedString("image");
                        userModel.dream = user.GetNamedString("dream");
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
