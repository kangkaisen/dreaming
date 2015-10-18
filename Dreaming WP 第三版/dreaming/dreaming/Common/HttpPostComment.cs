using DataHelp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public  class HttpPostComment
    {
        public static async void HttpPost(string id,string phone,string image, string name, string content, string time,string atName,string atPhone,string atImage)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
                string posturi = Config.apiCommentPublish;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(posturi));
                HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("id",id),
                        new KeyValuePair<string,string>("phone",phone),
                        new KeyValuePair<string, string>("image", image),
                        new KeyValuePair<string, string>("name", name),
                        new KeyValuePair<string, string>("content", content),
                        new KeyValuePair<string, string>("time", time),
                        new KeyValuePair<string, string>("atName", atName),
                        new KeyValuePair<string, string>("atPhone", atPhone),
                        new KeyValuePair<string, string>("atImage", atImage),
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
