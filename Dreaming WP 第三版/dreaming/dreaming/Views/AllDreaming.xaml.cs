using DataHelp.Common;
using DataHelp.Models;
using dreaming.Cache;
using dreaming.Common;
using dreaming.ControlHelp;
using dreaming.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
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

    public sealed partial class AllDreaming : Page
    {
      
         
      

       
        
        public AllDreaming()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            
        }

        
        ObservableCollection<DreamingModel> dream;
        ObservableCollection<DreamingModel> experience;
        ObservableCollection<DreamingModel> tucao;
        ObservableCollection<DreamingModel> ask;


       
        public ObservableCollection<DreamingModel> DreamList;
        public ObservableCollection<DreamingModel> ExperienceList;
        public ObservableCollection<DreamingModel> TucaoList;
        public ObservableCollection<DreamingModel> AskList;
        //数据加载的标识
        public bool IsLoading = false;
        int count = 10;
        //线程锁的对象
        private object o = new object();
        
    

    
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame.Navigate(typeof(Main));
            e.Handled = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            songMedia.Source = null;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            base.OnNavigatedFrom(e);
        }
        public async void Load( int id)
        {
           
            count = 10;
            string response = null;
            if (Config.IsNetWork)
            {
                string uri = Config.apiDreaming + id.ToString();
                response = await HttpGetNoCache.HttpGet(uri);
            }
            else
            {
                HelpMethods.Msg("亲,您无法连接网络!");
            }
            if (response != null)
                {
                    try
                    {
                        DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(ObservableCollection<DreamingModel>));
                        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                        switch(id)
                        {
                            case 0:
                                DreamList = new ObservableCollection<DreamingModel>();
                                dream = ds.ReadObject(ms) as ObservableCollection<DreamingModel>;
                                for (int i = 0; i < count && i < dream.Count; i++)
                                {
                                    DreamList.Add(dream[i]);
                                }
                                dreamList.ItemsSource = DreamList;
                                break;
                            case 1:
                                ExperienceList= new ObservableCollection<DreamingModel>();
                                experience = ds.ReadObject(ms) as ObservableCollection<DreamingModel>;
                                for (int i = 0; i < count && i < experience.Count; i++)
                                {
                                    ExperienceList.Add(experience[i]);
                                }
                                experienceList.ItemsSource = ExperienceList;
                                break;
                            case 2:
                                TucaoList = new ObservableCollection<DreamingModel>();
                                tucao = ds.ReadObject(ms) as ObservableCollection<DreamingModel>;
                                for (int i = 0; i < count && i < tucao.Count; i++)
                                {
                                    TucaoList.Add(tucao[i]);
                                }
                                tucaoList.ItemsSource = TucaoList;
                                break;
                            case 3:
                                AskList = new ObservableCollection<DreamingModel>();
                                ask = ds.ReadObject(ms) as ObservableCollection<DreamingModel>;
                                for (int i = 0; i < count && i < ask.Count; i++)
                                {
                                    AskList.Add(ask[i]);
                                }
                                askList.ItemsSource = AskList;
                                break;
                            default:
                                break;

                        }
                        
                      

                   

                     }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message.ToString());
                    }
                }
                
         
            

        }


       




        //点赞
        private void praise_Click(object sender,RoutedEventArgs e)
        {
            Button btn=(Button)sender;
            DreamingModel dreaming = (btn.DataContext) as DreamingModel;
            HtppPostPraise.HttpPost(dreaming._id,dreaming.user_phone, Config.UserPhone, Config.UserImage, Config.UserName);
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
                songMedia.Source = new Uri(Config.apiFile + btn.Tag.ToString());
                songMedia.Play();
            }
             
           
        }
        //获取指定用户dreaming
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image image = (Image)sender;
            string uid=image.Tag.ToString();
            Frame.Navigate(typeof(UserDreaming), uid);
        }

    
        //打开图片
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ListView imageList = (ListView)sender;
            
            DreamingModel dreaming = (imageList.DataContext) as DreamingModel;
            Frame.Navigate(typeof(ImageView), dreaming);
            
        }
        
        private void publish_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dreaming), DreamingPivot.SelectedIndex);
        }
       







       



   



        private void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            ListView listView=sender as ListView;
            lock (o)
            {
                if (!IsLoading)
                {
                    if (args.ItemIndex == listView.Items.Count - 1)
                    {
                        IsLoading = true;
                        int counts = listView.Items.Count;
                        switch (DreamingPivot.SelectedIndex)
                        {

                            case 0:

                                for (int i = counts; i <count+10 && i < dream.Count; i++)
                                {
                                    DreamList.Add(dream[i]);
                                }
                                
                                break;
                            case 1:

                                for (int i = counts; i < count + 10 && i < experience.Count; i++)
                                {
                                    ExperienceList.Add(experience[i]);
                                }
                                
                                break;
                            case 2:

                                for (int i = counts; i < count + 10 && i < tucao.Count; i++)
                                {
                                    TucaoList.Add(tucao[i]);
                                }
                              
                                break;
                            case 3:

                                for (int i = counts; i < count + 10 && i < ask.Count; i++)
                                {
                                    AskList.Add(ask[i]);
                                }
                             
                                break;
                            default:
                                break;
                        }
                        IsLoading = false;
                    }
                }
            }
        }

      
     


        private void DreamingPivot_PivotItemLoading(Pivot sender, PivotItemEventArgs args)
        {
            switch (args.Item.Name)
            {
                case "dreamPivot":
                    Load(0);
                    break;
                case "experiencePivot":
                    Load(1);
                    break;
                case "tucaoPivot":
                    Load(2);
                    break;
                case "askPivot":
                    Load(3);
                    break;
                default:
                    break;
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Load(DreamingPivot.SelectedIndex);
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            switch (DreamingPivot.SelectedIndex)
            {
                case 0:
                    dreamList.ScrollIntoView(dreamList.Items.FirstOrDefault());
                    break;
                case 1:
                    experienceList.ScrollIntoView(experienceList.Items.FirstOrDefault());
                    break;
                case 2:
                    tucaoList.ScrollIntoView(tucaoList.Items.FirstOrDefault());
                    break;
                case 3:
                    askList.ScrollIntoView(askList.Items.FirstOrDefault());
                    break;
                default:
                    break;
            }
        }


    }
}
