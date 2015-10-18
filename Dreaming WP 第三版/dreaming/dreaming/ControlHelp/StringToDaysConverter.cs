using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace dreaming.ControlHelp
{
    public class StringToDaysConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string time= value as string;
            if (time!= null)
            {
            string[] strArray = time.Split('-');
            int year =Int32.Parse(strArray[0].ToString());
            int month = Int32.Parse(strArray[1].ToString());
            int day = Int32.Parse(strArray[2].ToString());
            DateTime date1 = new DateTime(year,month,day);
            DateTime date2 = DateTime.Now;
            int days = date2.Subtract(date1).Days;
            string dayStr = "已坚持追梦" + days.ToString() + "天";
            return dayStr;
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
