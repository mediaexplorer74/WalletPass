// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ImageSourceExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class ImageSourceExtensions
  {
    private const string IsoStoreScheme = "isostore:/";
    private const string MsAppXScheme = "ms-appx:///";

    public static ImageSource ToBitmapImage(this ImageSource imageSource)
    {
      if (imageSource == null)
        return (ImageSource) null;
      if (!(imageSource is BitmapImage bitmapImage))
        return imageSource;
      string lower = bitmapImage.UriSource.ToString().ToLower();
      if (lower.StartsWith("isostore:/") || lower.StartsWith("ms-appx:///"))
      {
        string path = lower.Replace("isostore:/", string.Empty).TrimEnd('.').Replace("ms-appx:///", string.Empty).TrimEnd('.');
        bitmapImage = new BitmapImage();
        if (!ApplicationSpace.IsDesignMode)
        {
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          {
            if (storeForApplication.FileExists(path))
            {
              using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(path, FileMode.Open))
                ((BitmapSource) bitmapImage).SetSource((Stream) storageFileStream);
            }
          }
        }
      }
      return (ImageSource) bitmapImage;
    }
  }
}
