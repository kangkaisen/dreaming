using DataHelp.Common;
using DataHelp.Help;
using DataHelp.Models;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace dreaming.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageView : Page
    {
        public ImageView()
        {
            this.InitializeComponent();
            
        }

        DreamingModel dreaming;
      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                dreaming = (DreamingModel)e.Parameter;
           
                this.flipview.ItemsSource = dreaming.image;
               
            }
        }

        private   void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            DreamingModel.imageModel image = this.flipview.SelectedItem as DreamingModel.imageModel;

            string uri = image.i;

            string imagePath = Config.apiFile + uri;
               


            DownloadAndScale("dreaming.jpg", imagePath);
            ToastNotify.Notify("保存成功!");

        }


   

        private async void DownloadAndScale(string outfileName, string downloadUriString)
        {
             Uri downLoadingUri = new Uri(downloadUriString);
             var client = new HttpClient();
    
            using (var response = await client.GetAsync(downLoadingUri))
            {
                var buffer = await response.Content.ReadAsBufferAsync();
                var memoryStream = new InMemoryRandomAccessStream();
                await memoryStream.WriteAsync(buffer);
                await memoryStream.FlushAsync();
                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(memoryStream);

                var pixelProvider = await decoder.GetPixelDataAsync();



                var localFolder = Windows.Storage.KnownFolders.PicturesLibrary;

                var scaledFile = await localFolder.CreateFileAsync(outfileName, CreationCollisionOption.GenerateUniqueName);
             
                using (var scaledFileStream = await scaledFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                {


                    var encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync( BitmapEncoder.JpegEncoderId, scaledFileStream);
                    var pixels = pixelProvider.DetachPixelData();
                    encoder.SetPixelData(
                        decoder.BitmapPixelFormat,
                        decoder.BitmapAlphaMode,
                        decoder.PixelWidth,
                        decoder.PixelHeight,
                        decoder.DpiX,
                        decoder.DpiY,
                        pixels
                        );
                    await encoder.FlushAsync();
                }
            }
        }
    }
}
