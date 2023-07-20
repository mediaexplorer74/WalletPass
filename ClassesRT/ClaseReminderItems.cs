// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseReminderItems
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;

namespace Wallet_Pass
{
  public class ClaseReminderItems
  {
    public TimeSpan listPickerCalendarItemTimeSpan(int id)
    {
      switch (id)
      {
        case 0:
          return TimeSpan.Zero;
        case 1:
          return TimeSpan.FromSeconds(0.0);
        case 2:
          return TimeSpan.FromMinutes(5.0);
        case 3:
          return TimeSpan.FromMinutes(10.0);
        case 4:
          return TimeSpan.FromMinutes(15.0);
        case 5:
          return TimeSpan.FromMinutes(30.0);
        case 6:
          return TimeSpan.FromHours(1.0);
        case 7:
          return TimeSpan.FromHours(4.0);
        case 8:
          return TimeSpan.FromHours(18.0);
        case 9:
          return TimeSpan.FromDays(1.0);
        case 10:
          return TimeSpan.FromDays(7.0);
        default:
          return TimeSpan.Zero;
      }
    }

    public TimeSpan listPickerNotificationItemTimeSpan(int id)
    {
      switch (id)
      {
        case 0:
          return TimeSpan.FromMinutes(20.0);
        case 1:
          return TimeSpan.FromMinutes(45.0);
        case 2:
          return TimeSpan.FromHours(1.0);
        case 3:
          return TimeSpan.FromHours(4.0);
        case 4:
          return TimeSpan.FromHours(18.0);
        case 5:
          return TimeSpan.FromDays(1.0);
        default:
          return TimeSpan.Zero;
      }
    }
  }
}
