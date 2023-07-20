// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.IO
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace Wallet_Pass
{
  public static class IO
  {
    public static List<ClasePass> LoadDataPasses()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("passes.xml", typeof (List<ClasePass>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePass>) taskArray[0].Result ?? new List<ClasePass>();
    }

    public static List<ClasePassBackgroundTask> LoadDataPassesBackgroundTask()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("passesBackgroundTask.xml", typeof (List<ClasePassBackgroundTask>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePassBackgroundTask>) taskArray[0].Result ?? new List<ClasePassBackgroundTask>();
    }

    public static List<ClasePassBackgroundTask> LoadDataPassesUpdateBackgroundTask()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("passesUpdateBackgroundTask.xml", typeof (List<ClasePassBackgroundTask>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<ClasePassBackgroundTask>) taskArray[0].Result ?? new List<ClasePassBackgroundTask>();
    }

    public static string LoadDataNotificationReg()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("notificationReg.xml", typeof (string))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (string) taskArray[0].Result ?? "";
    }

    public static List<string> LoadDataMSWalletPasses()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("passesMSWallet.xml", typeof (List<string>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<string>) taskArray[0].Result ?? new List<string>();
    }

    public static List<string> LoadDataMSWalletPassesDelete()
    {
      Task<object>[] taskArray = new Task<object>[1]
      {
        Task.Run<object>((Func<Task<object>>) (async () => await Wallet_Pass.IO.LoadValue("passesMSWalletDelete.xml", typeof (List<string>))))
      };
      Task.WaitAny((Task[]) taskArray);
      return (List<string>) taskArray[0].Result ?? new List<string>();
    }

    public static void SaveUpdateData(ClasePassBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await Wallet_Pass.IO.SaveValue("passesUpdateBackgroundTask.xml", (object) passes))));

    public static void SaveData(ClasePassBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await Wallet_Pass.IO.SaveValue("passesBackgroundTask.xml", (object) passes))));

    public static void SaveData(ClasePassCollection Passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await Wallet_Pass.IO.SaveValue("passes.xml", (object) Passes))));

    public static void SaveDataMSWallet(ClasePassMSWalletBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await Wallet_Pass.IO.SaveValue("passesMSWallet.xml", (object) passes))));

    public static void SaveDataMSWalletDelete(ClasePassMSWalletBackgroundTaskCollection passes) => Task.WaitAny(Task.Run((Func<Task>) (async () => await Wallet_Pass.IO.SaveValue("passesMSWalletDelete.xml", (object) passes))));

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
        stream?.Dispose();
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
        stream?.Dispose();
      }
    }
  }
}
