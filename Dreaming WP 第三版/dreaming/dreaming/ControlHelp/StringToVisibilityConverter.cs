
using DataHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace dreaming.ControlHelp
{
    //图片
 public   class StringToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<DreamingModel.imageModel> list = value as List<DreamingModel.imageModel>;
            if (list!=null&&list.Count > 0)
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
