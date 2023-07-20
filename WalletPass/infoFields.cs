// Decompiled with JetBrains decompiler
// Type: WalletPass.infoFields
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

namespace WalletPass
{
  public class infoFields
  {
    public string Label { get; set; }

    public string Value { get; set; }

    public infoFields(string label, string value)
    {
      this.Label = label;
      this.Value = value;
    }
  }
}
