using DataHelp.Common;
using DataHelp.Models;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace dreaming.ControlHelp
{
    public class ChatBubbleSelector : DataTemplateSelector
    {
        public DataTemplate　textToBubble { get; set; }
        public DataTemplate  textFromBubble { get; set; }

        public DataTemplate voiceToBubble { get; set; }

        public DataTemplate voiceFromBubble { get; set; }
        public DataTemplate imageToBubble { get; set; }
        public DataTemplate imageFromBubble { get; set; }
       


        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = item as MessageModel;
            if (message.type == 0)
            {
                if (message == null || message.myPhone == Config.UserPhone)
                {
                    return textToBubble;
                }
                else
                {
                    return textFromBubble;
                }
            }
            else if (message.type == 1)
            {
                if (message == null || message.myPhone == Config.UserPhone)
                {
                    return voiceToBubble;
                }
                else
                {
                    return voiceFromBubble;
                }
            }
            else
            {
                if (message == null || message.myPhone == Config.UserPhone)
                {
                    return imageToBubble;
                }
                else
                {
                    return imageFromBubble;
                }
            }
           
           
        }
    }
}
