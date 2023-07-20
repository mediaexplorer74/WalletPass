// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseStringsImg
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Wallet_Pass
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
