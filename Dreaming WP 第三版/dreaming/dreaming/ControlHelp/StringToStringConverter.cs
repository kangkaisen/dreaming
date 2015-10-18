using DataHelp.Common;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace dreaming.ControlHelp
{
    public  class StringToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string image = value as string;
            if (image != null)
            {
                return new Uri(Config.apiFile + image);
            }
            else
            {
                return null;
            }
        
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
