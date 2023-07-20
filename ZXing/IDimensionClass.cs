// Decompiled with JetBrains decompiler
// Type: ZXing.IDimensionClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(577576125, 63733, 20711, 79, 221, 210, 198, 104, 129, 102, 139)]
  [Version(917504)]
  [ExclusiveTo(typeof (Dimension))]
  internal interface IDimensionClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool Equals([In] object other);

    int Height { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
