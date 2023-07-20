// Decompiled with JetBrains decompiler
// Type: Nokia.Phone.HereLaunchers.PublicTransitSearchStopsTask
// Assembly: HereLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A9474C2-AE7D-4EDA-8211-6A2D787D8226
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\HereLauncher.dll

using System;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class PublicTransitSearchStopsTask : TaskBase
  {
    public void Show() => this.Launch(new Uri("public-transit://v2.0/search/stops/"));
  }
}
