// Decompiled with JetBrains decompiler
// Type: ZXing.PDF417.Internal.Dimensions
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.PDF417.Internal
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IDimensionsFactory), 917504)]
  public sealed class Dimensions : IDimensionsClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Dimensions([In] int minCols, [In] int maxCols, [In] int minRows, [In] int maxRows);

    public extern int MaxCols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int MaxRows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int MinCols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int MinRows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
