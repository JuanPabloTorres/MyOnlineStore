using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource castedValue = null;
            MemoryStream stream;
            if (value != null)
            {
                stream = new MemoryStream((byte[])value);
                castedValue = ImageSource.FromStream(()=>stream);
            }

            return castedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
