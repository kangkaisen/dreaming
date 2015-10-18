using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace dreaming.ControlHelp
{
   public class StringToTimeConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string time =value as string;
            string timeString = "";
            if (time != null)
            {
                DateTime dateTimeToConvert = DateTime.Parse(time);
                TimeSpan timeLag = System.DateTime.Now - dateTimeToConvert;
                if (dateTimeToConvert.Date == System.DateTime.Now.Date)
                {
                    if (TimeSpan.FromHours(1) > timeLag)
                    {

                            timeString = string.Format("{0}" + "分钟前", timeLag.Minutes);
                    }
                    else
                    {
                        timeString = string.Format("今天" + "{0}:{1:d2}", dateTimeToConvert.Hour, dateTimeToConvert.Minute);
                    }
                }
                else if (dateTimeToConvert.AddDays(1).Date == System.DateTime.Now.Date)
                {
                    timeString = string.Format("昨天" + "{0}:{1:d2}", dateTimeToConvert.Hour, dateTimeToConvert.Minute);
                }

                else
                {
                    timeString = dateTimeToConvert.ToString("yyyy-MM-dd");
                }
            }

            
     
            return timeString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
