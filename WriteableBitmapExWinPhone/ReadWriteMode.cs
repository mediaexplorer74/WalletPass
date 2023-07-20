// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.ReadWriteMode
// Assembly: WriteableBitmapExWinPhone, Version=1.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B0ADAD6-8531-4A99-AF69-71B377344AB9
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.xml

namespace System.Windows.Media.Imaging
{
  /// <summary>Read Write Mode for the BitmapContext.</summary>
  public enum ReadWriteMode
  {
    /// <summary>On Dispose of a BitmapContext, do not Invalidate</summary>
    ReadOnly,
    /// <summary>On Dispose of a BitmapContext, invalidate the bitmap</summary>
    ReadWrite,
  }
}
