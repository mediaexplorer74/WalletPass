// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.TimeSpanExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls.Common
{
  [Obsolete("Moved to Coding4Fun.Toolkit.dll now, Namespace is System, ")]
  public static class TimeSpanExtensions
  {
    public static TimeSpan CheckBound(this TimeSpan value, TimeSpan maximum) => System.TimeSpanExtensions.CheckBound(value, maximum);

    public static TimeSpan CheckBound(this TimeSpan value, TimeSpan minimum, TimeSpan maximum) => System.TimeSpanExtensions.CheckBound(value, minimum, maximum);
  }
}
