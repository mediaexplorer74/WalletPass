// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.passType
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;

namespace Wallet_Pass
{
  public class passType
  {
    public List<passField> headerFields { get; set; }

    public List<passField> primaryFields { get; set; }

    public List<passField> secondaryFields { get; set; }

    public List<passField> backFields { get; set; }

    public List<passField> auxiliaryFields { get; set; }

    public string transitType { get; set; }
  }
}
