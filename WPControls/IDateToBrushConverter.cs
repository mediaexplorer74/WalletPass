// Decompiled with JetBrains decompiler
// Type: WPControls.IDateToBrushConverter
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System;
using System.Windows.Media;

namespace WPControls
{
  public interface IDateToBrushConverter
  {
    Brush Convert(DateTime dateTime, bool isSelected, Brush defaultValue, BrushType brushType);
  }
}
