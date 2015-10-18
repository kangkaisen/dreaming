using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace dreaming.ViewModels
{
    public class UserFollowerViewModel:ModelBase
    {
        private List<FollowerModel> list;

        public List<FollowerModel> List
        {
            get { return list; }
            set
            { this.SetProperty(ref this.list, value); }
        }

        public UserFollowerViewModel(string id)
        {
            Load(id);
        }


        async void Load(string id)
        {

            string uri = Config.apiUserFollowers + id;
            string response = await HttpGet.HttpGets(uri);
            try
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(List<FollowerModel>));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                List<FollowerModel> list = ds.ReadObject(ms) as List<FollowerModel>;
                List = list;




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }
    }
}
