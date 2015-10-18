using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace dreaming.ControlHelp
{
    public class BackgroundTaskHelp
    {
        public static async void BackRegister()
        {
            string taskName = "back_notifi"; //后台任务名称
            string entryPoint = "BackPush.NotifiBackTask"; //入口点
            // 检查是否许后台任务
            var result = await BackgroundExecutionManager.RequestAccessAsync();
            if (result == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity)
            {
                // 检查是否已经注册后台任务
                var task = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault((t) => t.Name == taskName);
                // 如果未注册，则进行注册
                if (task == null)
                {
                    BackgroundTaskBuilder tb = new BackgroundTaskBuilder();
                    tb.TaskEntryPoint = entryPoint;
                    tb.Name = taskName;
                    // 触发器为推送通知触发器
                    tb.SetTrigger(new PushNotificationTrigger());
                    // 运行条件为网络可用
                    tb.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                    // 注册
                    tb.Register();
                }
            }
        }
    }
}
