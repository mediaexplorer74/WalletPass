// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Internal.Version
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Internal
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Static(typeof (IVersionStatic), 917504)]
  public sealed class Version : IVersionClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern Version getVersionForDimensions([In] int numRows, [In] int numColumns);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getDataRegionSizeColumns();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getDataRegionSizeRows();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getSymbolSizeColumns();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getSymbolSizeRows();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getTotalCodewords();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getVersionNumber();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
