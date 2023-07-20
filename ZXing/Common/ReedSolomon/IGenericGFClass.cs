// Decompiled with JetBrains decompiler
// Type: ZXing.Common.ReedSolomon.IGenericGFClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common.ReedSolomon
{
  [Guid(2099574600, 52177, 22763, 73, 138, 15, 219, 212, 239, 200, 131)]
  [Version(917504)]
  [ExclusiveTo(typeof (GenericGF))]
  internal interface IGenericGFClass
  {
    int GeneratorBase { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Size { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
