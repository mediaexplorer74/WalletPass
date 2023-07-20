// Decompiled with JetBrains decompiler
// Type: ZXing.Common.ReedSolomon.IGenericGFFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common.ReedSolomon
{
  [Guid(3258575939, 16405, 22707, 120, 238, 57, 79, 233, 5, 142, 134)]
  [Version(917504)]
  [ExclusiveTo(typeof (GenericGF))]
  internal interface IGenericGFFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    GenericGF CreateGenericGF([In] int primitive, [In] int size, [In] int genBase);
  }
}
