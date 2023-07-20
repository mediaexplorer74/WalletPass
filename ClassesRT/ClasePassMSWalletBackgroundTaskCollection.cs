// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassMSWalletBackgroundTaskCollection
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wallet_Pass
{
  public class ClasePassMSWalletBackgroundTaskCollection : ObservableCollection<string>
  {
    public ClasePassMSWalletBackgroundTaskCollection()
    {
    }

    public ClasePassMSWalletBackgroundTaskCollection(List<string> passes)
    {
      for (int index = 0; index < passes.Count; ++index)
        this.Add(passes[index]);
    }
  }
}
