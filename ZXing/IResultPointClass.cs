// Decompiled with JetBrains decompiler
// Type: ZXing.IResultPointClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(3157303570, 61349, 22354, 85, 188, 109, 194, 231, 17, 36, 234)]
  [Version(917504)]
  [ExclusiveTo(typeof (ResultPoint))]
  internal interface IResultPointClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool Equals([In] object other);

    float X { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    float Y { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
