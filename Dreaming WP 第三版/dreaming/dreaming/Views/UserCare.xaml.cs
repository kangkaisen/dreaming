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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserCare : Page
    {
        public UserCare()
        {
            this.InitializeComponent();
            
        }

      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = e.Parameter as string;
            this.DataContext = new UserCareViewModel(id);
        }
        //取消关注
        private async void care_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FollowerModel follower = btn.DataContext as FollowerModel;
            string uri = Config.apiUserUnFollow + Config.UserPhone+ "/"+follower.cphone;
            string  response=await HttpGet.HttpGets(uri);
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image image = sender as Image;
            string id = image.Tag.ToString();
            Frame.Navigate(typeof(UserDreaming),id);
        }
    }
}
