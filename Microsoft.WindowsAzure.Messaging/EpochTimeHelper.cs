// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.EpochTimeHelper
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging
{
  internal static class EpochTimeHelper
  {
    public static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    public static DateTime GetDateTimeByPassingSeconds(int unixTicksExpiryTime) => EpochTimeHelper.UnixEpochStart.AddSeconds((double) unixTicksExpiryTime);

    public static long GetTotalSecondsByTimeSpan(TimeSpan timeToLive) => Convert.ToInt64((object) DateTime.UtcNow.Add(timeToLive).Subtract(EpochTimeHelper.UnixEpochStart).TotalSeconds, (IFormatProvider) CultureInfo.InvariantCulture);
  }
}
