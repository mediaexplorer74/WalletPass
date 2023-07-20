// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassCollectionUpdate
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WalletPass
{
  public class ClasePassCollectionUpdate : ObservableCollection<ClasePass>
  {
    public ClasePassCollectionUpdate()
    {
    }

    public ClasePassCollectionUpdate(List<ClasePass> passes)
    {
      for (int index = 0; index < passes.Count; ++index)
        this.Add(passes[index]);
    }

    public void AddAllNew(List<ClasePass> passCollection)
    {
      for (int index = 0; index < passCollection.Count; ++index)
        this.AddNew(passCollection[index]);
    }

    public void AddNew(ClasePass pass)
    {
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].relevantDate > pass.relevantDate)
        {
          this.Insert(index, new ClasePass(pass, false));
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.Add(new ClasePass(pass, false));
    }

    public bool passExists(ClasePass pass)
    {
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumber == pass.serialNumber)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public ClasePass returnPass(string serialNumber)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumberGUID == serialNumber)
          return this[index];
      }
      return (ClasePass) null;
    }

    public ClasePass returnPass(string localID, bool _null)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].idAppointment == localID)
          return this[index];
      }
      return (ClasePass) null;
    }

    public void getFrontPasses()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].getFrontPass();
    }

    public void updateSettings()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].updateSettings();
    }

    public int IndexPass(string serialNumber)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumberGUID == serialNumber)
          return index;
      }
      return -1;
    }

    public void addDeleteDoubles(ClasePass pass)
    {
      int index = this.IndexPass(pass.serialNumberGUID);
      if (index != -1)
        this.RemoveItem(index);
      this.Add(pass);
    }
  }
}
