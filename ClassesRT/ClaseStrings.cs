// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseStrings
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Wallet_Pass
{
  [DataContract]
  public class ClaseStrings
  {
    [DataMember]
    public List<ClaseField> Fields;

    [DataMember]
    public string Language { get; set; }

    public ClaseStrings(string language)
    {
      this.Language = language;
      this.Fields = new List<ClaseField>();
    }

    public void AddField(string label, string value) => this.Fields.Add(new ClaseField(label, value));
  }
}
