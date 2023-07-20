// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Internal.IVersionStatic
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Internal
{
  [Guid(1253692234, 59115, 20619, 123, 19, 60, 233, 1, 252, 179, 32)]
  [Version(917504)]
  [ExclusiveTo(typeof (Version))]
  internal interface IVersionStatic
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Version getVersionForDimensions([In] int numRows, [In] int numColumns);
  }
}
