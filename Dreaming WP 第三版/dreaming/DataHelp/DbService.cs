using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Collections.ObjectModel;
using DataHelp.Models;
using DataHelp.Common;
namespace DataHelp
{
   public class DbService
    {
       private DbService()
       {

       }

       private static DbService dbService;

       public static DbService Current
       {
           get
           {
               if (dbService == null)
               {
                   dbService = new DbService();
                   connection = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\msg.db");
               }
               return dbService;
           }
       }

       static SQLiteAsyncConnection connection;

       public async void Add(MessageModel msg)
       {
           await connection.InsertAsync(msg);
       }
       public async void Add(UserModel user)
       {
           await connection.InsertAsync(user);
       }

       public async void Add(NotifyModel notify)
       {
           await connection.InsertAsync(notify);
       }
       public async void AddList(ObservableCollection<MessageModel> msgList)
       {
           await connection.InsertAllAsync(msgList);
       }
       //public async void AddList(List<MessageModel> msgList)
       //{
       //    await connection.InsertAllAsync(msgList);
       //}
       public async void DeleteMsg(string id)
       {
           var query = from msg in connection.Table<MessageModel>()
                       where (msg.toPhone == id || msg.myPhone == id )
                       select msg;
           List<MessageModel> msgList = await query.ToListAsync();
           foreach (var item in msgList)
           {
               await connection.DeleteAsync(item);
           }
           
       }
       public async void DeleteNotify()
       {
          await connection.DropTableAsync<NotifyModel>();
       }
       public async void DeleteUser(UserModel user)
       {
           await connection.DeleteAsync(user);
       }
       public async Task<List<MessageModel>> FindMsg(string id)
       {
           var query = from msg in connection.Table<MessageModel>()
                       where (msg.toPhone == id||msg.myPhone==id)
                       orderby msg.time ascending
                       select msg;
           List<MessageModel> msgList = await query.ToListAsync();
           return msgList;
       }

       public async Task<List<UserModel>> FindUser()
       {
           var query = connection.Table<UserModel>();
           List<UserModel> userList = await query.ToListAsync();
           return userList;
       }

       public async Task<List<NotifyModel>> FindPraise()
       {
           var query = from n in connection.Table<NotifyModel>()
                       where n.type==0
                       orderby n.time descending
                       select n;
           List<NotifyModel> List = await query.ToListAsync();
           return List;
       }

       public async Task<List<NotifyModel>> FindComment()
       {
           var query = from n in connection.Table<NotifyModel>()
                       where n.type ==1||n.type==2
                       orderby n.time descending
                       select n;
           List<NotifyModel> List = await query.ToListAsync();
           return List;
       }
       public async void InsertOrUpdateFalse(UserModel User)
       {
           var query = from user in connection.Table<UserModel>()
                       where user.uid == User.uid
                       select user;
           UserModel userQuery = await query.FirstOrDefaultAsync();
           if (userQuery != null)
           {
               userQuery.isRead = false;
               await connection.UpdateAsync(userQuery);
              
           }
           else
           {
               await connection.InsertAsync(User);
           }

       }
      
       public async void InsertOrUpdateTrue(UserModel User)
       {
           var query = from user in connection.Table<UserModel>()
                       where user.uid == User.uid
                       select user;
           UserModel userQuery = await query.FirstOrDefaultAsync();
           if (userQuery != null)
           {
               userQuery.isRead = true;
               await connection.UpdateAsync(userQuery);
           }
           else
           {
               await connection.InsertAsync(User);
           }


       }

    }
}
