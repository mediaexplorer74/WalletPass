// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.StringToColorConverter
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Wallet_Pass
{
  public sealed class StringToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      string str = (string) value;
      byte r = byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber);
      byte g = byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber);
      byte b = byte.Parse(str.Substring(7, 2), NumberStyles.HexNumber);
      return (object) Color.FromArgb(byte.Parse(str.Substring(1, 2), NumberStyles.HexNumber), r, g, b);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      Color color = (Color) value;
      return (object) ("#" + color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
    }
  }
}
