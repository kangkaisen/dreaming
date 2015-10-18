using DataHelp.Common;
using dreaming.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace dreaming.ControlHelp
{
    public static class HelpMethods
    {
        //消息提醒
        public static void Msg(string msg)
        {
            NotifyControl notify = new NotifyControl();
            notify.Text = msg;
            notify.Show();
        }

       //static async void ShowStatusBar()
       // {
       //     StatusBar StatusBar = StatusBar.GetForCurrentView();
       //     StatusBar.ForegroundColor = Color.FromArgb(0xFF, 0x35, 0xE7, 0x9E);
       //     StatusBar.BackgroundColor = Colors.White;
       //     StatusBar.ProgressIndicator.Text = " ";
       //     StatusBar.ProgressIndicator.ProgressValue = 0;
       //     await StatusBar.ProgressIndicator.ShowAsync();
       // }

        //隐藏状态栏
        public static async void HideStatusBar()
        {
           await  Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
        }

        //获取当前时间
        public static string GetTimeNow()
        {
           
            string format = "yyyy-MM-dd HH:mm:ss";
            DateTime now=DateTime.Now;
            return now.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }
        //获取屏幕宽度
        public static double GetWindowsWidth()
        {
            Windows.UI.Xaml.Controls.Frame rootFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            double Width = rootFrame.ActualWidth;
            return Width;
        }
        public static double GetWindowHeight()
        {
            Windows.UI.Xaml.Controls.Frame rootFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            double height = rootFrame.ActualHeight;
            return height;

        }
        //清除用户数据
        public static void UserClear()
        {
            Config.UserPhone = null;
            Config.UserName = null;
            Config.UserImage = null;
            Config.UserDream = null;
            Config.UserCare = 0;
            Config.UserFollow = 0;
            Config.UserPost = 0;
        }

        public static string GetVersionString()
        {
            string appVersion = string.Format("{0}.{1}.{2}.{3}",
                Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Build,
                Package.Current.Id.Version.Revision);
            return appVersion;
        }

        //public static void btn_NightMode_Click(Page page)
        //{
        //    CNBlogs.DataHelper.DataModel.CNBlogSettings setting = CNBlogs.DataHelper.DataModel.CNBlogSettings.Instance;
        //    if (setting.NightModeTheme)    // true = night mode
        //    {
        //        page.RequestedTheme = ElementTheme.Light;
        //        setting.NightModeTheme = false;
        //    }
        //    else // false = day mode
        //    {
        //        page.RequestedTheme = ElementTheme.Dark;
        //        setting.NightModeTheme = true;
        //    }
        //}

        //public static void SetTheme(Page page)
        //{
        //    if (1)
        //    {
        //        page.RequestedTheme = ElementTheme.Dark;
        //    }
        //    else
        //    {
        //        page.RequestedTheme = ElementTheme.Light;
        //    }
        //}

        public static void RefreshUIOnDataLoading(ProgressBar pb, CommandBar cb)
        {
            if (cb != null)
            {
                cb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (pb != null)
            {
                pb.IsIndeterminate = true;
                pb.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public static void RefreshUIOnDataLoaded(ProgressBar pb, CommandBar cb)
        {
            if (cb != null)
            {
                cb.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            if (pb != null)
            {
                pb.IsIndeterminate = false;
                pb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public static void ListViewScrollToTop(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                var item0 = lv.Items[0];
                lv.ScrollIntoView(item0, ScrollIntoViewAlignment.Leading);
            }
        }

        public static void GridViewScrollToTop(GridView gv)
        {
            if (gv.Items.Count > 0)
            {
                var item0 = gv.Items[0];
                gv.ScrollIntoView(item0, ScrollIntoViewAlignment.Leading);
            }
        }

        public static ScrollViewer GetScrollViewer(Windows.UI.Xaml.DependencyObject depObj)
        {
            if (depObj is ScrollViewer)
            {
                return depObj as ScrollViewer;
            }

            for (int i = 0; i < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

      public   static T FindChildType<T>(DependencyObject root) where T : class
        { //创建一个队列来存放可视化树对象
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            //循环查找类型
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                //查找子节点的类型对象
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        //public T FindFirstVisualChild<T>(DependencyObject obj, string childName) where T : DependencyObject
        //{
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //    {
        //        DependencyObject child = VisualTreeHelper.GetChild(obj, i);
        //        if (child != null && child is T && child.GetValue(NameProperty).ToString() == childName)
        //        {
        //            return (T)child;
        //        }
        //        else
        //        {
        //            T childOfChild = FindFirstVisualChild<T>(child, childName);
        //            if (childOfChild != null)
        //            {
        //                return childOfChild;
        //            }
        //        }
        //    }
        //    return null;
        //}


    }
}
