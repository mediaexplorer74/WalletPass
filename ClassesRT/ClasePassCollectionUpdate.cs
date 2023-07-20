// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassCollectionUpdate
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wallet_Pass
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
          this.Insert(index, new ClasePass(pass));
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.Add(new ClasePass(pass));
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
