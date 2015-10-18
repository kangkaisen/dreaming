using dreaming.Command;
using dreaming.ControlHelp;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using System.IO;
using Windows.Foundation;
using DataHelp.Models;
using DataHelp.Common;
using DataHelp.Help;
using dreaming.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
namespace dreaming.ViewModels
{
    public class DreamingViewModel:ModelBase
    {
        private string dreaming;
        public string Dreaming
        {
            get { return dreaming; }
            set
            { this.SetProperty(ref this.dreaming, value); }
        }


        public string  Song { get; set; }

        public List<string> ImagePathList { get; set; }

    

        public DelegateCommand PostCommand { get; set; }

        public int type { get; set; }

        public DreamingViewModel(int i)
        {
            Init();
            type = i;
            PostCommand = new DelegateCommand(Post);
        }

        public void Post()
        {
            string date = HelpMethods.GetTimeNow();
            if (Dreaming != null && Dreaming.Length > 0)
            {
                HttpPost(Config.UserPhone, Config.UserImage, Config.UserName, Dreaming, ImagePathList, Song, date,type);
                PostCommand.CanExecutes = false;
            }
            else
            {
                ToastNotify.Notify("请您输入文字");
            }
        }



        public  async void HttpPost(string uid, string uimage, string name, string content, List<string> imagePsthList, string songPath,string date,int type)
        {
            NotifyControl notify = new NotifyControl();
            notify.Text = "亲,努力发送中...";
            notify.Show();
            HttpClient httpClient = new HttpClient();
            uint MaxImageWidth = 480;
            uint MaxImageHeight = 800;
            uint width;
            uint height;
            try
            {
                string posturi = Config.apiDreamingPublish;
                HttpMultipartFormDataContent fileContent = new HttpMultipartFormDataContent();
                if (imagePsthList.Count > 0)
                {
                    for (int i = 0; i < imagePsthList.Count; i++)
                    {
                        StorageFile inFile = await StorageFile.GetFileFromPathAsync(imagePsthList[i]);
                        var inStream = await inFile.OpenReadAsync();
                        InMemoryRandomAccessStream outStream = new InMemoryRandomAccessStream();
                        BitmapDecoder decoder = await ImageHelp.GetProperDecoder(inStream,inFile);
                        if (decoder == null) return;


                        if (decoder.OrientedPixelWidth > MaxImageWidth && decoder.OrientedPixelHeight > MaxImageHeight)
                        {
                            width = MaxImageWidth;
                            height = MaxImageHeight;
                        }
                        else
                        {
                            width = decoder.OrientedPixelWidth;
                            height = decoder.OrientedPixelHeight;
                        }

                        


                        PixelDataProvider provider = await decoder.GetPixelDataAsync();
                        byte[] data = provider.DetachPixelData(); //获取像素数据的字节数组
                               

                        BitmapPropertySet propertySet = new BitmapPropertySet();
                        BitmapTypedValue qualityValue = new BitmapTypedValue(0.5, PropertyType.Single);
                        propertySet.Add("ImageQuality", qualityValue);


                        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, outStream, propertySet);//建立编码器
                                encoder.SetPixelData(decoder.BitmapPixelFormat,
                                                     decoder.BitmapAlphaMode,
                                                     decoder.OrientedPixelWidth,
                                                     decoder.OrientedPixelHeight,
                                                      decoder.DpiX,
                                                      decoder.DpiY,
                                                      data
                                    );
                         
                                encoder.BitmapTransform.ScaledWidth = width;
                                encoder.BitmapTransform.ScaledHeight = height;
                                await encoder.FlushAsync(); 
                                HttpStreamContent streamContent1 = new HttpStreamContent(outStream);
                                fileContent.Add(streamContent1, "pic", inFile.Name);
                              }
                          }

              
                if (songPath != null)
                {
                    StorageFile file1 = await StorageFile.GetFileFromPathAsync(songPath);
                    IRandomAccessStreamWithContentType stream1 = await file1.OpenReadAsync();
                    HttpStreamContent streamContent1 = new HttpStreamContent(stream1);
                    fileContent.Add(streamContent1, "song", file1.Name);
                }

                HttpStringContent stringContent = new HttpStringContent(content);
                HttpStringContent stringName = new HttpStringContent(name);
                HttpStringContent stringUid = new HttpStringContent(uid);
                HttpStringContent stringUimage = new HttpStringContent(uimage);
                HttpStringContent stringTime = new HttpStringContent(date);
                HttpStringContent stringType = new HttpStringContent(type.ToString());
                

                fileContent.Add(stringContent, "content");
                fileContent.Add(stringUid, "phone");
                fileContent.Add(stringUimage, "uimage");

                fileContent.Add(stringName, "name");
                fileContent.Add(stringTime, "time");
                fileContent.Add(stringType, "type");

                HttpResponseMessage response = await httpClient.PostAsync(new Uri(posturi), fileContent);
              
                Init();
                notify.Hide();
                PostCommand.CanExecutes = true;
                NavigationHelp.NavigateTo(typeof(AllDreaming));
                



            }
            catch (Exception ex)
            {
                HelpMethods.Msg(ex.Message.ToString());
              
            }

        }


        public void Init()
        {
          
            ImagePathList = new List<string>();
            Song = null;
            Dreaming = null;
           
        }




    }
}
