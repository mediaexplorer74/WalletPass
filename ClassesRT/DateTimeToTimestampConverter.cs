// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.DateTimeToTimestampConverter
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using Windows.UI.Xaml.Data;

namespace Wallet_Pass
{
  public sealed class DateTimeToTimestampConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      DateTime dateTime1 = (DateTime) value;
      DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, dateTime1.Kind);
      return (object) System.Convert.ToInt64((dateTime1 - dateTime2).TotalSeconds);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) => (object) new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) (long) value);
  }
}
