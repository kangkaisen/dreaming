using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace DataHelp.Help
{
    public class ToastNotify
    {
        public static void Notify(string content)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(content));
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("silent", "true");
            toastNode.AppendChild(audio);
            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(1);
            toast.Tag = "dreaming";
            toast.Dismissed += toast_Dismissed;
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
        static void toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            ToastNotificationManager.History.Remove("dreaming");
        }
    }
}
