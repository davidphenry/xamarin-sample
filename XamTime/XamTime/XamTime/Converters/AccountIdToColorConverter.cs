using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTime.Converters
{
    public class AccountIdToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.White;
            string accountId = value.ToString();
            if (string.IsNullOrEmpty(accountId))
                return Color.White;
            int id;
            int.TryParse(accountId, out id);
            if (id % 2 == 0)
                return Color.Pink;

            return Color.Yellow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
