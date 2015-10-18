using DataHelp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public  class HttpPostUserFollower
    {
       public static async void HttpPost(string cid, string cname, string cimage, string cdream, string fid, string fname,string fimage,string fdream)
       {
           try
           {

               HttpClient httpClient = new HttpClient();
               HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(Config.apiUserFollow));
               HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                   new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("cphone", cid),
                        new KeyValuePair<string, string>("cname", cname),
                        new KeyValuePair<string, string>("cimage", cimage),
                        new KeyValuePair<string, string>("cdream", cdream),
                        new KeyValuePair<string, string>("fphone", fid),
                        new KeyValuePair<string, string>("fname", fname),
                        new KeyValuePair<string, string>("fimage", fimage),
                        new KeyValuePair<string, string>("fdream", fdream),
                       
                       
                        
                    }
               );
               request.Content = postData;
               HttpResponseMessage response = await httpClient.SendRequestAsync(request);
               string responseString = await response.Content.ReadAsStringAsync();
              


           }
           catch (Exception ex)
           {
               HelpMethods.Msg(ex.Message.ToString());
           }
       }
    }
}
