// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseFieldImg
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Runtime.Serialization;

namespace Wallet_Pass
{
  [DataContract]
  public class ClaseFieldImg
  {
    [DataMember]
    public string label { get; set; }

    [DataMember]
    public byte[] imgLow { get; set; }

    [DataMember]
    public byte[] imgHigh { get; set; }

    public ClaseFieldImg(string label)
    {
      this.label = label;
      this.imgLow = (byte[]) null;
      this.imgHigh = (byte[]) null;
    }

    public ClaseFieldImg(string label, byte[] imgLow, byte[] imgHigh)
    {
      this.label = label;
      this.imgLow = imgLow;
      this.imgHigh = imgHigh;
    }
  }
}
