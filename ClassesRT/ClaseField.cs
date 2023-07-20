// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseField
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

namespace Wallet_Pass
{
  public class ClaseField
  {
    public string Label { get; set; }

    public string Value { get; set; }

    public string Alignment { get; set; }

    public string changeMessage { get; set; }

    public ClaseField()
      : this("", "", "", "")
    {
    }

    public ClaseField(string label, string value)
      : this(label, value, "", "")
    {
    }

    public ClaseField(string label, string value, string alignment)
      : this(label, value, alignment, "")
    {
    }

    public ClaseField(string label, string value, string alignment, string changeMessage)
    {
      this.Label = label;
      this.Value = value;
      this.Alignment = alignment;
      this.changeMessage = changeMessage;
    }
  }
}
