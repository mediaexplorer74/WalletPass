// Decompiled with JetBrains decompiler
// Type: ZXing.Dimension
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IDimensionFactory), 917504)]
  public sealed class Dimension : IDimensionClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Dimension([In] int width, [In] int height);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern bool Equals([In] object other);

    public extern int Height { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
