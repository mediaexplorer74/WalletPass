// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseStringsImg
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WalletPass
{
  [DataContract]
  public class ClaseStringsImg
  {
    [DataMember]
    public List<ClaseFieldImg> Fields;

    [DataMember]
    public string Language { get; set; }

    public ClaseStringsImg(string language)
    {
      this.Language = language;
      this.Fields = new List<ClaseFieldImg>();
    }

    public void AddImgToField(string label, bool typeIsLow, byte[] img)
    {
      bool flag = false;
      for (int index = 0; index < this.Fields.Count; ++index)
      {
        if (this.Fields[index].label == label)
        {
          flag = true;
          if (typeIsLow)
          {
            this.Fields[index].imgLow = img;
            break;
          }
          this.Fields[index].imgHigh = img;
          break;
        }
      }
      if (flag)
        return;
      this.AddField(label);
      if (typeIsLow)
        this.Fields[this.Fields.Count - 1].imgLow = img;
      else
        this.Fields[this.Fields.Count - 1].imgHigh = img;
    }

    public void AddField(string label) => this.Fields.Add(new ClaseFieldImg(label));

    public void AddField(string label, byte[] imgLow, byte[] imgHigh) => this.Fields.Add(new ClaseFieldImg(label, imgLow, imgHigh));
  }
}
