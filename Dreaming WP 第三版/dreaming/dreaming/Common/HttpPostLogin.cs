using DataHelp.Common;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
    public class HttpPostLogin
    {
        public static async void HttpPost(string phone, string password)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string posturi = Config.apiUserLogin;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(posturi));
                HttpFormUrlEncodedContent postData = new HttpFormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("phone", phone),//手机
                        new KeyValuePair<string, string>("password", password),//密码
                    }
                );
                request.Content = postData;
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                string responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);

                JsonObject login = JsonObject.Parse(responseString);
                try
                {
                    int code = (int)login.GetNamedNumber("code");
                    switch (code)
                    {
                        case 0:
                            {
                                JsonObject user = login.GetNamedObject("user");
                                Config.UserName = user.GetNamedString("name") ;
                                Config.UserImage = Config.apiFile + user.GetNamedString("image");
                                Config.UserPhone = user.GetNamedString("phone") ;
                                Config.UserDream = user.GetNamedString("dream") ;
                                Config.UserTag = user.GetNamedString("tag");
                                NavigationHelp.NavigateTo(typeof(Main));
                                break;
                            }
                        case 1:
                            HelpMethods.Msg("手机号未注册!");
                            break;
                        case 2:
                            HelpMethods.Msg("密码错误!");
                            break;
                        default:
                            break;

                    }
                }
                catch (Exception ex)
                {
                    HelpMethods.Msg("服务器出错!");
                    Debug.WriteLine(ex.Message.ToString());
                }


            }
            catch (Exception ex)
            {
               HelpMethods.Msg(ex.Message.ToString());
            }
        }
     
    }
}
