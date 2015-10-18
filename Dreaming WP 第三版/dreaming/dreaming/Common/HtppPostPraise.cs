using DataHelp.Common;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace dreaming.Common
{
   public class HtppPostPraise
    {
       public static async void HttpPost(string id,string touser, string phone, string image, string name)
       {
           try
           {

               HttpClient httpClient = new HttpClient();
               string posturi = Config.apiDreamingPraise;
               HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,new Uri(posturi));
               HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                   new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("id",id),
                        new KeyValuePair<string,string>("to",touser),
                        new KeyValuePair<string,string>("phone",phone),
                        new KeyValuePair<string, string>("image", image),
                        new KeyValuePair<string, string>("name", name),
                    }
               );
               request.Content = postData;
               HttpResponseMessage response = await httpClient.SendRequestAsync(request);
           }
           catch (Exception ex)
           {
               HelpMethods.Msg(ex.Message.ToString());
           }
       }
    }
}
