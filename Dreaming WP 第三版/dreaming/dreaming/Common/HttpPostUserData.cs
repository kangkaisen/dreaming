using DataHelp.Common;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
    public class HttpPostUserData
    {
        public static async void HttpUserPost(string phone, string name, string content, string picPath,string tag)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                string posturi = Config.apiUserUpdate;
                StorageFile file1 = await StorageFile.GetFileFromPathAsync(picPath);
                IRandomAccessStreamWithContentType stream1 = await file1.OpenReadAsync();
                HttpStreamContent streamContent1 = new HttpStreamContent(stream1);
                HttpStringContent stringContent = new HttpStringContent(content);
                HttpStringContent stringTitle = new HttpStringContent(phone);
                HttpStringContent stringName = new HttpStringContent(name);
                HttpStringContent stringTag = new HttpStringContent(tag);

                HttpMultipartFormDataContent fileContent = new HttpMultipartFormDataContent();
                fileContent.Add(stringContent, "dream");
                fileContent.Add(stringTitle, "phone");
                fileContent.Add(stringTag, "tag");
                fileContent.Add(stringName, "name");
                fileContent.Add(streamContent1, "picture", "pic.jpg");
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(posturi), fileContent);
                string responString = await response.Content.ReadAsStringAsync();

              

                if ((int)response.StatusCode == 200)
                {
                    JsonObject user = JsonObject.Parse(responString);
                    
                   
                    Config.UserName= user.GetNamedString("name");
                    Config.UserImage = Config.apiFile+ user.GetNamedString("image");
                    Config.UserPhone= user.GetNamedString("phone");
                    Config.UserDream = user.GetNamedString("dream");
                    Config.UserTag = user.GetNamedString("tag");

                    NavigationHelp.NavigateTo(typeof(Main));

                }

            }
            catch (Exception ex)
            {
               HelpMethods.Msg(ex.Message.ToString());
            }

        }
    }
}
