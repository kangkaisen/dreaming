using DataHelp;
using DataHelp.Common;
using DataHelp.Help;
using DataHelp.Models;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace dreaming.ControlHelp
{
    public class SocKetHelp
    {


        public static Socket socket;

        public static void SocketInit()
        {
            socket = IO.Socket(Config.apiBase);
            socket.Emit("login", Config.UserPhone);
            //socket.On("chat", (data) =>
            //{
            //    string str = data.ToString();
                
            //    MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(str);
            //    DbService.Current.Add(messageModel);
            //    UserModel chatUser = new UserModel
            //    {
            //        uid = messageModel.myPhone,
            //        name = messageModel.myName,
            //        image = messageModel.myImage,
            //        dream = messageModel.myDream,
            //        isRead = false
            //    };
            //    DbService.Current.InsertOrUpdateFalse(chatUser);
            //    switch (messageModel.type)
            //    {
            //        case 0:
            //            ToastNotify.Notify("私信  "+messageModel.myName + ":" + messageModel.msg);
            //            break;
            //        case 1:
            //            ToastNotify.Notify("私信  " + messageModel.myName + "向您发送一段了语音");
            //            break;
            //        case 2:
            //            ToastNotify.Notify("私信  " + messageModel.myName + "向您发送了一张图片");
            //            break;
            //        default:
            //            break;

            //    }

            //});
            
            
        }

        
       
      
      
    }
}
