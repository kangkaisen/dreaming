

namespace dreaming.Cache
{
    public class CacheEntity
    {
         /// <summary>
   /// 缓存起始时间
  /// </summary>
 
  public string StartDate { get; set; }
 
  /// <summary>
  /// 缓存周期
   /// </summary>
 
  public string CacheDate { get; set; }
  
   /// <summary>
   /// 唯一存储Key
   /// </summary>
 
  public string CacheKey { get; set; }
  
   /// <summary>
   /// 存储模块
   /// </summary>

  public string CacheDirName { get; set; }
   
  /// <summary>
  /// 存储文件名称
  /// </summary>
 
   public string CacheFileName { get; set; }//文件名称
   
   /// <summary>
  /// 缓存数据类型
  /// </summary>
  
  public string CacheContext { get; set; }//缓存数据类型



    }
}
