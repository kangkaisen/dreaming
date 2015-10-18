using dreaming.ControlHelp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace dreaming.Cache
{
   public static class FileManager
    {
      public static  StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      
       public static async void CreateFile(string fileName, string fileContent)
       {
           StorageFile file = await localFolder.GetFileAsync(fileName);
           await FileIO.WriteTextAsync(file,fileContent);
       }

       public static async void CreateAndInitFile(string fileName, string fileContent)
       {
           StorageFile storageFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
           await FileIO.WriteTextAsync(storageFile, fileContent);
       }
       public static async void FileInit(string fileName)
       {

          
        StorageFile storageFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
           
       }

       public static async Task<string> GetFile(string fileName)
       {
           StorageFile file = await localFolder.GetFileAsync(fileName);
           string fileContent = await FileIO.ReadTextAsync(file);
           return fileContent;

       }

       public static async Task<bool> IsExistFile(string fileName)
       {
           IReadOnlyList<StorageFile> files = await localFolder.GetFilesAsync();
           bool isExist = files.Any(file => file.Name == fileName);
           return isExist;
       }

      
    }
}
