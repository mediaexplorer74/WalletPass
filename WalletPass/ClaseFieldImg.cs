// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseFieldImg
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Runtime.Serialization;

namespace WalletPass
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
