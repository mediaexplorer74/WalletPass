// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassCollection
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WalletPass
{
  public class ClasePassCollection : ObservableCollection<ClasePass>
  {
    public ClasePassCollection()
    {
    }

    public ClasePassCollection(List<ClasePass> passes)
    {
      for (int index = 0; index < passes.Count; ++index)
        this.Add(passes[index]);
    }

    public void AddAllNew(List<ClasePass> passCollection)
    {
      for (int index = 0; index < passCollection.Count; ++index)
      {
        if (!string.IsNullOrEmpty(passCollection[index].type))
          this.AddNew(passCollection[index]);
      }
    }

    public int AddNew(ClasePass pass)
    {
      int num = -1;
      AppSettings appSettings = new AppSettings();
      for (int index = 0; index < this.Count; ++index)
      {
        if (appSettings.listOrder == 0)
        {
          if (this[index].relevantDate > pass.relevantDate)
          {
            this.Insert(index, new ClasePass(pass, false));
            num = index;
            break;
          }
        }
        else if (this[index].relevantDate < pass.relevantDate)
        {
          this.Insert(index, new ClasePass(pass, false));
          num = index;
          break;
        }
      }
      if (num == -1)
      {
        this.Add(new ClasePass(pass, false));
        num = this.Count - 1;
      }
      return num;
    }

    public int passIndex(ClasePass pass)
    {
      int num = -1;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumber == pass.serialNumber)
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public int swapPass(ClasePass pass)
    {
      int index = this.returnPass(pass.serialNumber, pass.passTypeIdentifier);
      if (index != -1)
        this[index].swapPass(pass);
      return index;
    }

    public int swapPass(ClasePass pass, string serialNumberGUID)
    {
      int index = this.returnIndex(serialNumberGUID);
      if (index != -1)
      {
        pass.serialNumberGUID = this[index].serialNumberGUID;
        pass.idAppointment = this[index].idAppointment;
        pass.showNotifications = this[index].showNotifications;
        pass.allowUpdates = this[index].allowUpdates;
        pass.filenamePass = this[index].filenamePass;
        pass.dateModified = this[index].dateModified;
        pass.registered = this[index].registered;
        this.RemoveAt(index);
        index = this.AddNew(pass);
      }
      return index;
    }

    public int returnPass(string serialNumber, string passTypeIdentifier)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumber == serialNumber && this[index].passTypeIdentifier == passTypeIdentifier)
          return index;
      }
      return -1;
    }

    public int returnIndex(string serialNumber)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumberGUID == serialNumber)
          return index;
      }
      return -1;
    }

    public bool passExists(ClasePass pass)
    {
      try
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
      catch
      {
        return false;
      }
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

    public void getFrontPasses(bool isTombStone = false)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].getFrontPass(isTombStone);
    }

    public void updateSettings()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].updateSettings();
    }
  }
}
