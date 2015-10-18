using DataHelp.Common;
using DataHelp.Models;
using dreaming.Common;
using dreaming.ControlHelp;
using dreaming.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace dreaming.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserDreaming : Page
    {
        public UserDreaming()
        {
            this.InitializeComponent();
          
        }

     
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
                string id = e.Parameter as string;
                this.DataContext = new UserDreamingViewModel(id);
            }
        }

        //点赞
        private void praise_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DreamingModel dreaming = (btn.DataContext) as DreamingModel;
            HtppPostPraise.HttpPost(dreaming._id,dreaming.user_phone,Config.UserPhone, Config.UserImage, Config.UserName);
            btn.Content = ((int)btn.Content + 1).ToString();
            btn.IsEnabled = false;
        }
        //评论
        private void comment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DreamingModel dreaming = (btn.DataContext) as DreamingModel;
            Frame.Navigate(typeof(DreamingComment), dreaming);
        }
        //播放语音
        private void song_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                string song = btn.Tag.ToString();
                songMedia.Source = new Uri(Config.apiFile + song);
                songMedia.Play();
            }
        }

        //打开图片
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView imageList = (ListView)sender;
            DreamingModel dreaming = (imageList.DataContext) as DreamingModel;
            Frame.Navigate(typeof(ImageView), dreaming);

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)

        {
            UserModel user=((UserDreamingViewModel)(this.DataContext)).User;
            Frame.Navigate(typeof(Chat), user);
        }

      
    }
}
