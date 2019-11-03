using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Converters
{
   public class BytesToImageConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                //Stream stream = new MemoryStream(imageAsBytes);
                // = ImageSource.FromStream(stream);
            }
            return retSource;

            //if (value != null)
            //{ byte[] imageAsBytes = (byte[])value; byte[] decodedByteArray = System.Convert.FromBase64String(Encoding.UTF8.GetString(imageAsBytes, 0, imageAsBytes.Length)); var stream = new MemoryStream(decodedByteArray);
            //    retSource = ImageSource.FromStream(() => stream);
            //}

            //return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

