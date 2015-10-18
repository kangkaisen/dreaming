using DataHelp.Common;
using dreaming.Cache;
using dreaming.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public class HttpGet
    {
        private static string responseString=null;

        public static async Task<string> HttpGets(string uri)
        {


            if (Config.IsNetWork)
            {
                NotifyControl notify = new NotifyControl();
                notify.Text = "亲,努力加载中...";
              
                notify.Show();
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = new HttpResponseMessage();
                        response = await httpClient.GetAsync(new Uri(uri, UriKind.Absolute));
                        responseString = await response.Content.ReadAsStringAsync();
                        notify.Hide();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message.ToString());
                    }
                    
                  
                }
              
            }
            return responseString;
                
           

        }

       
    }
}
