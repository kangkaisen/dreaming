using DataHelp.Common;
using dreaming.ControlHelp;
using dreaming.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace dreaming.Common
{
   public class HttpGetNoCache
    {
        private static string responseString = null;

        public static async Task<string> HttpGet(string uri)
        {


            if (Config.IsNetWork)
            {
                NotifyControl notify = new NotifyControl();
                notify.Text = "亲,努力加载中...";
                notify.Show();
             
                var _filter = new HttpBaseProtocolFilter();
                using (HttpClient httpClient = new HttpClient(_filter))
                {

                    _filter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await httpClient.GetAsync(new Uri(uri, UriKind.Absolute));
                    responseString = await response.Content.ReadAsStringAsync();
                    notify.Hide();

                }

            }
            return responseString;



        }
    }
}
