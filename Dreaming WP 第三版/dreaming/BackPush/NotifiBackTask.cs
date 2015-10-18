using DataHelp.Help;
using DataHelp.Models;
using DataHelp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using DataHelp.Common;


namespace BackPush
{
    public sealed class NotifiBackTask : IBackgroundTask
    {
       
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Windows.Networking.PushNotifications.RawNotification notification = taskInstance.TriggerDetails as Windows.Networking.PushNotifications.RawNotification;

            if (notification != null)
            {
               
                string message = notification.Content;
                string[] items = message.Split('|');
                int type = int.Parse(items[0]);
                if (type == 1)
                {
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(MessageModel));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(items[1]));
                    MessageModel messageModel = ds.ReadObject(ms) as MessageModel;
                    UserModel chatUser = new UserModel
                    {
                        uid = messageModel.myPhone,
                        name = messageModel.myName,
                        image = messageModel.myImage,
                        dream = messageModel.myDream,
                        isRead= false
                    };
                    DbService.Current.InsertOrUpdateFalse(chatUser);
                    if (Config.IsNotify)
                    {
                        switch (messageModel.type)
                        {
                            case 0:
                                ToastUpdate.Update("私信  " + messageModel.myName + ":" + messageModel.msg);
                                break;
                            case 1:
                                ToastUpdate.Update("私信  " + messageModel.myName + "向您发送了一段语音");
                                break;
                            case 2:
                                ToastUpdate.Update("私信  " + messageModel.myName + "向您发送了一张图片");
                                break;
                            default:
                                break;

                        }
                    }
                   
                   
                }
           
               


                
            }
        }
    }
}
