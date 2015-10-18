using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using dreaming.ViewModels;
using System;
using System.Collections.Generic;
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
   
    public sealed partial class UserFollower : Page
    {
        public UserFollower()
        {
            this.InitializeComponent();
            
        }

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = e.Parameter as string;
            this.DataContext = new UserFollowerViewModel(id);
        }
        //关注
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FollowerModel follower = btn.DataContext as FollowerModel;
            HttpPostUserFollower.HttpPost(follower.fphone, follower.fname, follower.fimage, follower.fdream, Config.UserPhone, Config.UserName, Config.UserImage, Config.UserDream);
            btn.IsEnabled = false;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image image = sender as Image;
            string id = image.Tag.ToString();
            Frame.Navigate(typeof(UserDreaming), id);
        }

    }
}
