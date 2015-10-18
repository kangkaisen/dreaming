using DataHelp.Common;
using DataHelp.Models;
using dreaming.Cache;
using dreaming.ControlHelp;
using dreaming.Views;
using Quobject.SocketIoClientDotNet.Client;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;



namespace dreaming
{
   
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;
        
           public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            Init();
            VoiceHelp.RegisterVcd();
            BackgroundTaskHelp.BackRegister();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }



           void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
           {
               Frame rootFrame = Window.Current.Content as Frame;
               if (rootFrame != null && rootFrame.CanGoBack)
               {
                   rootFrame.GoBack();
                   e.Handled = true;
               }
           }
          
           //文件初始化
           public async void Init()
           {
               bool isExist = await FileManager.IsExistFile(Config.CacheFileName);
               if(!isExist)
               {
                   FileManager.FileInit(Config.CacheFileName);
               }
             
               isExist = await FileManager.IsExistFile("msg.db");
               if(!isExist)
               {
                   using (var db = new SQLiteConnection(FileManager.localFolder.Path + "\\msg.db"))
                   {
                       db.CreateTable<MessageModel>();
                       db.CreateTable<UserModel>();
                       db.CreateTable<NotifyModel>();
                   }
               }
               
           }



        


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {


            Frame rootFrame = Window.Current.Content as Frame;

           
            if (rootFrame == null)
            {
                
                rootFrame = new Frame();

           
                rootFrame.CacheSize = 3;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    
                }

            
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
                if (Config.UserPhone == null)
                {
                    rootFrame.Navigate(typeof(UserLogin));
                }
                else
                {
                    rootFrame.Navigate(typeof(Main));
                }
               
               
            }

          
            Window.Current.Activate();
        }

        
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

       
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            
            deferral.Complete();
        }
      
        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args is FileOpenPickerContinuationEventArgs)
            {
                Frame rootFrame = Window.Current.Content as Frame;

                if (Config.pictrueType==1)
                {
                    
                    var p = rootFrame.Content as Dreaming;
                    p.FilePickerEvent = (FileOpenPickerContinuationEventArgs)args;
                    rootFrame.Navigate(typeof(Dreaming));
                
                }
                else if(Config.pictrueType==2)
                {
                    
                    var p = rootFrame.Content as Chat;
                    p.FilePickerEvent = (FileOpenPickerContinuationEventArgs)args;
                    rootFrame.Navigate(typeof(Chat));
                }
                else
                {
                    var p = rootFrame.Content as UserData;
                    p.FilePickerEvent = (FileOpenPickerContinuationEventArgs)args;
                    rootFrame.Navigate(typeof(UserData));
                }


                Window.Current.Activate();
            }



            if (args is VoiceCommandActivatedEventArgs)
            {
                VoiceCommandActivatedEventArgs voice = (VoiceCommandActivatedEventArgs)args;
                string result = voice.Result.Text;
                string path= voice.Result.RulePath[0];
                string navigation= voice.Result.SemanticInterpretation.Properties["NavigationTarget"][0];
                Debug.WriteLine(result);
                Debug.WriteLine(navigation);
                Frame rootFrame = new Frame();
                
                if (path=="dreamingStart")
                {
                    rootFrame.Navigate(typeof(AllDreaming),voice);
                }
                else if (result.Contains("发布"))
                {
                    rootFrame.Navigate(typeof(Dreaming),voice);
                }
                Window.Current.Content = rootFrame;
                Window.Current.Activate();

            }
        }
    }
}