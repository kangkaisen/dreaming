using DataHelp.Common;
using dreaming.ControlHelp;
using dreaming.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace dreaming.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserData : Page
    {
        public UserData()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.DataContext = new UserDataViewModel();
         
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Config.pictrueType = 0;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.ContinuationData["Operation"] = "Image";
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


            if ((args.ContinuationData["Operation"] as string) == "Image" && args.Files != null && args.Files.Count > 0)
            {
             
                StorageFile inFile = args.Files[0];
                StorageFile imageFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(inFile.Name, CreationCollisionOption.ReplaceExisting);
              
                var inStream = await inFile.OpenAsync(FileAccessMode.Read);
               
                var outStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite);
                outStream.Size = 0;
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(inStream);
                PixelDataProvider provider = await decoder.GetPixelDataAsync();
                byte[] data = provider.DetachPixelData();
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, outStream);
                encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode,
                                                   decoder.PixelWidth, decoder.PixelHeight,
                                                   decoder.DpiX, decoder.DpiY, data
                    );

                try
                {
                    await encoder.FlushAsync();

                    ((UserDataViewModel)DataContext).ImagePath = imageFile.Path;
             

                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.ToString());
                }
                finally
                {

                }
            }
        }





       
    }
}
