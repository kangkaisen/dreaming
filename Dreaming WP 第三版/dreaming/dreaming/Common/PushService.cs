using DataHelp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public  class PushService
    {
       public static async void CreateChannel()
       {
           if (Config.IsNetWork)
           {
               try
               {
                   PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                   if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("url") == false)
                   {
                       Windows.Storage.ApplicationData.Current.LocalSettings.Values["url"] = channel.Uri;
                       SendURL(channel.Uri);
                   }
                   else
                   {
                       string savedUrl = Windows.Storage.ApplicationData.Current.LocalSettings.Values["url"] as string;
                       // 当URL改变了，就重新发给服务器
                       if (savedUrl != channel.Uri)
                       {
                           // 再次保存本地设置
                           Windows.Storage.ApplicationData.Current.LocalSettings.Values["url"] = channel.Uri;
                           SendURL(channel.Uri);
                       }
                   }
               }
               catch(Exception ex)
               {
                   Debug.WriteLine(ex.Message.ToString());
               }
          
             }
          
         
       }

       private static async void SendURL(string url)
       {
           using (HttpClient httpClient=new HttpClient ())
           {
               string uri = Config.apiUserUrlUpdate;
               HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
               HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                   new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("url",url),
                        new KeyValuePair<string, string>("phone",Config.UserPhone),
                       
                    }
               );
               request.Content = postData;
               HttpResponseMessage response = await httpClient.SendRequestAsync(request);
           }
       }

   

       
    }
}
