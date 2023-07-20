// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseGroup`1
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WalletPass
{
  public class ClaseGroup<T> : List<T>
  {
    public string Title { get; set; }

    public BitmapImage Icon { get; set; }

    public Brush color { get; set; }

    public ClaseGroup()
    {
    }

    public ClaseGroup(string name)
    {
      this.Title = this.correctText(name);
      this.color = (Brush) new SolidColorBrush(Colors.Transparent);
    }

    public ClaseGroup(string name, BitmapImage iconImage)
    {
      this.Title = this.correctText(name);
      this.Icon = iconImage;
      this.color = (Brush) new ImageBrush()
      {
        ImageSource = (ImageSource) iconImage
      };
    }

    public ClaseGroup(string name, BitmapImage iconImage, SolidColorBrush color)
    {
      this.Title = this.correctText(name);
      this.Icon = iconImage;
      this.color = (Brush) color;
    }

    private string correctText(string Text) => !string.IsNullOrEmpty(Text) ? Text : string.Empty;
  }
}
