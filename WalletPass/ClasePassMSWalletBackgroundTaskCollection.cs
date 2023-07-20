// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassMSWalletBackgroundTaskCollection
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WalletPass
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
