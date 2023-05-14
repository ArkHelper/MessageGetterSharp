using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFDemo.Converter
{
    internal class PictureCountToWrappanelWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("value can not be null");
            var input = (List<Picture>)value;

            double @return;
            if (input.Count > 3)
                @return = 315;
            else
                @return = 470;

            return @return;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
