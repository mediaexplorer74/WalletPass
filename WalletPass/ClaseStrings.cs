// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseStrings
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WalletPass
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
