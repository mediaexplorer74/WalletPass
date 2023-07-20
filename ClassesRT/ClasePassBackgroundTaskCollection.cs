// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassBackgroundTaskCollection
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wallet_Pass
{
  public class ClasePassBackgroundTaskCollection : ObservableCollection<ClasePassBackgroundTask>
  {
    public ClasePassBackgroundTaskCollection()
    {
    }

    public ClasePassBackgroundTaskCollection(List<ClasePassBackgroundTask> passes)
    {
      for (int index = 0; index < passes.Count; ++index)
        this.Add(passes[index]);
    }

    public ClasePassBackgroundTask returnPass(string serialNumber, bool GUID = true)
    {
      if (GUID)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].serialNumberGUID == serialNumber)
            return this[index];
        }
      }
      else
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].serialNumber == serialNumber)
            return this[index];
        }
      }
      return (ClasePassBackgroundTask) null;
    }

    public ClasePassBackgroundTask returnFirst(string passTypeID)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].passTypeIdentifier == passTypeID)
          return this[index];
      }
      return (ClasePassBackgroundTask) null;
    }

    public ClasePassBackgroundTaskCollection returnAll(string passTypeID)
    {
      ClasePassBackgroundTaskCollection backgroundTaskCollection = new ClasePassBackgroundTaskCollection();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].passTypeIdentifier == passTypeID)
          backgroundTaskCollection.Add(this[index]);
      }
      return backgroundTaskCollection;
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

    public void addDeleteDoubles(ClasePassBackgroundTask pass)
    {
      int index = this.IndexPass(pass.serialNumberGUID);
      if (index != -1)
        this.RemoveItem(index);
      this.Add(pass);
    }

    public void removePass(ClasePassBackgroundTask pass)
    {
      int index = this.IndexPass(pass.serialNumberGUID);
      if (index == -1)
        return;
      this.RemoveItem(index);
    }

    public void addDeleteDoubles(ClasePass pass)
    {
      ClasePassBackgroundTask passBackgroundTask = new ClasePassBackgroundTask();
      int index = this.IndexPass(pass.serialNumberGUID);
      passBackgroundTask.type = pass.type;
      passBackgroundTask.transitType = pass.transitType;
      passBackgroundTask.showNotifications = pass.showNotifications;
      passBackgroundTask.allowUpdates = pass.allowUpdates;
      passBackgroundTask.serialNumber = pass.serialNumber;
      passBackgroundTask.serialNumberGUID = pass.serialNumberGUID;
      passBackgroundTask.relevantDate = pass.relevantDate;
      passBackgroundTask.PrimaryFields = pass.PrimaryFields;
      passBackgroundTask.organizationName = pass.organizationName;
      passBackgroundTask.expirationDate = pass.expirationDate;
      passBackgroundTask.webServiceURL = pass.webServiceURL;
      passBackgroundTask.passTypeIdentifier = pass.passTypeIdentifier;
      passBackgroundTask.authenticationToken = pass.authenticationToken;
      passBackgroundTask.dateModified = pass.dateModified;
      passBackgroundTask.sinceUpdate = pass.sinceUpdate;
      passBackgroundTask.PrimaryFields = pass.PrimaryFields;
      passBackgroundTask.idAppointment = pass.idAppointment;
      if (index != -1)
      {
        passBackgroundTask.dateModified = this[index].dateModified;
        this.RemoveItem(index);
      }
      this.Add(passBackgroundTask);
    }
  }
}
