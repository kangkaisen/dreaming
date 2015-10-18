using DataHelp;
using DataHelp.Common;
using DataHelp.Help;
using DataHelp.Models;
using dreaming.Cache;
using dreaming.ControlHelp;
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
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;



namespace dreaming.Views
{
   
    public sealed partial class Chat : Page
    {
        public Chat()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        
         }

      

        
        ObservableCollection<MessageModel> messageList;
        MediaCapture _mediaCaptureManager = null;
       
        private StorageFile _recordStorageFile;
        UserModel User;
       
        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter != null)
            {
                User = e.Parameter as UserModel;
                textName.Text = User.name;
            }
            Init();
            SocKetHelp.socket.Off("chat");
            SocKetHelp.socket.Emit("get", User.uid + "|" + Config.UserPhone);

            SocKetHelp.socket.On("new", (data) =>
            {
                string str = data.ToString();
             
                ObservableCollection<MessageModel> newMessageList = JsonConvert.DeserializeObject<ObservableCollection<MessageModel>>(str);
                ShowList(newMessageList);

            });
               
           
            SocKetHelp.socket.On("chat", (data) =>
            {
                string str = data.ToString();
                MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(str);
                if (messageModel.myPhone == User.uid)
                {
                    ShowOne(messageModel);
                }
                else
                {
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
                    MainViewModel main = new MainViewModel();
                }


            });


            DbService.Current.InsertOrUpdateTrue(User);
        }


        async void ShowList(ObservableCollection<MessageModel> newMessageList)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach (var item in newMessageList)
                {
                    messageList.Add(item);
                }
                if (myChat.Items.Count > 0)
                {
                    myChat.ScrollIntoView(myChat.Items.Last(), ScrollIntoViewAlignment.Leading);
                }
            });
            
            DbService.Current.AddList(newMessageList);
            
        }
        async void ShowOne(MessageModel msg)
        {
           
            
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                messageList.Add(msg);
                
                if (myChat.Items.Count > 0)
                {
                    myChat.ScrollIntoView(myChat.Items.Last(), ScrollIntoViewAlignment.Leading);
                }
            });
            DbService.Current.Add(msg);
        }

        async void Init()
        {
            List<MessageModel> msgList = await DbService.Current.FindMsg(User.uid);
            if (msgList != null)
            {
                messageList = new ObservableCollection<MessageModel>(msgList);
            }
            else
            {
                messageList = new ObservableCollection<MessageModel>();
            }

             myChat.ItemsSource = messageList;
        }
      
    
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
             songMedia.Source = null;
      
             base.OnNavigatedFrom(e);
        }
       
        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter&&chatText.Text!=null)
            {
                MessageModel message = new MessageModel();
                message.myImage = Config.UserImage;
                message.myPhone = Config.UserPhone;
                message.myName = Config.UserName;
                message.myDream = Config.UserDream;
                message.type = 0;
                message.msg = chatText.Text;
                message.toPhone = User.uid;
                message.toImage = User.image;
                message.time = HelpMethods.GetTimeNow();
                messageList.Add(message);
                if (myChat.Items.Count > 0)
                {
                    myChat.ScrollIntoView(myChat.Items.Last(), ScrollIntoViewAlignment.Leading);
                }
                DbService.Current.Add(message);
                string messageJSON = JsonConvert.SerializeObject(message);
                SocKetHelp.socket.Emit("chat", messageJSON);
                chatText.Text = string.Empty;
                this.Focus(FocusState.Keyboard);
                
            }
        }
        //录音初始化
        private async void record_Tapped(object sender, TappedRoutedEventArgs e)
        {

            try
            {
                _mediaCaptureManager = new MediaCapture();
                var settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;//设置为音频

                await _mediaCaptureManager.InitializeAsync(settings);


                gridRecord.Visibility = Visibility.Visible;
                myChat.Visibility = Visibility.Collapsed;

                record.IsTapEnabled = false;
                storyboard.Begin();


                String fileName = "1.aac";
                _recordStorageFile = await KnownFolders.MusicLibrary.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                MediaEncodingProfile recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);//录音
                await _mediaCaptureManager.StartRecordToStorageFileAsync(recordProfile, this._recordStorageFile);//将录音保存到视频库





            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }
           
        //选取照片
        private void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            Config.pictrueType = 2;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.ContinuationData["Operation"] = "ImageChat";
            openPicker.PickSingleFileAndContinue();
        }

        private FileOpenPickerContinuationEventArgs _filePickerEventArgs = null;
        public FileOpenPickerContinuationEventArgs FilePickerEvent
        {
            get { return _filePickerEventArgs; }
            set
            {
                _filePickerEventArgs = value;
                ContinueFileOpenPicker(_filePickerEventArgs);
            }
        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {


            if ((args.ContinuationData["Operation"] as string) == "ImageChat" && args.Files != null && args.Files.Count > 0)
            {
               
                StorageFile inFile = args.Files[0];
              
              
              

                MessageModel message1 = new MessageModel();
                string imagePath = await HttpPostSignleImage.HttpPost(inFile);
                message1.type = 2;
                message1.msg = imagePath;
                message1.toPhone = User.uid;
                message1.myImage = Config.UserImage;
                message1.myPhone= Config.UserPhone;
                message1.myName = Config.UserName;
                message1.myDream = Config.UserDream;
                message1.toImage = User.image;
                message1.time = HelpMethods.GetTimeNow();
                messageList.Add(message1);

                if (myChat.Items.Count > 0)
                {
                    myChat.ScrollIntoView(myChat.Items.Last(), ScrollIntoViewAlignment.Leading);
                }
                 
                string messageJSON = JsonConvert.SerializeObject(message1);
                SocKetHelp.socket.Emit("chat", messageJSON);
                DbService.Current.Add(message1);
                
                
            }
        }
        //放弃录音
        private void cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_mediaCaptureManager != null)
            {
                _mediaCaptureManager.Dispose();
                _mediaCaptureManager = null;
            }

              gridRecord.Visibility = Visibility.Collapsed;
              record.IsTapEnabled = true;
              myChat.Visibility = Visibility.Visible;
              stop.Visibility = Visibility.Visible;
              cancel.Visibility = Visibility.Collapsed;
              accept.Visibility = Visibility.Collapsed;
        }
        //暂停录音
        private async void stop_Tapped(object sender, TappedRoutedEventArgs e)
        {
              await _mediaCaptureManager.StopRecordAsync();//停止录音
              storyboard.Stop();
              stop.Visibility = Visibility.Collapsed;
              cancel.Visibility = Visibility.Visible;
              accept.Visibility = Visibility.Visible;
        }
        //发送录音
        private async void accept_Tapped(object sender, TappedRoutedEventArgs e)
        {
           

             
             HttpClient httpClient = new HttpClient();
             IRandomAccessStreamWithContentType stream1 = await _recordStorageFile.OpenReadAsync();
             HttpStreamContent streamContent1 = new HttpStreamContent(stream1);
             HttpMultipartFormDataContent fileContent = new HttpMultipartFormDataContent();
             fileContent.Add(streamContent1, "song", _recordStorageFile.Name);
             string uri = Config.apiChatRecord;
             HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), fileContent);
             string msgRecord= await response.Content.ReadAsStringAsync();


             MessageModel message = new MessageModel();
             message.myImage = Config.UserImage;
             message.myPhone = Config.UserPhone;
             message.myName = Config.UserName;
             message.myDream = Config.UserDream;
             message.type = 1;
             message.msg = msgRecord;
             message.toPhone = User.uid;
             message.toImage = User.image;
             message.time = HelpMethods.GetTimeNow();
            
             
             messageList.Add(message);
             if (myChat.Items.Count > 0)
             {
                 myChat.ScrollIntoView(myChat.Items.Last(), ScrollIntoViewAlignment.Leading);
             }
             string messageJSON = JsonConvert.SerializeObject(message);
             SocKetHelp.socket.Emit("chat", messageJSON);

             DbService.Current.Add(message);
             gridRecord.Visibility = Visibility.Collapsed;
             record.IsTapEnabled = true;
             myChat.Visibility = Visibility.Visible;
             stop.Visibility = Visibility.Visible;
             cancel.Visibility = Visibility.Collapsed;
             accept.Visibility = Visibility.Collapsed;

             if (_mediaCaptureManager != null)
             {
                 _mediaCaptureManager.Dispose();
                 _mediaCaptureManager = null;
             }
        }

       

        //播放录音
        private  void palySong(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                string song = btn.Tag.ToString();
                songMedia.Source = new Uri(Config.apiFile + song);
                songMedia.Play();
            }
        }




     
    }
}
