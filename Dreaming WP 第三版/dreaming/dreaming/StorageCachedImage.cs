using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace dreaming.Cache
{
    public class StorageCachedImage : BitmapSource
    {
        public DependencyProperty UriSourceProperty = DependencyProperty.Register("UriSource", typeof(string), typeof(StorageCachedImage), new PropertyMetadata( new PropertyChangedCallback(UriSourcePropertyChangedCallback)));
       
      

        public string UriSource
        {
            get { return (string)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty,value); }
        }

       
        
           

        private async static  void UriSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
                Debug.WriteLine("我被触发了");
                StorageCachedImage image = d as StorageCachedImage;
                string i = e.NewValue as string;
                int k = i.IndexOf("image");
                string imagePath = i.Substring(k + 7);
                string uri = App.Uri + "public/images/" + imagePath;
                Debug.WriteLine(uri);
              
             
                Uri imageUri = new Uri(uri);

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(imageUri);
                var buffer = await response.Content.ReadAsBufferAsync();
                var memoryStream = new InMemoryRandomAccessStream();
                await memoryStream.WriteAsync(buffer);
                await memoryStream.FlushAsync();
             
                await image.SetSourceAsync(memoryStream);
              
            

        }

     


    
        
    }
}
