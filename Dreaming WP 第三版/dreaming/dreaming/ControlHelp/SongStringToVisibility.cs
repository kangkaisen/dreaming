using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace dreaming.ControlHelp
{
    public class SongStringToVisibility : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
             string str = value as string;
             if (str!= null)
             {
                return Visibility.Visible;
             }
             return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
