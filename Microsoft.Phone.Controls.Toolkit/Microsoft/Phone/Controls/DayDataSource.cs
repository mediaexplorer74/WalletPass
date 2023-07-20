// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DayDataSource
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;

namespace Microsoft.Phone.Controls
{
  public class DayDataSource : DataSource
  {
    protected override DateTime? GetRelativeTo(DateTime relativeDate, int delta)
    {
      int num = DateTime.DaysInMonth(relativeDate.Year, relativeDate.Month);
      int day = (num + relativeDate.Day - 1 + delta) % num + 1;
      return new DateTime?(new DateTime(relativeDate.Year, relativeDate.Month, day, relativeDate.Hour, relativeDate.Minute, relativeDate.Second));
    }
  }
}
