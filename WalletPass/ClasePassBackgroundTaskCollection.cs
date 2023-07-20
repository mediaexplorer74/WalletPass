// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassBackgroundTaskCollection
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WalletPass
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

    public ClasePassBackgroundTask returnPass(string serialNumber)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].serialNumberGUID == serialNumber)
          return this[index];
      }
      return (ClasePassBackgroundTask) null;
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
      if (index != -1)
      {
        passBackgroundTask.dateModified = this[index].dateModified;
        this.RemoveItem(index);
      }
      this.Add(passBackgroundTask);
    }
  }
}
