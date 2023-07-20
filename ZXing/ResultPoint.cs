// Decompiled with JetBrains decompiler
// Type: ZXing.ResultPoint
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
  [Activatable(917504)]
  [Activatable(typeof (IResultPointFactory), 917504)]
  [Static(typeof (IResultPointStatic), 917504)]
  public sealed class ResultPoint : IResultPointClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ResultPoint();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ResultPoint([In] float x, [In] float y);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern float distance([In] ResultPoint pattern1, [In] ResultPoint pattern2);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern bool Equals([In] object other);

    public extern float X { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern float Y { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
