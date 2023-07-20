// Decompiled with JetBrains decompiler
// Type: ZXing.Common.ReedSolomon.GenericGF
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Common.ReedSolomon
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IGenericGFFactory), 917504)]
  public sealed class GenericGF : IGenericGFClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern GenericGF([In] int primitive, [In] int size, [In] int genBase);

    public extern int GeneratorBase { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Size { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
