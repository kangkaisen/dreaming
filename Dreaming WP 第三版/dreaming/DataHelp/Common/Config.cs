using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataHelp.Common
{
    public static class Config
    {
        //图片选择器
        public static int pictrueType = 1;//0 :用户头像　１：Ｄreaming 2:聊天
        //API地址:
        #region

        public const string apiBase = "http://121.41.54.199/";
        //public const string apiBase = "http://172.23.164.1:8088/";
        public const string apiUserRegister = apiBase + "api/user/signin";
        public const string apiUserUpdate = apiBase + "api/user/update";
        public const string apiUserLogin = apiBase + "api/user/login";
        public const string apiUserInfo = apiBase + "api/user/";
        public const string apiUserUrlUpdate = apiBase + "api/user/url";
        public const string apiUserFollow = apiBase + "api/user/follow";
        public const string apiUserUnFollow = apiBase + "api/user/unfollow/";
        public const string apiUserCarers = apiBase + "api/user/carers/";
        public const string apiUserFollowers = apiBase + "api/user/followers/";
        public const string apiUserFind = apiBase + "api/user/find/";
        public const string apiDreamingPublish = apiBase + "api/dreaming/publish";
        public const string apiDreamingDelete = apiBase + "api/dreaming/delete";
        public const string apiDreaming = apiBase + "api/dreaming/";
        public const string apiDreamingOne = apiBase + "api/dreaming/post/";
        public const string apiDreamingUserId = apiBase + "api/dreaming/user/";
        public const string apiDreamingId = apiBase + "api/dreaming/id";
        public const string apiDreamingPraise = apiBase + "api/dreaming/praise";
        public const string apiCommentPublish = apiBase + "api/comment/publish";
        public const string apiCommentGet = apiBase + "api/comment/";
        public const string apiCommentUserGet = apiBase + "api/comment/user/";
        public const string apiCommentOneGet = apiBase + "api/comment/one/";
        public const string apiPraiseGet = apiBase + "api/praise/";
        public const string apiChatRecord = apiBase + "api/chat/record";
        public const string apiChatImage = apiBase + "api/chat/image";
        public const string apiFile = apiBase + "files/";
        #endregion
        //APP唯一元素

        //是否联网
        public static bool IsNetWork
        {
            get
            {
                return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public static ApplicationDataContainer appSettings = ApplicationData.Current.LocalSettings;
        //用户信息
        #region
        //用户ID
        public static string UserPhone
        {
            get
            {
                return appSettings.Values.ContainsKey("UserPhone") ? appSettings.Values["UserPhone"] as string : null;
            }
            set
            {
                appSettings.Values["UserPhone"] = value;
            }
        }

        //用户昵称

        public static string UserName
        {
            get
            {
                return appSettings.Values.ContainsKey("UserName") ? appSettings.Values["UserName"] as string : null;
            }
            set
            {
                appSettings.Values["UserName"] = value;
            }
        }
        //用户网络图片
        public static string UserImage
        {
            get
            {
                return appSettings.Values.ContainsKey("UserImage") ? appSettings.Values["UserImage"] as string : null;
            }
            set
            {
                appSettings.Values["UserImage"] = value;
            }
        }

        public static string UserLocalImage
        {
            get
            {
                return appSettings.Values.ContainsKey("UserLocalImage") ? appSettings.Values["UserLocalImage"] as string : "ms-appx:///Assets/1.png";
            }
            set
            {
                appSettings.Values["UserLocalImage"] = value;
            }
        }

        //用户梦想
        public static string UserDream
        {
            get
            {
                return appSettings.Values.ContainsKey("UserDream") ? appSettings.Values["UserDream"] as string : null;
            }
            set
            {
                appSettings.Values["UserDream"] = value;
            }
        }
        public static string UserTag
        {
            get
            {
                return appSettings.Values.ContainsKey("UserTag") ? appSettings.Values["UserTag"] as string : null;
            }
            set
            {
                appSettings.Values["UserTag"] = value;
            }
        }
        public static int UserCare
        {
            get
            {
                return appSettings.Values.ContainsKey("UserCare") ? (int)appSettings.Values["UserCare"] : 0;
            }
            set
            {
                appSettings.Values["UserCare"] = value;
            }
        }
        public static int UserFollow
        {
            get
            {
                return appSettings.Values.ContainsKey("UserFollow") ? (int)appSettings.Values["UserFollow"] : 0;
            }
            set
            {
                appSettings.Values["UserFollow"] = value;
            }
        }

        public static int UserPost
        {
            get
            {
                return appSettings.Values.ContainsKey("UserPost") ? (int)appSettings.Values["UserPost"] : 0;
            }
            set
            {
                appSettings.Values["UserPost"] = value;
            }
        }

        public static bool IsNotify
        {
            get
            {
                return appSettings.Values.ContainsKey("IsNotify") ? (bool)appSettings.Values["IsNotify"] :true;
            }
            set
            {
                appSettings.Values["IsNotify"] = value;
            }
        }
        #endregion


        //固定字符串
        //缓存文件名称
        public const string CacheFileName = "dreaming.json";










    }
}
