// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassCollection
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Wallet_Pass
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
        this.AddNew(passCollection[index]);
    }

    public int AddNew(ClasePass pass)
    {
      AppSettings appSettings = new AppSettings();
      int num = -1;
      for (int index = 0; index < this.Count; ++index)
      {
        if (appSettings.listOrder == 0)
        {
          if (this[index].relevantDate > pass.relevantDate)
          {
            this.Insert(index, new ClasePass(pass));
            num = index;
            break;
          }
        }
        else if (this[index].relevantDate < pass.relevantDate)
        {
          this.Insert(index, new ClasePass(pass));
          num = index;
          break;
        }
      }
      if (num == -1)
      {
        this.Add(new ClasePass(pass));
        num = this.Count - 1;
      }
      return num;
    }

    public int swapPass(ClasePass pass)
    {
      int index = this.returnPass(pass.serialNumber, pass.passTypeIdentifier);
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

    public async Task<int> swapPass(ClasePass pass, string serialNumberGUID)
    {
      int index = this.IndexOf(this.returnPass(serialNumberGUID));
      if (index != -1)
      {
        pass.serialNumberGUID = this[index].serialNumberGUID;
        pass.idAppointment = this[index].idAppointment;
        pass.showNotifications = this[index].showNotifications;
        pass.allowUpdates = this[index].allowUpdates;
        pass.filenamePass = this[index].filenamePass;
        pass.dateModified = this[index].dateModified;
        pass.registered = this[index].registered;
        ClaseSaveCalendar _calendar = new ClaseSaveCalendar();
        int num = await _calendar.editAppointment(pass.idAppointment, pass.relevantDate) ? 1 : 0;
        this.RemoveAt(index);
        index = this.AddNew(pass);
      }
      return index;
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

    public int returnPass(string serialNumber, string passTypeIdentifier)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumber == serialNumber && this[index].passTypeIdentifier == passTypeIdentifier)
          return index;
      }
      return -1;
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
  }
}
