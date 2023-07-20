// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Tile
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class Tile : ButtonBase
  {
    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof (TextWrapping), typeof (TextWrapping), typeof (Tile), new PropertyMetadata((object) (TextWrapping) 1));

    public Tile() => ((Control) this).DefaultStyleKey = (object) typeof (Tile);

    public TextWrapping TextWrapping
    {
      get => (TextWrapping) ((DependencyObject) this).GetValue(Tile.TextWrappingProperty);
      set => ((DependencyObject) this).SetValue(Tile.TextWrappingProperty, (object) value);
    }
  }
}
