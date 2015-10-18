using DataHelp.Common;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public  class HttpPostRegister
    {
       public static async void HttpPost(string phone, string password)
       {
           try
           {

               HttpClient httpClient = new HttpClient();
               string posturi = Config.apiUserRegister;

               string date = DateTime.Now.Date.Year.ToString() + "-" + DateTime.Now.Date.Month.ToString() + "-" + DateTime.Now.Date.Day.ToString();
               HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(posturi));
               HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                   new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("phone", phone),//手机号
                        new KeyValuePair<string, string>("password", password),//密码
                        new KeyValuePair<string, string>("date", date),//注册日期
                    }
               );
               request.Content = postData;
               HttpResponseMessage response = await httpClient.SendRequestAsync(request);
               string responseString = await response.Content.ReadAsStringAsync();
             


               JsonObject register = JsonObject.Parse(responseString);
               try
               {
                   int code = (int)register.GetNamedNumber("code");
                   switch (code)
                   {
                       case 0:
                           {
                               JsonObject user = register.GetNamedObject("user");
                               Config.UserPhone = user.GetNamedString("phone");
                               NavigationHelp.NavigateTo(typeof(UserData));
                               break;
                           }
                       case 1:
                           HelpMethods.Msg("手机号已注册!");
                           break;
                       case 2:
                           HelpMethods.Msg("未知错误!");
                           break;
                       default:
                           break;

                   }
               }
               catch (Exception ex)
               {
                   HelpMethods.Msg(ex.Message.ToString());
               }
               

           }
           catch (Exception ex)
           {
               HelpMethods.Msg(ex.Message.ToString());
           }
       }
    }
}
