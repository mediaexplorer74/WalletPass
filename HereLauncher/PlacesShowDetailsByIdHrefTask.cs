// Decompiled with JetBrains decompiler
// Type: Nokia.Phone.HereLaunchers.PlacesShowDetailsByIdHrefTask
// Assembly: HereLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A9474C2-AE7D-4EDA-8211-6A2D787D8226
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\HereLauncher.dll

using System;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class PlacesShowDetailsByIdHrefTask : TaskBase
  {
    public string Href { get; set; }

    public string Id { get; set; }

    public string Title { get; set; }

    public void Show()
    {
      if (string.IsNullOrEmpty(this.Href) && string.IsNullOrEmpty(this.Id))
        throw new InvalidOperationException("Please set the Place Id or Href for the place before calling Show()");
      string str1 = "places://v2.0/show/details/?";
      string uriString;
      if (!string.IsNullOrEmpty(this.Href))
      {
        string str2 = Uri.EscapeDataString(this.Href);
        uriString = str1 + "href=" + str2;
      }
      else
        uriString = str1 + "id=" + this.Id;
      if (!string.IsNullOrEmpty(this.Title))
      {
        string str3 = Uri.EscapeDataString(this.Title);
        uriString = uriString + "&title=" + str3;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
