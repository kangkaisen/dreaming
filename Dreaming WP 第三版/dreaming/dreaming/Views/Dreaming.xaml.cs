using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using dreaming.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace dreaming.Views
{
    
    public sealed partial class Dreaming : Page
    {
        public Dreaming()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
          
            Imagelist = new ObservableCollection<DreamingModel.imageModel>();
        }

        MediaCapture _mediaCaptureManager = null;

        private StorageFile _recordStorageFile;

        public ObservableCollection<DataHelp.Models.DreamingModel.imageModel> Imagelist;
        bool isPicure = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter != null&&!isPicure)
            {
                int id = (int)e.Parameter;
                this.DataContext = new DreamingViewModel(id);
            }
            
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (!isPicure)
            {
                songMedia.Source = null;
                song.Visibility = Visibility.Collapsed;
                Imagelist = new ObservableCollection<DreamingModel.imageModel>();
                listView.ItemsSource = null;
                image.IsEnabled = true;
            }
            
            base.OnNavigatingFrom(e);
        }


        private void image_Click(object sender, RoutedEventArgs e)
         {
            isPicure = true;
            Config.pictrueType = 1;
            image.IsEnabled = false;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.ContinuationData["Operation"] = "ImageWeibo";
            openPicker.PickMultipleFilesAndContinue();
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
            if ((args.ContinuationData["Operation"] as string) == "ImageWeibo" && args.Files != null && args.Files.Count > 0)
            {


                for (int i = 0; i < args.Files.Count; i++)
                {

                    ((DreamingViewModel)(DataContext)).ImagePathList.Add(args.Files[i].Path);


                    string path = await  ImageHelp.GetImagePath(args.Files[i]);
                     Imagelist.Add(new DataHelp.Models.DreamingModel.imageModel { i = path});

                }
                listView.ItemsSource = Imagelist;
            }
            isPicure = false;
        }



       

        private async void record_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                _mediaCaptureManager = new MediaCapture();
                var settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;//设置为音频
                 await _mediaCaptureManager.InitializeAsync(settings);


                gridRecord.Visibility = Visibility.Visible;
               

                
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

       

        //放弃录音
        private void cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_mediaCaptureManager != null)
            {
                _mediaCaptureManager.Dispose();
                _mediaCaptureManager = null;
            }

            gridRecord.Visibility = Visibility.Collapsed;
           
            
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

        private async void accept_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ((DreamingViewModel)(DataContext)).Song = _recordStorageFile.Path;


            gridRecord.Visibility = Visibility.Collapsed;
           
            stop.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Collapsed;
            accept.Visibility = Visibility.Collapsed;
            if (_mediaCaptureManager != null)
            {
                _mediaCaptureManager.Dispose();
                _mediaCaptureManager = null;
            }
            
            var stream = await _recordStorageFile.OpenAsync(FileAccessMode.Read);
            songMedia.SetSource(stream, _recordStorageFile.FileType);
            songMedia.AutoPlay = false;
            song.Visibility = Visibility.Visible;
            
        }

        private void song_Click(object sender, RoutedEventArgs e)
        {
          
            songMedia.Play();
        }

     

    }
}
