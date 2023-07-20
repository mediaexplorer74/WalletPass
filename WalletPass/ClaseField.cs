// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseField
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

namespace WalletPass
{
  public class ClaseField
  {
    public string Label { get; set; }

    public string Value { get; set; }

    public string Alignment { get; set; }

    public ClaseField()
      : this("", "", "")
    {
    }

    public ClaseField(string label, string value)
      : this(label, value, "")
    {
    }

    public ClaseField(string label, string value, string alignment)
    {
      this.Label = label;
      this.Value = value;
      this.Alignment = alignment;
    }
  }
}
