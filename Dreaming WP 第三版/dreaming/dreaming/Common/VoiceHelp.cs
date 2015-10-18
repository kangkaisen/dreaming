using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace dreaming.ControlHelp
{
   public class VoiceHelp
    {
       private const string VcdPath = "VoiceCommandDefinition1.xml";

       public static  async void RegisterVcd()
       {
           

           try
           {
               
               StorageFile file=await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(VcdPath);
               await VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(file);
           }
           catch (Exception ex)
           {
               Debug.WriteLine(ex.HResult + ex.Message);
           }
       }
    }
}
