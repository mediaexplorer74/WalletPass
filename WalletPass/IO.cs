// Decompiled with JetBrains decompiler
// Type: WalletPass.IO
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace WalletPass
{
  public static class IO
  {
    public static List<ClasePass> LoadDataPasses()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("passes.xml", typeof (List<ClasePass>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePass>) taskArray[0].Result ?? new List<ClasePass>();
    }

    public static async Task<List<ClasePass>> LoadDataPassesArchived()
    {
      List<ClasePass> loadPasses = (List<ClasePass>) await WalletPass.IO.LoadValue("passesArchived.xml", typeof (List<ClasePass>));
      return loadPasses == null ? new List<ClasePass>() : loadPasses;
    }

    public static async Task<List<ClasePass>> asyncLoadDataPasses()
    {
      List<ClasePass> loadPasses = (List<ClasePass>) await WalletPass.IO.LoadValue("passes.xml", typeof (List<ClasePass>));
      return loadPasses == null ? new List<ClasePass>() : loadPasses;
    }

    public static List<ClasePassBackgroundTask> LoadDataPassesUpdateBackgroundTask()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("passesUpdateBackgroundTask.xml", typeof (List<ClasePassBackgroundTask>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePassBackgroundTask>) taskArray[0].Result ?? new List<ClasePassBackgroundTask>();
    }

    public static List<ClasePassBackgroundTask> LoadDataPassesBackgroundTask()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("passesBackgroundTask.xml", typeof (List<ClasePassBackgroundTask>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePassBackgroundTask>) taskArray[0].Result ?? new List<ClasePassBackgroundTask>();
    }

    public static VersionInfo LoadDataShowTutorial()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("showTutorial.xml", typeof (VersionInfo))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (VersionInfo) taskArray[0].Result ?? new VersionInfo();
    }

    public static bool LoadDataShowMessageBox()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("showMsg.xml", typeof (bool))))
      };
      Task.WaitAny((Task[]) taskArray);
      object result = taskArray[0].Result;
      return result == null || (bool) result;
    }

    public static string LoadDataDevPushToken()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("pushToken.xml", typeof (string))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (string) taskArray[0].Result ?? "";
    }

    public static string LoadDataNotificationReg()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("notificationReg.xml", typeof (string))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (string) taskArray[0].Result ?? "";
    }

    public static List<string> LoadDataMSWalletPasses()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("passesMSWallet.xml", typeof (List<string>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<string>) taskArray[0].Result ?? new List<string>();
    }

    public static List<string> LoadDataMSWalletPassesDelete()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await WalletPass.IO.LoadValue("passesMSWalletDelete.xml", typeof (List<string>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<string>) taskArray[0].Result ?? new List<string>();
    }

    public static void SaveData(ClasePassCollection Passes)
    {
      Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passes.xml", (object) Passes))));
      ClasePassBackgroundTaskCollection backgroundTaskCollection = new ClasePassBackgroundTaskCollection(WalletPass.IO.LoadDataPassesBackgroundTask());
      ClasePassBackgroundTaskCollection passes = new ClasePassBackgroundTaskCollection();
      for (int index = 0; index <= Passes.Count - 1; ++index)
      {
        bool flag = false;
        ClasePassBackgroundTask passBackgroundTask;
        if ((passBackgroundTask = backgroundTaskCollection.returnPass(Passes[index].serialNumberGUID)) != null)
          flag = true;
        else
          passBackgroundTask = new ClasePassBackgroundTask();
        passBackgroundTask.type = Passes[index].type;
        passBackgroundTask.transitType = Passes[index].transitType;
        passBackgroundTask.showNotifications = Passes[index].showNotifications;
        passBackgroundTask.allowUpdates = Passes[index].allowUpdates;
        passBackgroundTask.serialNumber = Passes[index].serialNumber;
        passBackgroundTask.serialNumberGUID = Passes[index].serialNumberGUID;
        passBackgroundTask.relevantDate = Passes[index].relevantDate;
        passBackgroundTask.PrimaryFields = Passes[index].PrimaryFields;
        passBackgroundTask.organizationName = Passes[index].organizationName;
        passBackgroundTask.expirationDate = Passes[index].expirationDate;
        passBackgroundTask.webServiceURL = Passes[index].webServiceURL;
        passBackgroundTask.passTypeIdentifier = Passes[index].passTypeIdentifier;
        passBackgroundTask.authenticationToken = Passes[index].authenticationToken;
        passBackgroundTask.dateModified = Passes[index].dateModified;
        passBackgroundTask.sinceUpdate = Passes[index].sinceUpdate;
        passBackgroundTask.idAppointment = Passes[index].idAppointment;
        passes.Add(passBackgroundTask);
        if (flag)
          backgroundTaskCollection.Remove(passBackgroundTask);
      }
      WalletPass.IO.SaveData(passes);
    }

    public static void SaveDataArchived(ClasePassCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passesArchived.xml", (object) passes))));

    public static void SaveData(ClasePassBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passesBackgroundTask.xml", (object) passes))));

    public static void SaveUpdateData(ClasePassBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passesUpdateBackgroundTask.xml", (object) passes))));

    public static void SaveDataShowTutorial(VersionInfo versioninfo) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("showTutorial.xml", (object) versioninfo))));

    public static void SaveDataShowMessageBox(bool value) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("showMsg.xml", (object) value))));

    public static void SaveDataDevPushToken(string value) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("pushToken.xml", (object) value))));

    public static void SaveDataNotificationReg(string value) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("notificationReg.xml", (object) value))));

    public static void SaveDataMSWallet(ClasePassMSWalletBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passesMSWallet.xml", (object) passes))));

    public static void SaveDataMSWalletDelete(ClasePassMSWalletBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await WalletPass.IO.SaveValue("passesMSWalletDelete.xml", (object) passes))));

    private static async Task<object> LoadValue(string path, Type objectType)
    {
      Stream stream = (Stream) null;
      try
      {
        ApplicationData appData = ApplicationData.Current;
        StorageFolder storageFolder = appData.LocalFolder;
        StorageFile file = await storageFolder.GetFileAsync(path).AsTask<StorageFile>().ConfigureAwait(false);
        stream = await ((IStorageFile) file).OpenStreamForReadAsync().ConfigureAwait(false);
        DataContractSerializer ser = new DataContractSerializer(objectType);
        return ser.ReadObject(stream);
      }
      catch
      {
        return (object) null;
      }
      finally
      {
        if (stream != null)
        {
          stream.Close();
          stream.Dispose();
        }
      }
    }

    private static async Task SaveValue(string path, object saveObject)
    {
      Stream stream = (Stream) null;
      try
      {
        ApplicationData appData = ApplicationData.Current;
        StorageFolder storageFolder = appData.LocalFolder;
        StorageFile file = await storageFolder.CreateFileAsync(path, (CreationCollisionOption) 1);
        stream = await ((IStorageFile) file).OpenStreamForWriteAsync();
        DataContractSerializer ser = new DataContractSerializer(saveObject.GetType());
        ser.WriteObject(stream, saveObject);
      }
      catch
      {
      }
      finally
      {
        if (stream != null)
        {
          stream.Close();
          stream.Dispose();
        }
      }
    }
  }
}
