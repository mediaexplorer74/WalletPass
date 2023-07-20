// Decompiled with JetBrains decompiler
// Type: WalletPass.DateTimeToTimestampConverter
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace WalletPass
{
  public sealed class DateTimeToTimestampConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      DateTime dateTime1 = (DateTime) value;
      DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, dateTime1.Kind);
      return (object) System.Convert.ToInt64((dateTime1 - dateTime2).TotalSeconds);
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) (long) value);
    }
  }
}
