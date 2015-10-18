using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
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
    
    public sealed partial class DreamingComment : Page
    {
        public DreamingComment()
        {
            this.InitializeComponent();
           
        }

        DreamingModel dreaming;
        ObservableCollection<CommentModel> commentsList = new ObservableCollection<CommentModel>();
        string atName ="";
        string atPhone ="";
        string atImage = "";
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter!= null && e.Parameter is DreamingModel)
            {
                dreaming = (DreamingModel)e.Parameter;
                postGrid.DataContext = dreaming;
                LoadComment(dreaming._id);
            }
            else if (e.Parameter != null && e.Parameter is UserModel)
            {
                UserModel user = e.Parameter as UserModel;
                LoadPost(user.day);
                LoadComment(user.day);
            }
           
           
        }

        async void LoadPost(string id)
        {
            string response = await HttpGet.HttpGets(Config.apiDreamingOne + id);
            if (response != null)
            {
                try
                {
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(DreamingModel));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                    dreaming = ds.ReadObject(ms) as DreamingModel;
                    postGrid.DataContext = dreaming;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message.ToString());
                }
            }
        }
       async void LoadComment(string id)
        {
            
                string response = await HttpGet.HttpGets(Config.apiCommentGet+id);
             
                if (response!= null)
                {
                    try
                    {
                        DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(ObservableCollection<CommentModel>));
                        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                        commentsList = ds.ReadObject(ms) as ObservableCollection<CommentModel>;
                       
                     }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message.ToString());
                    }
                }
                this.listview.ItemsSource = commentsList;
        }
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

        private void comment_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                string date = HelpMethods.GetTimeNow();
                HttpPostComment.HttpPost(dreaming._id, Config.UserPhone, Config.UserImage, Config.UserName, comment.Text,date,atName,atPhone,atImage);
                commentsList.Add(new CommentModel{ post_id=dreaming._id, user_name= Config.UserName, content = comment.Text, user_image = Config.UserImage, time = date ,user_phone=Config.UserPhone,at_name=atName,at_phone=atPhone,at_image=atImage});
                this.Focus(FocusState.Pointer);
                comment.Text = "";
                atImage = "";
                atName = "";
                atPhone = "";
                comment.PlaceholderText = "请输入您的评论";
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView imageList = (ListView)sender;
            DreamingModel dreaming = (imageList.DataContext) as DreamingModel;
            Frame.Navigate(typeof(ImageView), dreaming);

        }

        private void commentAtText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock cb = sender as TextBlock;
            CommentModel commentModel = cb.DataContext as CommentModel;
            atName = commentModel.user_name;
            atPhone = commentModel.user_phone;
            atImage = commentModel.user_image;
            comment.PlaceholderText = "回复@" + atName+":";
            comment.Focus(FocusState.Pointer);

        }
    }
}
