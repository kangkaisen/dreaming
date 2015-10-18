using DataHelp;
using DataHelp.Common;
using DataHelp.Help;
using DataHelp.Models;
using dreaming.Cache;
using dreaming.ControlHelp;
using dreaming.Controls;
using dreaming.ViewModels;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class Main : Page
    {
        public Main()
        {
            this.InitializeComponent();
            HelpMethods.HideStatusBar();
            PushService.CreateChannel();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            SocketInit();
        }


        void SocketInit()
        {
            SocKetHelp.SocketInit();
            SocKetHelp.socket.On("chat", (data) =>
            {
                string str = data.ToString();

                MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(str);
                DbService.Current.Add(messageModel);
                UserModel chatUser = new UserModel
                {
                    uid = messageModel.myPhone,
                    name = messageModel.myName,
                    image = messageModel.myImage,
                    dream = messageModel.myDream,
                    isRead = false
                };
                DbService.Current.InsertOrUpdateFalse(chatUser);
                switch (messageModel.type)
                {
                    case 0:
                        ToastNotify.Notify("私信  " + messageModel.myName + ":" + messageModel.msg);
                        break;
                    case 1:
                        ToastNotify.Notify("私信  " + messageModel.myName + "向您发送一段了语音");
                        break;
                    case 2:
                        ToastNotify.Notify("私信  " + messageModel.myName + "向您发送了一张图片");
                        break;
                    default:
                        break;

                }

            });
        }
    
       
    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = new MainViewModel();
            Frame.BackStack.Clear();
            sbMain.Begin();
        
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

     
        DateTime _lastTimeClickBack = DateTime.MinValue;

        bool isSecondClick = false; //是否第二次点击
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var deltaTime = DateTime.Now - _lastTimeClickBack;
            if (!isSecondClick)
            {
                _lastTimeClickBack = DateTime.Now;

       

                isSecondClick = true;

                this.bd_QuitInfo.Visibility = Windows.UI.Xaml.Visibility.Visible;
                

                ThreadPoolTimer.CreateTimer(async t =>
                {

                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {

                        this.bd_QuitInfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                        isSecondClick = false;





                    });

                }, new TimeSpan(0, 0, 3));

                e.Handled = true;

            }

            else
            {

                
                if (deltaTime.TotalSeconds < 5)
                {
                    SocKetHelp.socket.Disconnect();
                    Application.Current.Exit();

                }

                e.Handled = true;

            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
      
    

      
        //进入聊天页
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            UserModel user = e.ClickedItem as UserModel;

            Frame.Navigate(typeof(Chat), user);
        }
        //关于
        private void about_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
        //反馈
        private async  void reply_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Email.EmailMessage mail = new Windows.ApplicationModel.Email.EmailMessage();
            mail.Subject = "Dreaming 反馈";
            mail.To.Add(new Windows.ApplicationModel.Email.EmailRecipient("kangkaisen@live.com"));
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(mail);
        }
        //设置
        private void setting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
        //好评
        private async void praise_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(
               new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }
        //关注
        private void care_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserCare), Config.UserPhone);
        }
        //粉丝
        private void follow_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserFollower), Config.UserPhone);
        }
        //文章
        private void post_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserDreaming), Config.UserPhone);
        }

        private void dreaming_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllDreaming));
        }

      


        //附加 消息删除
        private void msgGrid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

     

        private void userImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserData));
        }

        private void mainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem pivotItem = this.mainPivot.SelectedItem as PivotItem;
            if (pivotItem.Tag != null)
            {
                string tag = pivotItem.Tag.ToString();
                if (tag == "message")
                {
                    commandBar.Visibility = Visibility.Visible;
                }
               
            }
            else
            {
                commandBar.Visibility = Visibility.Collapsed;
            }
        }

        private void zhiyin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserFind));
        }

        private void praiseButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPraise));
        }

        private void commentBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserComment));
        }

        private void task_Click(object sender, RoutedEventArgs e)
        {
            HelpMethods.Msg("该功能马上推出!");
        }

        private void taskList_Click(object sender, RoutedEventArgs e)
        {
            HelpMethods.Msg("该功能马上推出!");
        }


    }
}
