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
    internal class DateTimeToFormatStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("value can not be null");

            string @return = string.Empty;
            var input = (DateTime)value;

            if (input.Year != DateTime.Now.Year) @return += input.ToString("yyyy年");
            @return += input.ToString("M") + " " + input.ToString("HH:mm");

            return @return;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
