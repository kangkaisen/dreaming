using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace DataHelp.Help
{
   public class ToastUpdate
    {
       public static void Update(string item1)
       {
           XmlDocument doctoast = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
           var txtnodes = doctoast.GetElementsByTagName("text");
    
            ((XmlElement)txtnodes[0]).InnerText =item1;
              
           
           // 发送Toast通知
           ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(doctoast));


           XmlDocument badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
           XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
           badgeElement.SetAttribute("value", "88");
           BadgeNotification badge = new BadgeNotification(badgeXml);
           BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);



           //// 更新磁贴
           //Windows.Data.Xml.Dom.XmlDocument doc = Windows.UI.Notifications.TileUpdateManager.GetTemplateContent(Windows.UI.Notifications.TileTemplateType.TileSquare150x150Text02);
           //var nodes = doc.SelectNodes("tile/visual/binding/text");
           //if (nodes.Count >= 2)
           //{
           //    // 修改XML值
           //    ((XmlElement)nodes[0]).InnerText = items[0];
           //    ((XmlElement)nodes[1]).InnerText = items[1];
           //}
           //// 更新磁贴
           //TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(doc));

           // 顺便也发一下Toast通知
       }
    }
}
