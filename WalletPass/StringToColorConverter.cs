// WalletPass.StringToColorConverter

using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
//using System.Windows.Data;
//using System.Windows.Media;

namespace WalletPass
{
  public sealed class StringToColorConverter : IValueConverter
  {
        public object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
          string str = (string) value;
          byte num1 = byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber);
          byte num2 = byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber);
          byte num3 = byte.Parse(str.Substring(7, 2), NumberStyles.HexNumber);

          return (object) new SolidColorBrush(Color.FromArgb(byte.Parse(str.Substring(1, 2), 
              NumberStyles.HexNumber), num1, num2, num3));
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
          Color color = (Color) value;
          return (object) ("#" + color.A.ToString("X2") 
                    + color.R.ToString("X2") + color.G.ToString("X2")
                    + color.B.ToString("X2"));
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = (string)value;
            byte num1 = byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber);
            byte num2 = byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber);
            byte num3 = byte.Parse(str.Substring(7, 2), NumberStyles.HexNumber);

            return (object)new SolidColorBrush(Color.FromArgb(byte.Parse(str.Substring(1, 2),
                NumberStyles.HexNumber), num1, num2, num3));
        }

        public object ConvertBack(object value, Type targetType, object parameter, 
            string language)
        {
            Color color = (Color)value;

            return (object)("#" + 
                color.A.ToString("X2") 
                + color.R.ToString("X2")
                + color.G.ToString("X2") 
                + color.B.ToString("X2"));
        }
    }
}
