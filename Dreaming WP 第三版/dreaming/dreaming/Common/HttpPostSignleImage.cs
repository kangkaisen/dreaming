using DataHelp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace dreaming.ControlHelp
{
   public class HttpPostSignleImage
    {
       public static async Task<string> HttpPost(StorageFile inFile)
       {
           HttpClient httpClient = new HttpClient();
           uint MaxImageWidth = 480;
           uint MaxImageHeight = 800;
           uint width;
           uint height;
           string posturi = Config.apiChatImage;
           HttpMultipartFormDataContent fileContent = new HttpMultipartFormDataContent();

                        var inStream = await inFile.OpenReadAsync();
                        InMemoryRandomAccessStream outStream = new InMemoryRandomAccessStream();
                        BitmapDecoder decoder = await ImageHelp.GetProperDecoder(inStream,inFile);
                       


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
                                HttpResponseMessage response = await httpClient.PostAsync(new Uri(posturi), fileContent);
                             
                             return await response.Content.ReadAsStringAsync();
       
       
       
       
       }





    }
}
