using DataHelp.Models;
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
    public sealed partial class UserPraise : Page
    {
        public UserPraise()
        {
            this.InitializeComponent();
            this.DataContext = new UserPraiseViewModel();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

  
    
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image image = sender as Image;
            string id = image.Tag.ToString();
            Frame.Navigate(typeof(UserDreaming), id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            UserModel userModel = btn.DataContext as UserModel;
            Frame.Navigate(typeof(DreamingComment), userModel);
        }
    }
}
