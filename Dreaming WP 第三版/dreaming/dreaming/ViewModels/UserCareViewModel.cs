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
using Windows.Data.Json;

namespace dreaming.ViewModels
{
    class UserCareViewModel:ModelBase
    {
        private List<FollowerModel> list;

        public List<FollowerModel> List
        {
            get { return list; }
            set
            { this.SetProperty(ref this.list, value); }
        }
        

        public UserCareViewModel(string id)
        {
            Load(id);
        }

       async void Load(string id)
        {

            string uri = Config.apiUserCarers + id;
            string response = await HttpGet.HttpGets(uri);
            if (response != null)
            {
                try
                {
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(List<FollowerModel>));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                    List = ds.ReadObject(ms) as List<FollowerModel>;





                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message.ToString());
                }
            }
            
        }
        
    }
}
