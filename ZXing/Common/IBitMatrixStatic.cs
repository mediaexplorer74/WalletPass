// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitMatrixStatic
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(3701774532, 23849, 23091, 66, 20, 227, 71, 219, 182, 7, 187)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitMatrix))]
  internal interface IBitMatrixStatic
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitMatrix parse([In] string stringRepresentation, [In] string setString, [In] string unsetString);
  }
}
