// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.ThemedImageConverterHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public static class ThemedImageConverterHelper
  {
    private static readonly Dictionary<string, BitmapImage> ImageCache = new Dictionary<string, BitmapImage>();

    public static BitmapImage GetImage(string path, bool negateResult = false)
    {
      if (string.IsNullOrEmpty(path))
        return (BitmapImage) null;
      bool flag = Application.Current.Resources.Contains((object) "PhoneDarkThemeVisibility") && (Visibility) Application.Current.Resources[(object) "PhoneDarkThemeVisibility"] == 0;
      if (negateResult)
        flag = !flag;
      path = string.Format(path, flag ? (object) "dark" : (object) "light");
      BitmapImage image;
      if (!ThemedImageConverterHelper.ImageCache.TryGetValue(path, out image))
      {
        image = new BitmapImage(new Uri(path, UriKind.Relative));
        ThemedImageConverterHelper.ImageCache.Add(path, image);
      }
      return image;
    }
  }
}
